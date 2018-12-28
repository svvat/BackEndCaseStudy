using deloitte.fileParser;
using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace deloitte.ActivitiesGenerator.Tests
{
    [TestClass()]
    public class Test_ScheduleWriter
    {
        [TestMethod()]
        public void Test_ScheduleWriterNew()
        {
            int startHour = 9;
            MemoryStream ms = new MemoryStream();
            StreamWriter sw = new StreamWriter(ms);
            IScheduleWriter o = new ScheduleWriter(sw);
            ISchedule sh = new Schedule(startHour);
            Assert.AreNotSame(sh, null);
        }

        [TestMethod()]
        public void Test_write()
        {
            string task1 = "AAAA sprint";
            string task2 = "BBBB 10min";
            string task3 = "CCCC 20min";
            int startHour = 9;
            MemoryStream ms = new MemoryStream();
            StreamWriter sw = new StreamWriter(ms);
            IScheduleWriter o = new ScheduleWriter(sw);
            ISchedule sh = new Schedule(startHour);
            sh.addIfSlotAvailable(new Activity(task1));
            sh.addIfSlotAvailable(new Activity(task2));
            sh.addIfSlotAvailable(new Activity(task3));
            o.write(sh);
            string actual = readStream(ms, sw);
            string expected = "0" + startHour.ToString() + ":00 AM : " + task1 + "\r\n"
                            + "0" + startHour.ToString() + ":15 AM : " + task2 + "\r\n"
                            + "0" + startHour.ToString() + ":25 AM : " + task3 + "\r\n";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Test_writeHeader()
        {
            MemoryStream ms = new MemoryStream();
            StreamWriter sw = new StreamWriter(ms);
            IScheduleWriter o = new ScheduleWriter(sw);

            o.writeHeader(2);

            string expected = "\r\nTeam 2 :\r\n";
            string actual = readStream(ms, sw, expected.Length);

            Assert.AreEqual(expected, actual);
        }

        private string readStream(MemoryStream ms, StreamWriter sw, int len = 100)
        {
            sw.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            byte[] buffer = new byte[len];
            ms.Read(buffer, 0, (int)ms.Length);
            return System.Text.Encoding.Default.GetString(buffer).Substring(0, (int)ms.Length);
        }
    }
}

