using Nest;
using Newtonsoft.Json;

namespace ElastiSearch.Indexer.JsonObjects
{
    [ElasticsearchType(RelationName = "properties")]
    public class Properties
    {
        [JsonProperty("property")]
        public Property Property { get; set; }

        [JsonProperty("suggest")]
        [Completion]
        public CompletionField Suggest { get; set; }
    }
}
