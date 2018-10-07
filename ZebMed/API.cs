using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZebMed
{
    public enum Method { Web, NFS };

    public class API
    {
        
        public string studyId { get; set; }
        public string dataSource { get; set; }
        public Method connectionType { get; set; }
        public IDataRetrieval dataRetrieval { get; set; }

        public API(string studyId, string dataSource, Method connectionType)
        {
            this.connectionType = connectionType;
            this.dataSource = dataSource;
            this.studyId = studyId;
            Creator creator = new Creator();
            this.dataRetrieval = creator.Create(studyId, dataSource, connectionType);
        }

        public  Dictionary<string, string[]> GetAllSeriesForStudy(string studyId)
        {
            if (this.dataRetrieval == null || studyId == null)
                return null;

            return dataRetrieval.GetAllSeriesForStudy(studyId);
        }

        public List<string> GetSeriesBySeriesId(string seriesId)
        {
            if (this.dataRetrieval == null || seriesId == null)
                return null;

            return dataRetrieval.GetSeriesBySeriesId(seriesId);
        }

        public string GetFileByFileName(string fileName)
        {
            if (this.dataRetrieval == null || fileName == null)
                return null;

            return dataRetrieval.GetFileByFileName(fileName);
        }
    }
}
