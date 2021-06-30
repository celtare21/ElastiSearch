using System.Collections.Generic;
using System.IO;
using System.Linq;
using Nest;
using Newtonsoft.Json;
using Properties = ElastiSearch.Indexer.JsonObjects.Properties;

namespace ElastiSearch.Indexer.FileOperations
{
    public static class JsonDeserializer<T> where T : class
    {
        private static IEnumerable<T> JsonToEnumerable(string filePath)
        {
            var jsonString = File.ReadAllText(filePath);

            return JsonConvert.DeserializeObject<List<T>>(jsonString);
        }

        private static IEnumerable<T> InitSuggestionsEnumerable(string filePath)
        {
            var jsonObjects = JsonToEnumerable(filePath);

            if (typeof(T) == typeof(List<Properties>))
            {
                foreach (var jsonObject in (List<Properties>) jsonObjects)
                {
                    jsonObject.Suggest = new CompletionField
                    {
                        Input = jsonObject.Property.Name.Split(' ').ToList().Append(jsonObject.Property.Name),
                        Weight = 5
                    };
                }
            }

            return jsonObjects;
        }

        public static (T[], T[]) SplitRequests(string filePath)
        {
            var jsonObjects = InitSuggestionsEnumerable(filePath);
            var objectsArray = jsonObjects.ToArray();
            var arrayLength = objectsArray.Length;

            return (objectsArray[..(arrayLength / 2)], objectsArray[(arrayLength / 2)..]);
        }
    }
}
