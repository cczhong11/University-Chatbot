using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
public class FindPeople
    {
        public FindPeople()
        {
            string file2 = "seu领导.txt";
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file2);
            string[] lines = System.IO.File.ReadAllLines(path);

            // Display the file contents by using a foreach loop.
            System.Console.WriteLine("Contents of WriteLines2.txt = ");
            foreach (string line in lines)
            {
                // Use a tab to indent each line of the file.
                Console.WriteLine("\t" + line);
            }

        }
        static public string getPeople(string school, string job)
        {
            string[] lines = System.IO.File.ReadAllLines("seu领导.txt");
            string[] result;
            foreach (string line in lines)
            {
                // Use a tab to indent each line of the file.
                result = line.Split('\t');
                if (result[0] == school && result[1].Contains(job))
                {
                    return result[2];
                }
                
            }
            return "不知道";
        }
    }
