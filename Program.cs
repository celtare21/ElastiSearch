using System;
using ElastiSearch.Indexer.FileOperations;
using ElastiSearch.Indexer.JsonObjects;
using ElastiSearch.Indexer.Stopwatch;

namespace ElastiSearch.Indexer
{
    public static class Program
    {
        public static void Main()
        {
            var stopWatch = new StopwatchHelper();
            var client = new EsClient<Properties>(stopWatch);
            var filePath = FilePath.GetFilePath();

            client.IndexJsonObject(filePath, "properties");

            Console.WriteLine("Finished");
        }
    }
}
