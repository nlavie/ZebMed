using MongoDB.Bson.IO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ZebMed
{
    public class AnswerAllSeriesForStudy
    {
        public List<string> result;
    }

    public class AnswerGetSeriesBySeriesId
    {
        public string[] result;
    }

    public class AnswerGetFileByFileName
    {
        public string result;
    }

    public class WebInterfaceRetrieval : IDataRetrieval
    {
        public string studyId { get; set; }
        public string dataSource { get; set; }
        private string getListOfSeriesURI { get; set; }
        private string getListOfScansURI { get; set; }
        private string getScanFileURI { get; set; }
        private string studyIdInterfaceParameter = "study_id";
        private string seriesIdInterfaceParameter = "series_id";
        private string fileNameInterfaceParameter = "file_name";
        private string dataSourceInterfaceParameter = "data_source";

        public WebInterfaceRetrieval(string studyId, string dataSource)
        {
            this.dataSource = dataSource;
            this.studyId = studyId;
            this.getListOfScansURI = ConfigurationSettings.AppSettings["getListOfScansURI"];
            this.getListOfSeriesURI = ConfigurationSettings.AppSettings["getListOfSeriesURI"];
            this.getScanFileURI = ConfigurationSettings.AppSettings["getScanFileURI"];
        }

        public string GetFileByFileName(string fileName)
        {
            string result = string.Empty;

            List<string> seriesNameList = this.GetAllSeriesNamesForStudy(studyId);

            foreach (string seriesName in seriesNameList)
            {
                string getFileApiResult = this.GetFileByDataSourceStudyIdSeriesIdAndFilename(dataSource, studyId, seriesName, fileName);

                if (!string.IsNullOrEmpty(getFileApiResult))
                {
                    result = getFileApiResult;
                    break;
                }
            }

            return result;
        }

        private string GetFileByDataSourceStudyIdSeriesIdAndFilename(string dataSource, string studyId, string seriesId, string fileName)
        {
            var client = new HttpClient();
            var pairs = new List<KeyValuePair<string, string>>();
            pairs.Add(new KeyValuePair<string, string>(this.dataSourceInterfaceParameter, this.dataSource));
            pairs.Add(new KeyValuePair<string, string>(this.studyIdInterfaceParameter, studyId));
            pairs.Add(new KeyValuePair<string, string>(this.seriesIdInterfaceParameter, seriesId));
            pairs.Add(new KeyValuePair<string, string>(this.fileNameInterfaceParameter, fileName));

            HttpContent requestBody = new StringContent(MongoDB.Bson.BsonExtensionMethods.ToJson(pairs));
            requestBody.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            try
            {
                var response = client.PostAsync(this.getScanFileURI, requestBody).Result;
                string result = response.Content.ReadAsStringAsync().Result;

                AnswerGetFileByFileName answer = Newtonsoft.Json.JsonConvert.DeserializeObject<AnswerGetFileByFileName>(result);

                if (answer == null)
                    return null;

                return answer.result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while calling API" + this.getListOfScansURI, ex.InnerException);
            }
        }

        public Dictionary<string, string[]> GetAllSeriesForStudy(string studyId)
        {
            Dictionary<string, string[]> result = new Dictionary<string, string[]>();

            List<string> seriesNameList = this.GetAllSeriesNamesForStudy(studyId);

            foreach (string seriesName in seriesNameList)
            {
                result.Add(seriesName, this.GetSeriesBySeriesId(seriesName).ToArray());
            }

            return result;
        }

        public List<string> GetSeriesBySeriesId(string seriesId)
        {
            var client = new HttpClient();
            var pairs = new List<KeyValuePair<string, string>>();
            pairs.Add(new KeyValuePair<string, string>(this.dataSourceInterfaceParameter, this.dataSource));
            pairs.Add(new KeyValuePair<string, string>(this.studyIdInterfaceParameter, studyId));
            pairs.Add(new KeyValuePair<string, string>(this.seriesIdInterfaceParameter, seriesId));

            HttpContent requestBody = new StringContent(MongoDB.Bson.BsonExtensionMethods.ToJson(pairs));
            requestBody.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            try
            {
                var response = client.PostAsync(this.getListOfScansURI, requestBody).Result;
                string result = response.Content.ReadAsStringAsync().Result;

                AnswerGetSeriesBySeriesId answer = Newtonsoft.Json.JsonConvert.DeserializeObject<AnswerGetSeriesBySeriesId>(result);

                if (answer == null)
                    return null;

                return answer.result.ToList<string>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while calling API" + this.getListOfScansURI, ex.InnerException);
            }
        }

        private List<string> GetAllSeriesNamesForStudy(string studyId)
        {
            var client = new HttpClient();
            var pairs = new List<KeyValuePair<string, string>>();
            pairs.Add(new KeyValuePair<string, string>(this.dataSourceInterfaceParameter, this.dataSource));
            pairs.Add(new KeyValuePair<string, string>(this.studyIdInterfaceParameter, studyId));

            HttpContent requestBody = new StringContent(MongoDB.Bson.BsonExtensionMethods.ToJson(pairs));
            requestBody.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            try
            {
                var response = client.PostAsync(this.getListOfSeriesURI, requestBody).Result;
                string result = response.Content.ReadAsStringAsync().Result;

                AnswerAllSeriesForStudy answer = Newtonsoft.Json.JsonConvert.DeserializeObject<AnswerAllSeriesForStudy>(result);

                if (answer == null)
                    return null;

                return answer.result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while calling API" + this.getListOfSeriesURI, ex.InnerException);
            }
        }

        private Boolean AreFileNamesEqual(string fileName1, string fileName2)
        {
            /*
             * Implement Remote file name Trimming so it can be equaled 
             * to simple file name, I'll assume it's working because I dont have anyway 
             * telling what is the remote file path format
             */

            return true;
        }

    }
}
