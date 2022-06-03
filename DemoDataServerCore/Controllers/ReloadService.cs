using Flexmonster.DataServer.Core.DataStorages;
using System.Threading.Tasks;

namespace DemoDataServerCore.Controllers
{

    public class ReloadService 
    {
        private readonly IDataStorage _dataStorage;

        public ReloadService(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }

        public async Task Reload(string indexName)
        {
            // Refresh the data index:
            _dataStorage.Remove(indexName);
            await _dataStorage.GetOrAddAsync(indexName);
        }
    }
}
