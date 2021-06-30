using Nest;
using Newtonsoft.Json;

namespace ElastiSearch.Indexer.JsonObjects
{
    [ElasticsearchType(RelationName = "property")]
    public class Property
    {
        [JsonProperty("propertyID")]
        public long PropertyId { get; set; }

        [JsonProperty("name")]
        [Text(Analyzer = "full_english_search", SearchAnalyzer = "full_english_search", Name = nameof(Name))]
        public string Name { get; set; }

        [JsonProperty("formerName")]
        [Text(Analyzer = "full_english_search", SearchAnalyzer = "full_english_search", Name = nameof(FormerName))]
        public string FormerName { get; set; }

        [JsonProperty("streetAddress")]
        public string StreetAddress { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("market")]
        public string Market { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lng")]
        public double Lng { get; set; }
    }
}
