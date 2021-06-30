using Newtonsoft.Json;

namespace ElastiSearch.Indexer.JsonObjects
{
    public class MgmtCollection
    {
        [JsonProperty("mgmt")]
        public MgmtObject MgmtMgmt { get; set; }
    }
}
