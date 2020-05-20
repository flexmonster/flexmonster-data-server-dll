using Flexmonster.DataServer.Core;
using Flexmonster.DataServer.Core.Parsers;
using System;
using System.Collections.Generic;

namespace DemoDataServerCore
{
    public class CustomParser : IParser
    {
        public CustomParser()
        {
            Type = "custom";
        }

        public string Type { get; private set; }

        public IndexOptions IndexOptions { get; set; }

        //Map column name and type to column index. Must be not null
        public Dictionary<int, ColumnInfo> ColumnInfoByIndex { get; set; }

        public IEnumerable<object[,]> Parse()
        {
            FillDataInfo();
            for (int i = 0; i < data.GetLength(0); i++)
            {
                var partData = new object[1, 3];
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    partData[0, j] = data[i, j];
                }
                //return data by line
                yield return partData;
            }
        }

        //fill info about columns(name and type)
        private void FillDataInfo()
        {
            ColumnInfoByIndex = new Dictionary<int, ColumnInfo>();
            ColumnInfoByIndex.Add(0, new ColumnInfo("Country", data[0, 0].GetType()));
            ColumnInfoByIndex.Add(1, new ColumnInfo("Price", data[0, 1].GetType()));
            ColumnInfoByIndex.Add(2, new ColumnInfo("Date", data[0, 2].GetType()));
        }

        private object[,] data = new object[24, 3]
        {
           { "Canada",174,new DateTime(2019,1,25)},
           { "USA",502,new DateTime(2019,2,25)},
           { "Canada",242,new DateTime(2019,3,25)},
           { "Australia",102,new DateTime(2019,4,25)},
           { "Germany",126,new DateTime(2019,5,25)},
           { "Germany",1246,new DateTime(2019,6,25)},
           { "Australia",680,new DateTime(2019,7,25)},
           { "USA",1241,new DateTime(2019,8,25)},
           { "Canada",244,new DateTime(2019,9,25)},
           { "France",501,new DateTime(2019,10,25)},
           { "Canada",690,new DateTime(2019,11,25)},
           { "France",115,new DateTime(2019,12,25)},
           { "USA",174,new DateTime(2019,1,25)},
           { "Germany",100,new DateTime(2019,2,25)},
           { "Canada",777,new DateTime(2019,3,25)},
           { "Australia",100,new DateTime(2019,4,25)},
           { "Germany",883,new DateTime(2019,5,25)},
           { "USA",588,new DateTime(2019,6,25)},
           { "USA",310,new DateTime(2019,7,25)},
           { "Germany",56,new DateTime(2019,8,25)},
           { "Canada",810,new DateTime(2019,9,25)},
           { "France",672,new DateTime(2019,10,25)},
           { "Australia",395,new DateTime(2019,11,25)},
           { "Australia",412,new DateTime(2019,12,25)}
        };
    }
}