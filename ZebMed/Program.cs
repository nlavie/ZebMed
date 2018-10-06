using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace ZebMed
{
    class Program
    {
        static void Main(string[] args)
        {
            API nfs = new API("b2", "a", "WEB");

            string t = nfs.GetFileByFileName("b211.txt");

            List<string> tt = nfs.GetSeriesBySeriesId("b21");

            Dictionary<string,string[]> ttt = nfs.GetAllSeriesForStudy("b2");


            
        }
    }
}
