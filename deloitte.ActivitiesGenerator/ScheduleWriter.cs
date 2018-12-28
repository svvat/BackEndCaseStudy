using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deloitte.fileParser
{

    public interface IScheduleWriter
    {
        void writeHeader(int teamN);
        void write(ISchedule schedule);
    }
    public class ScheduleWriter : IScheduleWriter
    {
        private StreamWriter _writer;

        public ScheduleWriter(StreamWriter writer)
        {
            _writer = writer;
        }

        public void write(ISchedule schedule)
        {
            _write(schedule);
        }

        public void writeHeader(int teamN)
        {
            try
            {
                _writeHeader(teamN);
            }
            catch (ActivitiesGeneratorException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new ActivitiesGeneratorException("messafe", e);
            }
        }

        private void _write(ISchedule schedule)
        {
            int nActivities = schedule.getActivites().Count;
            for (int idx = 0; idx < nActivities; idx++)
            {
                IScheduledActivity scheduledActivity = schedule.getActivites()[idx];
                _writer.WriteLine(scheduledActivity.ToString());
            }
        }

        private void _writeHeader(int teamN)
        {
            if (teamN > 1)
            {
                _writer.WriteLine();
            }
            _writer.WriteLine("Team " + teamN.ToString() + " :");
        }
    }
}
