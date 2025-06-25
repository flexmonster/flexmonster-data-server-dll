using Flexmonster.DataServer.Core.Logger;
using Flexmonster.DataServer.Core.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Flexmonster.DataServer.Core.Parsers
{
    public class CsvUrlIndexOptions : CsvIndexOptions {
        public CsvUrlIndexOptions(string path) : base(path)
        {
            Type = "csv_url";
        }
    }
    public class CSVURLParser : IParser
    {
        private CSVSerializerOptions _serializerOptions;
        private NumberFormatInfo _numberFormatInfo;
        private Dictionary<string, MappingObject> _mapping = new Dictionary<string, MappingObject>();
        private string _fullFilePath;
        private const ushort CHUNK_SIZE = ushort.MaxValue;
        private object[,] _dataBlock;
        private int index;

        public Dictionary<int, ColumnInfo> ColumnInfoByIndex { get; set; }
        public IndexOptions IndexOptions { get; set; }
        public string Type { get; set; }

        private readonly PathManager _pathManager;

        public CSVURLParser(PathManager pathManager = null)
        {
            Type = "csv_url";
            _pathManager = pathManager;
        }

        public virtual IEnumerable<object[,]> Parse()
        {
            ColumnInfoByIndex = new Dictionary<int, ColumnInfo>();

            var csvOptions = IndexOptions as CsvIndexOptions;
            _fullFilePath = csvOptions?.Path;
            var delimeter = csvOptions?.Delimiter;
            var decimalSeparator = csvOptions?.DecimalSeparator;
            var thousandsSeparator = csvOptions?.ThousandsSeparator;
            _serializerOptions = new CSVSerializerOptions();
            _numberFormatInfo = new CultureInfo("en-US", false).NumberFormat;

            if (delimeter.HasValue)
            {
                _serializerOptions.Delimiter = delimeter.Value;
            }
            if (decimalSeparator.HasValue)
            {
                _numberFormatInfo.CurrencyDecimalSeparator = decimalSeparator.Value.ToString();
                _numberFormatInfo.NumberDecimalSeparator = decimalSeparator.Value.ToString();
            }
            if (thousandsSeparator.HasValue)
            {
                _numberFormatInfo.CurrencyGroupSeparator = thousandsSeparator.Value.ToString();
                _numberFormatInfo.NumberGroupSeparator = thousandsSeparator.Value.ToString();
            }
            if (csvOptions.Mapping != null)
            {
                _mapping = csvOptions.Mapping;
            }

            using (var httpClient = new HttpClient())
            {
                var stream = httpClient.GetStreamAsync(_fullFilePath).GetAwaiter().GetResult();
                using (var reader = new StreamReader(stream))
                {
                    string line = "";
                    index = 0;
                    string headerLine = reader.ReadLine();
                    List<string> readingChunk = new List<string>(CHUNK_SIZE);

                    // first lines are required to detect data types
                    for (int i = 0; i < 10; i++)
                    {
                        line = reader.ReadLine();
                        if (line != null)
                        {
                            readingChunk.Add(line);
                            index++;
                        }
                    }
                    // parse headers
                    ParseHeader(headerLine, readingChunk);

                    while ((line = reader.ReadLine()) != null)
                    {
                        line = line.Trim();
                        if (line.Length > 0)
                        {
                            readingChunk.Add(line);
                            index++;
                            if (index % CHUNK_SIZE == 0)
                            {
                                _dataBlock = new object[CHUNK_SIZE, ColumnInfoByIndex.Keys.Count];
                                ParseBlock(readingChunk);
                                readingChunk = new List<string>();
                                yield return _dataBlock;
                            }
                        }
                    }
                    _dataBlock = new object[index % CHUNK_SIZE, ColumnInfoByIndex.Keys.Count];
                    ParseBlock(readingChunk);
                    readingChunk = new List<string>();
                    yield return _dataBlock;
                }
            }
        }

        private void ParseHeader(string header, List<string> firstLines)
        {
            var columnCount = header.Split(_serializerOptions.Delimiter).Count();
            var columnNames = ParseLineAsString(header, columnCount);
            string[][] lines = new string[firstLines.Count][];
            int i = 0;
            firstLines.ForEach(line =>
            {
                lines[i] = ParseLineAsString(line, columnNames.Count());
                i++;
            });
            i = 0;
            foreach (var columnName in columnNames)
            {
                List<string> column = new List<string>(firstLines.Count);
                for (int j = 0; j < firstLines.Count; j++)
                {
                    column.Add(lines[j][i]);
                }
                if (_mapping.ContainsKey(columnName))
                {
                    ColumnInfoByIndex.Add(i, new ColumnInfo(columnName, DataTypeHelper.SelectType(_mapping[columnName].Type)));
                }
                else
                {
                    ColumnInfoByIndex.Add(i, new ColumnInfo(columnName, DetectType(column)));
                }
                i++;
            }
        }

        private Type DetectType(List<string> column)
        {
            int dateCount = 0;
            int numberCount = 0;
            int stringCount = 0;
            int emptyCount = 0;
            foreach (var value in column)
            {
                if (double.TryParse(value, NumberStyles.Any, _numberFormatInfo, out _))
                {
                    numberCount++;
                }
                else if (DateTime.TryParse(value, out _))
                {
                    dateCount++;
                }
                else if (value != "")
                {
                    stringCount++;
                }
                else
                {
                    emptyCount++;
                }
            }
            if (dateCount > stringCount)
            {
                if (dateCount >= numberCount)
                {
                    return typeof(DateTime);
                }
            }
            if (stringCount > 0)
            {
                return typeof(string);
            }
            if (numberCount > 0)
            {
                return typeof(double);
            }
            return typeof(string);
        }

        private void ParseBlock(List<string> lines)
        {
            Parallel.For(0, lines.Count, (i =>
            {
                ParseLine(i, lines[i]);
            }));
        }

        private string[] ParseLineAsString(string line, int totalLineInHeader)
        {
            var parsedLine = new List<string>(); char char_;
            bool isQuote = false;
            string value = "";
            int length = line.Length;
            int currentWord = 0;
            for (int i = 0; i < length; i++)
            {
                char_ = line[i];
                if (char_ == _serializerOptions.FieldEnclosureToken)
                {
                    isQuote = !isQuote;
                }
                else
                if (char_ == _serializerOptions.Delimiter && !isQuote)
                {
                    parsedLine.Add(value);
                    currentWord++;
                    value = "";
                }
                else
                {
                    value += char_;
                }
            }
            parsedLine.Add(value);
            if (parsedLine.Count < totalLineInHeader)
            {
                parsedLine.Add("");
            }
            return parsedLine.ToArray();
        }

        private void ParseLine(int rowIndex, string line)
        {
            bool isQuote = false;
            char char_;
            string value = "";
            int length = line.Length;
            int currentWord = 0;
            for (int i = 0; i < length; i++)
            {
                char_ = line[i];
                if (char_ == _serializerOptions.FieldEnclosureToken)
                {
                    isQuote = !isQuote;
                }
                else
                if (char_ == _serializerOptions.Delimiter && !isQuote)
                {
                    if (ColumnInfoByIndex[currentWord].Type == typeof(double))
                    {
                        if (double.TryParse(value, NumberStyles.Any, _numberFormatInfo, out double convertedValue))
                        {
                            _dataBlock[rowIndex, currentWord] = convertedValue;
                        }
                        else
                        {
                            _dataBlock[rowIndex, currentWord] = null;
                        }
                    }
                    else if (ColumnInfoByIndex[currentWord].Type == typeof(DateTime))
                    {
                        if (DateTime.TryParse(value, out DateTime convertedValue))
                            _dataBlock[rowIndex, currentWord] = convertedValue;
                        else
                            _dataBlock[rowIndex, currentWord] = null;
                    }
                    else
                    {
                        if (value != "")
                            _dataBlock[rowIndex, currentWord] = value;
                        else
                            _dataBlock[rowIndex, currentWord] = null;
                    }
                    currentWord++;
                    value = "";
                }
                else
                {
                    value += char_;
                }
            }
            if (ColumnInfoByIndex[currentWord].Type == typeof(double))
            {
                if (double.TryParse(value, NumberStyles.Any, _numberFormatInfo, out double convertedValue))
                {
                    _dataBlock[rowIndex, currentWord] = convertedValue;
                }
                else
                {
                    _dataBlock[rowIndex, currentWord] = null;
                }
            }
            else if (ColumnInfoByIndex[currentWord].Type == typeof(DateTime))
            {
                if (DateTime.TryParse(value, out DateTime convertedValue))
                    _dataBlock[rowIndex, currentWord] = convertedValue;
                else
                    _dataBlock[rowIndex, currentWord] = null;
            }
            else
            {
                if (value != "")
                    _dataBlock[rowIndex, currentWord] = value;
                else
                    _dataBlock[rowIndex, currentWord] = null;
            }
            if (currentWord + 1 < _dataBlock.GetLength(1))
            {
                for (int i = currentWord + 1; i < _dataBlock.GetLength(1); i++)
                {
                    if (ColumnInfoByIndex[i].Type == typeof(double) || ColumnInfoByIndex[i].Type == typeof(DateTime))
                    {
                        _dataBlock[rowIndex, i] = null;
                    }
                    else
                    {
                        _dataBlock[rowIndex, i] = null;
                    }
                }
            }
        }
    }
}