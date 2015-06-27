using System;
using System.Text;

namespace PolygonMerge
{
    class ToFileFromFile
    {
        public static void SaveToFile(string[] lines, string FileName)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(FileName)) 
            {
                foreach (string line in lines)
                {
                   file.WriteLine(line);                    
                }
            }
        }
        public static string[] ReadFromFile(string FileName)
        {
            string[] lines = System.IO.File.ReadAllLines(FileName);
            return lines;
        }
    }
}
