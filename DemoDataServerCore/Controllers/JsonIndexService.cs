using Flexmonster.DataServer.Core;
using Flexmonster.DataServer.Core.DataStorages;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace DemoDataServerCore.Controllers
{
    public class JsonIndexService
    {
        public IDataStorage _dataStorage;
        public IOptionsMonitor<DatasourceOptions> _datasourceOptions;

        public JsonIndexService(IOptionsMonitor<DatasourceOptions> datasourceOptions,
                                IDataStorage dataStorage)
        {
            _datasourceOptions = datasourceOptions;
            _dataStorage = dataStorage;
        }
        //JsonIndexOptionModel - class, containing parameters that will be passed as settings of a new index
        //You can implement it in any way you want
        public async Task Create(JsonIndexOptionModel indexOptions)
        {
            var jsonIndexOptions = new JsonIndexOptions(indexOptions.Path);
            jsonIndexOptions.RefreshTime = indexOptions.RefreshTime;
            _datasourceOptions.CurrentValue.Indexes.Add(indexOptions.IndexName, jsonIndexOptions);
            await _dataStorage.GetOrAddAsync(indexOptions.IndexName);
        }
    }

}
