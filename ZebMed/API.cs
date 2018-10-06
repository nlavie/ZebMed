using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZebMed
{
    public class API
    {
        public string studyId { get; set; }
        public string dataSource { get; set; }
        public string connectionType { get; set; }
        public IDataRetrieval dataRetrieval { get; set; }

        public API(string studyId, string dataSource, string connectionType)
        {
            this.connectionType = connectionType;
            this.dataSource = dataSource;
            this.studyId = studyId;
            Creator creator = new Creator();
            this.dataRetrieval = creator.Create(studyId, dataSource, connectionType);
        }

        public  Dictionary<string, string[]> GetAllSeriesForStudy(string studyId)
        {
            return dataRetrieval.GetAllSeriesForStudy(studyId);
        }

        public List<string> GetSeriesBySeriesId(string seriesId)
        {
            return dataRetrieval.GetSeriesBySeriesId(seriesId);
        }

        public string GetFileByFileName(string fileName)
        {
            return dataRetrieval.GetFileByFileName(fileName);
        }
    }
}
