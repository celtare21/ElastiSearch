using System;

namespace ElastiSearch.Indexer.FileOperations
{
    public static class FilePath
    {
        public static string GetFilePath()
        {
            string path = default;

            while (string.IsNullOrWhiteSpace(path))
            {
                Console.WriteLine("Please input the full path of the file:");
                path = Console.ReadLine();
            }

            return path;
        }
    }
}
