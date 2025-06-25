using Flexmonster.DataServer.Core.DataStorages;
using Flexmonster.DataServer.Core;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Flexmonster.DataServer.Core.Parsers;

namespace DemoDataServerCore.Controllers
{

    public class IndexCreateService
    {
        private readonly IDataStorage _dataStorage;
        private readonly DatasourceOptions _datasourceOptions;

        public IndexCreateService(IDataStorage dataStorage, IOptionsMonitor<DatasourceOptions> datasourceOptions)
        {
            _dataStorage = dataStorage;
            _datasourceOptions = datasourceOptions.CurrentValue;
        }

        public async Task CreateIndexIfNotExists(string indexName)
        {
            bool indexExists = _datasourceOptions.Indexes.ContainsKey(indexName);
            if (!indexExists)
            {
                _datasourceOptions.Indexes.Add(indexName, new CsvUrlIndexOptions("https://cdn.flexmonster.com/data/data.csv"));
                await _dataStorage.GetOrAddAsync(indexName);
            }
        }

        public void DeleteIndex(string indexName)
        {
            if (_datasourceOptions.Indexes.ContainsKey(indexName))
            {
                _datasourceOptions.Indexes.Remove(indexName);
                _dataStorage.Remove(indexName);
            }
        }
    }

}