using System;
using System.Linq;
using ElastiSearch.Indexer.FileOperations;
using ElastiSearch.Indexer.Interfaces;
using ElastiSearch.Indexer.Secrets;
using Nest;

namespace ElastiSearch.Indexer
{
    public class EsClient<T> where T : class
    {
        private readonly IStopWatch _watch;
        public readonly ElasticClient Client;

        public EsClient(IStopWatch watch)
        {
            _watch = watch;

            Client = GetElasticClient();
        }

        private static ElasticClient GetElasticClient()
        {
            var config = SecretsHolder.GetConfigurationRoot();
            var node = new Uri(config.GetSection("UserSecrets")["NodeUri"]);
            var settings = new ConnectionSettings(node);

            settings.BasicAuthentication(config.GetSection("UserSecrets")["Username"], config.GetSection("UserSecrets")["Password"]);
            settings.ThrowExceptions();
            settings.PrettyJson();

            return new ElasticClient(settings);
        }

        private void CreateIndex(string index)
        {
            _ = Client.Indices.Create(index, c => c
                .Settings(s => s
                    .Analysis(a => a
                        .TokenFilters(tf => tf
                            .Stop("english_stop", st => st
                                .StopWords("_english_")
                            )
                            .Stemmer("english_stemmer", st => st
                                .Language("english")
                            )
                        )
                        .Analyzers(aa => aa
                            .Custom("full_english_search", ca => ca
                                .Tokenizer("standard")
                                .Filters("lowercase",
                                    "trim",
                                    "english_stop",
                                    "english_stemmer")
                            )
                        )
                    )
                )
                .Map<T>(m => m
                    .AutoMap()
                ));

            /*
             * Tech Debt
             * Would this be better?
             *
                .Custom("autocomplete", cc => cc
                   .Filters("eng_stopwords", "trim", "lowercase")
                   .Tokenizer("autocomplete")
               )
                .Tokenizers(tdesc => tdesc
                    .EdgeNGram("autocomplete", e => e
                        .MinGram(3)
                        .MaxGram(15)
                        .TokenChars(TokenChar.Letter, TokenChar.Digit)
                    )
                )
                .TokenFilters(f => f
                    .Stop("eng_stopwords", lang => lang
                        .StopWords("_english_")
                    )
                );
             */
        }

        public void IndexJsonObject(string filePath, string indexName)
        {
            _watch.StartWatch();

            CreateIndex(indexName);

            var (request1, request2) = JsonDeserializer<T>.SplitRequests(filePath);
            var result1 = Client.IndexMany(request1, indexName);
            var result2 = Client.IndexMany(request2, indexName);

            Console.WriteLine($"Valid 1: {result1.IsValid}");
            Console.WriteLine($"Valid 2: {result2.IsValid}");
            if (result1.Errors || result2.Errors)
            {
                Console.WriteLine("Items with errors:");
                foreach (var resultItemsWithError in result1.ItemsWithErrors.Concat(result2.ItemsWithErrors))
                {
                    Console.WriteLine(resultItemsWithError);
                }
            }

            _watch.StopWatch();
        }
    }
}