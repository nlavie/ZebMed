using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZebMed
{
    public class Creator
    {
        public IDataRetrieval Create(string studyId, string dataSource, string connectionType)
        {
            if (connectionType.Equals("WEB"))
            {
                return new WebInterfaceRetrieval(studyId, dataSource);
            }
            else if (connectionType.Equals("NFS"))
            {
                if (dataSource.Equals("test"))
                {
                    return new NFSRetrieval(GetTestingDirectory());
                }
                else
                {
                    return new NFSRetrieval(ConfigurationSettings.AppSettings["dataPath"]);
                }
            }

            return null;
        }

        private string GetTestingDirectory()
        {
            DirectoryInfo grandFother = Directory.GetParent(Environment.CurrentDirectory.ToString()).Parent.Parent;

            return grandFother.FullName + "\\ZebMedTests\\bin\\Debug";
        }
    }
}
