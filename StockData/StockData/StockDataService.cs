using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StockData
{
    class StockDataService
    {
        public static List<Stock> ReadFile(string filepath)
        {
            var lines = File.ReadAllLines(filepath);

            var data = from line in lines.Skip(1)
                       let split = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)") //split words by comma but not including commas in double quotes ""
                       select new Stock
                       {
                           //regex gets rid of " and numbers that are in () - negative numbers
                           Symbol = Regex.Replace(split[0].Replace("\"", ""), @"\((.*?)\)", ""),
                           Date = DateTime.Parse(split[1]),
                           Open = Regex.Replace(split[2].Replace("\"", ""), @"\((.*?)\)", ""),
                           High = Regex.Replace(split[3].Replace("\"", ""), @"\((.*?)\)", ""),
                           Low = Regex.Replace(split[4].Replace("\"", ""), @"\((.*?)\)", ""),
                           Close = Regex.Replace(split[5].Replace("\"", ""), @"\((.*?)\)", "")
                       };

            return data.ToList();
        }
    }
}
