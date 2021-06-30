using System;
using ElastiSearch.Indexer.Interfaces;

namespace ElastiSearch.Indexer.Stopwatch
{
    public class StopwatchHelper : IStopWatch
    {
        private System.Diagnostics.Stopwatch _watch;

        public void StartWatch()
        {
            Console.WriteLine("Starting indexing...");

            _watch = System.Diagnostics.Stopwatch.StartNew();
        }

        public void StopWatch()
        {
            _watch.Stop();

            Console.WriteLine($"Execution Time: {_watch.ElapsedMilliseconds / 1000} s");
        }
    }
}
