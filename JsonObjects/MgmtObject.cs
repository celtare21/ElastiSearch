using Nest;
using Newtonsoft.Json;

namespace ElastiSearch.Indexer.JsonObjects
{
    public class MgmtObject
    {
        [JsonProperty("mgmtID")]
        public long MgmtId { get; set; }

        [JsonProperty("name")]
        [Text(Analyzer = "full_english_search", SearchAnalyzer = "full_english_search", Name = nameof(Name))]
        public string Name { get; set; }

        [JsonProperty("market")]
        public string Market { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }
    }
}
