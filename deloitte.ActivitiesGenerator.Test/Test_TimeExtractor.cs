using deloitte.fileParser;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace deloitte.ActivitiesGenerator.Tests
{
    [TestClass()]
    public class Test_TimeExtractor
    {
        [TestMethod()]
        public void Test_getDurationIsCorrect()
        {
            string data = "AAAAAAAAAA 12min";
            int actual = TimeExtractor.Instance.getDuration(data);
            Assert.AreEqual(12, actual);

        }

        [TestMethod()]
        public void Test_getDuration_MissingDurationIsZero()
        {
            string data = "AAAAAAAAAA";
            int actual = TimeExtractor.Instance.getDuration(data);
            Assert.AreEqual(0, actual);
        }

        [TestMethod()]
        public void Test_getDuration_sprint()
        {
            string data = "AAAAAAAAAA sprint";
            int actual = TimeExtractor.Instance.getDuration(data);
            Assert.AreEqual(15, actual);
        }

        [TestMethod()]
        public void Test_getDuration_nullDataException()
        {
            try
            {
                TimeExtractor.Instance.getDuration(null);
                Assert.Fail("Exception not thrown");
            }
            catch (Exception e)
            {
                Type actual = e.GetType();
                Type expected = typeof(ActivitiesGeneratorException);
                Assert.AreSame(actual, expected);
            }
        }

        [TestMethod()]
        public void Test_getDuration_emptyDataException()
        {
            try
            {
                TimeExtractor.Instance.getDuration("");
                Assert.Fail("Exception not thrown");
            }
            catch (Exception e)
            {
                Type actual = e.GetType();
                Type expected = typeof(ActivitiesGeneratorException);
                Assert.AreSame(actual, expected);
            }
        }
    }
}

