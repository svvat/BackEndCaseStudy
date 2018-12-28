using deloitte.fileParser;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace deloitte.ActivitiesGenerator.Tests
{

    public class Mocked_Activity : IActivity
    {
        public const int DURATION = 60;
        public const string TEXT = "AAAA";
        public int getDuration()
        {
            return DURATION;
        }
        public override string ToString()
        {
            return TEXT;
        }
    }


    [TestClass()]
    public class Test_ScheduledActivity
    {
        private const string TIMETEXT = "01:01 PM";
        private DateTime DT = DateTime.Parse(TIMETEXT);

        [TestMethod()]
        public void Test_ScheduledActivityInstance()
        {
            IActivity a = new Mocked_Activity();
            IScheduledActivity o = new ScheduledActivity(a, DT);
            Assert.AreNotSame(o, null);
        }

        [TestMethod()]
        public void Test_getActivity()
        {
            IActivity a = new Mocked_Activity();
            IScheduledActivity o = new ScheduledActivity(a, DT);
            Assert.AreSame(a, o.getActivity());
        }

        [TestMethod()]
        public void Test_getStart()
        {
            IActivity a = new Mocked_Activity();
            IScheduledActivity o = new ScheduledActivity(a, DT);

            DateTime actual = o.getStart();
            DateTime expected = DT;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Test_getEnd()
        {
            IActivity a = new Mocked_Activity();
            IScheduledActivity o = new ScheduledActivity(a, DT);

            DateTime actual = o.getEnd();
            DateTime expected = DT.Add(new TimeSpan(0, Mocked_Activity.DURATION, 0));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Test_ToString()
        {
            IActivity a = new Mocked_Activity();
            IScheduledActivity o = new ScheduledActivity(a, DT);
            string actual = o.ToString();
            string expected = TIMETEXT + " : " + Mocked_Activity.TEXT;
            Assert.AreEqual(actual, expected);
        }
    }
}

