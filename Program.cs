using System;
using System.Collections.Generic;
using System.IO;

namespace FindMissingNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var sourcePath = @"c:\code\source.txt";
            var sourceNumbers = new List<int>();
            using (StreamReader sr = new StreamReader(sourcePath))
            {
                while (sr.Peek() >= 0)
                {
                    sourceNumbers.Add(int.Parse(sr.ReadLine()));
                }
            }

            var destinationPath = @"c:\code\destination.txt";
            var destinationNumbers = new List<int>();
            using (StreamReader sr = new StreamReader(destinationPath))
            {
                while (sr.Peek() >= 0)
                {
                    destinationNumbers.Add(int.Parse(sr.ReadLine()));
                }
            }

            var missingNumbers = new List<int>();
            var missingPath = @"c:\code\missing.txt";
            
            foreach (var s in sourceNumbers)
            {   
                if (!destinationNumbers.Contains(s))
                    missingNumbers.Add(s);
            }

            using (StreamWriter sw = new StreamWriter(missingPath))
            {
                foreach (var j in missingNumbers)
                {
                    sw.WriteLine(j);
                }
            }

            Console.WriteLine("final list");
        }
    }
}
