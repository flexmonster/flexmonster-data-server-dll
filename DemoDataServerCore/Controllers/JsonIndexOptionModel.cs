namespace DemoDataServerCore.Controllers
{
    public class JsonIndexOptionModel
    {
        public string Path;
        public int RefreshTime;
        public string IndexName;

    public JsonIndexOptionModel(string Path, int RefreshTime, string IndexName)
        {
            this.Path = Path;
            this.RefreshTime = RefreshTime;
            this.IndexName = IndexName;
        }

    }
}
