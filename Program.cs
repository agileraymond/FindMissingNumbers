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
            var sourceNumbers = new Dictionary<string, string>();
            
            using (StreamReader sr = new StreamReader(sourcePath))
            {
                while (sr.Peek() >= 0)
                {                    
                    var jobInfo = sr.ReadLine().Split('\t');
                    if (jobInfo.Length > 0)
                    {
                        sourceNumbers.Add(jobInfo[0].Trim(), jobInfo[1].Trim());
                    }
                    else
                    {
                        //Console.WriteLine(jobInfo[0]);
                    } 
                }
            }

            Console.WriteLine($"source jobs: {sourceNumbers.Count}");

            var destinationPath = @"c:\code\destination.txt";
            var destinationNumbers = new Dictionary<string, string>();
            using (StreamReader sr = new StreamReader(destinationPath))
            {
                while (sr.Peek() >= 0)
                {                    
                    var ifsJobInfo = sr.ReadLine().Split('\t');
                                        
                    if (ifsJobInfo.Length > 0)
                    {
                        destinationNumbers.Add(ifsJobInfo[0].Trim(), GetJobStatus(ifsJobInfo[1].Trim()));
                    }
                    else
                    {
                        //Console.WriteLine(ifsJobInfo[0]);
                    } 
                }
            }

            var missingNumbers = new List<string>();
            var missingPath = @"c:\code\missing-" + DateTime.Now.ToString("yyyy-M-dd-hh-mm-ss") + ".txt";
            
            foreach (var s in sourceNumbers)
            {
                if (destinationNumbers.ContainsKey(s.Key))
                {
                    var ifsJob = destinationNumbers[s.Key];
                    if (ifsJob != null && s.Value != ifsJob)
                    {
                        missingNumbers.Add($"{s.Key}, Mas status {s.Value} - {ifsJob}");
                    }
                }
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

        static string GetJobStatus(string ifsStatus)
        {
            var masStatus = "O";
            
            switch (ifsStatus)
            {
                case "APPOINTMENT":
                    masStatus = "APPOINTMENT";
                    break;
                case "ASSIGNED":
                    masStatus = "A";
                    break;
                case "BILLABLE":
                    masStatus = "B";
                    break;
                case "BOOKED":
                    masStatus = "S";
                    break;
                case "CANCELED":
                    masStatus = "L";
                    break;
                case "CLEARED":
                    masStatus = "A";
                    break;
                case "CLOSED":
                    masStatus = "C";
                    break;
                case "COMPLETED":
                    masStatus = "D";
                    break;
                case "ENROUTE":
                    masStatus = "A";
                    break;
                case "ONSITE":
                    masStatus = "A";
                    break;
                case "OPEN":
                    masStatus = "O";
                    break;
                case "RECALL":
                    masStatus = "R";
                    break;
                case "TAKEN":
                    masStatus = "T";
                    break;  
                default:
                    masStatus = "X";
                    break;                  
            }

            return masStatus;
        }
    }
}
