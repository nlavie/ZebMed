using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZebMed
{
    public class NFSRetrieval : IDataRetrieval
    {
        public string dataPath;

        public NFSRetrieval(string dataPath)
        {
            this.dataPath = dataPath;
        }

        public string GetFileByFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return null;

            string[] files;
            try
            {
                files = Directory.GetFiles(dataPath, "*.*", SearchOption.AllDirectories);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting file list in directory: " + dataPath, ex.InnerException);
            }

            foreach (string file in files)
            {
                string[] splitedFileName = file.Split(new string[] { "\\" }, StringSplitOptions.None);

                if (splitedFileName[splitedFileName.Count() - 1].Equals(fileName))
                {
                    return file;
                }
            }

            return null;
        }

        public Dictionary<string,string[]> GetAllSeriesForStudy(string studyId)
        {
            if (string.IsNullOrEmpty(studyId))
                return null;

            Dictionary<string, string[]> result = new Dictionary<string, string[]>();

            List<string> seriesDirectories = GetAllSeriesDirectoriesForStudy(studyId);

            result = GetFilesForStudyPerSeries(seriesDirectories);

            return result;
        }

        public List<string> GetSeriesBySeriesId(string seriesId)
        {
            if (string.IsNullOrEmpty(seriesId))
                return null;

            string seriesDirectoryPath = string.Empty;

            List<string> result = new List<string>();

            try
            {
                seriesDirectoryPath = DirSearch(dataPath, "series_" + seriesId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error reading files from directory" + dataPath, ex.InnerException);
            }

            if (!string.IsNullOrEmpty(seriesDirectoryPath))
            {
                string[] files;
                try
                {
                    files = Directory.GetFiles(seriesDirectoryPath, "*.*", SearchOption.AllDirectories);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting file list in directory: " + seriesDirectoryPath, ex.InnerException);
                }

                foreach (string file in files)
                {
                    result.Add(file);
                }
            }

            return result;
        }

        private Dictionary<string,string[]> GetFilesForStudyPerSeries(List<string> series)
        {
            Dictionary<string, string[]> result = new Dictionary<string, string[]>();

            foreach(string directory in series)
            {
                string[] seriesNameSplited = directory.Split(new string[] { "\\" }, StringSplitOptions.None);
                result.Add(seriesNameSplited[seriesNameSplited.Count() - 1], Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories));
            }

            return result;
        }

        private List<string> GetAllSeriesDirectoriesForStudy(string studyId)
        {
            List<string> result = new List<string>();

            string seriesDirectory = DirSearch(dataPath, "study_" + studyId);

            if (!string.IsNullOrEmpty(seriesDirectory))
            {
                foreach (string directory in Directory.GetDirectories(seriesDirectory))
                {
                    result.Add(directory);
                }
            }

            return result;
        }

        private string DirSearch(string dataPath, string searchdDirectory)
        {
            string[] directories = Directory.GetDirectories(dataPath, "*", SearchOption.AllDirectories);

            foreach (string directory in directories)
            {
                string[] splitedDirectoryName = directory.Split(new string[] { "\\" }, StringSplitOptions.None);
                if (searchdDirectory.Equals(splitedDirectoryName[splitedDirectoryName.Count() - 1]))
                {
                    return directory;
                }
            }

            return null;
        }
    }
}
