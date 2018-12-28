using deloitte.fileParser;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace deloitte.ActivitiesGenerator.Tests
{
    [TestClass()]
    public class Test_TimeManager
    {
        ITimeManager _scheduleManager = TimeManager.Instance;

        [TestMethod()]
        public void Test_getTimeSlot_goodTimeAM()
        {
            DateTime goodTime = DateTime.Parse("09:00 AM");
            DateTime actual = _scheduleManager.getTimeSlot(goodTime, 15);
            Assert.AreEqual(actual, goodTime);
        }

        [TestMethod()]
        public void Test_getTimeSlot_badTimeAMFail()
        {
            DateTime badTime = DateTime.Parse("11:45 AM");
            DateTime actual = _scheduleManager.getTimeSlot(badTime, 20);
            Assert.AreNotEqual(actual, badTime);
        }

        [TestMethod()]
        public void Test_getTimeSlot_badTimeAMGoodTimePM()
        {
            DateTime badTime = DateTime.Parse("11:45 AM");
            DateTime goodTime = DateTime.Parse("01:00 PM");
            DateTime actual = _scheduleManager.getTimeSlot(badTime, 20);
            Assert.AreEqual(actual, goodTime);
        }
        [TestMethod()]
        public void Test_getTimeSlot_goodTimePM()
        {
            DateTime goodTime = DateTime.Parse("01:00 PM");
            DateTime actual = _scheduleManager.getTimeSlot(goodTime, 15);
            Assert.AreEqual(actual, goodTime);
        }

        [TestMethod()]
        public void Test_getTimeSlot_TooEarlyFail()
        {
            DateTime goodTime = DateTime.Parse("09:00 AM");
            DateTime badTime = DateTime.Parse("08:00 AM");
            DateTime actual = _scheduleManager.getTimeSlot(badTime, 15);
            Assert.AreNotEqual(actual, badTime);
            Assert.AreEqual(actual, goodTime);
        }

        [TestMethod()]
        public void Test_getTimeSlot_TooEarlyGetFirstSlot()
        {
            DateTime goodTime = DateTime.Parse("09:00 AM");
            DateTime badTime = DateTime.Parse("08:00 AM");
            DateTime actual = _scheduleManager.getTimeSlot(badTime, 15);
            Assert.AreEqual(actual, goodTime);
        }

        [TestMethod()]
        public void Test_getTimeSlot_TooLate()
        {
            DateTime badTime = DateTime.Parse("05:40 PM");
            DateTime actual = _scheduleManager.getTimeSlot(badTime, 50);
            Assert.AreNotEqual(actual, badTime);
            Assert.AreEqual(actual, DateTime.MinValue);
        }

        [TestMethod()]
        public void Test_getStartTime()
        {
            DateTime actual = _scheduleManager.getStartTime();
            Assert.AreEqual(actual.Hour, TimeManager.STARTHOUR);
        }

        [TestMethod()]
        public void Test_getLunchTime()
        {
            DateTime actual = _scheduleManager.getLunchTime();
            Assert.AreEqual(actual.Hour, TimeManager.STARTLUNCHHOUR);
        }

        [TestMethod()]
        public void Test_getLunchEndTime()
        {
            DateTime actual = _scheduleManager.getLunchEndTime();
            Assert.AreEqual(actual.Hour, TimeManager.ENDLUNCHHOUR);
        }
    }
}

