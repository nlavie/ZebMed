using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZebMed
{
    public interface IDataRetrieval
    {
        Dictionary<string, string[]> GetAllSeriesForStudy(string studyId);

        List<string> GetSeriesBySeriesId(string seriesId);

        string GetFileByFileName(string fileName);
    }
}
