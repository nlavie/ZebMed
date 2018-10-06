using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZebMed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ZebMed.Tests
{
    [TestClass()]
    public class APITests
    {
        private string testDataPath = Environment.CurrentDirectory+"\\Data";
        /*
         * 
         * NFS - Positive Tests
         * 
         */
        [TestMethod()]
        public void GetAllSeriesForStudyTest()
        {
            // arrange

            API api = new API("b2", "test", "nfs");
            Dictionary<string, string[]> expected = new Dictionary<string, string[]>();
            expected.Add("series_b21",new string[] { this.testDataPath + "\\data_b\\study_b2\\series_b21\\b211.txt", this.testDataPath + "\\data_b\\study_b2\\series_b21\\b212.txt" });
            expected.Add("series_b22", new string[] { this.testDataPath + "\\data_b\\study_b2\\series_b22\\b221.txt", this.testDataPath + "\\data_b\\study_b2\\series_b22\\b222.txt" });
            // act

            Dictionary<string, string[]> actual = api.GetAllSeriesForStudy("b2");

            // assert

            Assert.AreEqual(expected["series_b21"][0], actual["series_b21"][0]);
            Assert.AreEqual(expected["series_b21"][1], actual["series_b21"][1]);
            Assert.AreEqual(expected["series_b22"][0], actual["series_b22"][0]);
            Assert.AreEqual(expected["series_b22"][1], actual["series_b22"][1]);
        }

        [TestMethod()]
        public void GetSeriesBySeriesIdTest()
        {
            // arrange
            API api = new API("b2", "test", "nfs");
            List<string> expected = new List<string>()
            {
                this.testDataPath+ "\\data_b\\study_b2\\series_b21\\b211.txt",
                this.testDataPath+ "\\data_b\\study_b2\\series_b21\\b212.txt"
            };
            // act

            List<string> actual = api.GetSeriesBySeriesId("b21");

            // assert

            if (actual == null || actual.Count == 0)
                Assert.Fail();

            foreach(string s in actual)
            {
                Assert.IsTrue(expected.Contains(s));
            }
        }

        [TestMethod()]
        public void GetFileByFileNameTest()
        {
            // arrange
            API api = new API("b2", "test", "nfs");
            string expected = this.testDataPath + "\\data_b\\study_b2\\series_b21\\b212.txt";
            // act

            string actual = api.GetFileByFileName("b212.txt");

            // assert

            Assert.AreEqual(expected, actual);
        }

        /*
         * 
         * NFS - Negative Tests
         * 
         */

        [TestMethod()]
        public void GetAllSeriesForStudyTest_FAIL_NULL_STUDY_ID()
        {
            // arrange

            API api = new API("b2", "test", "nfs");
            // act

            Dictionary<string, string[]> actual = api.GetAllSeriesForStudy(null);

            // assert

            Assert.IsNull(actual);
        }

        [TestMethod()]
        public void GetSeriesBySeriesIdTest_FAIL_NULL_SERIES_ID()
        {
            // arrange
            API api = new API("b2", "test", "nfs");
            // act

            List<string> actual = api.GetSeriesBySeriesId(null);

            // assert

            Assert.IsNull(actual);
        }

        [TestMethod()]
        public void GetFileByFileNameTest_FAIL_NULL_FILENAME()
        {
            // arrange
            API api = new API("b2", "test", "nfs");
            // act

            string actual = api.GetFileByFileName(null);

            // assert

            Assert.IsNull(actual);
        }

        [TestMethod()]
        public void GetAllSeriesForStudyTest_FAIL_NOT_EXISTS_STUDY_ID()
        {
            // arrange

            API api = new API("b2", "test", "nfs");
            // act

            Dictionary<string, string[]> actual = api.GetAllSeriesForStudy("zebra");

            // assert

            Assert.AreEqual(actual.Count,0);
        }

        [TestMethod()]
        public void GetSeriesBySeriesIdTest_FAIL_NOT_EXISTS_SERIES_ID()
        {
            // arrange
            API api = new API("b2", "test", "nfs");
            // act

            List<string> actual = api.GetSeriesBySeriesId("zebra");

            // assert

            Assert.AreEqual(actual.Count, 0);
        }

        [TestMethod()]
        public void GetFileByFileNameTest_FAIL_NOT_EXISTS_FILENAME()
        {
            // arrange
            API api = new API("b2", "test", "nfs");
            // act

            string actual = api.GetFileByFileName("zebra");

            // assert

            Assert.IsTrue(string.IsNullOrEmpty(actual));
        }

        /*
         * 
         * Web Interface - Not implemented because the web interface replies can only be assumed
         * 
         */
    }
}
