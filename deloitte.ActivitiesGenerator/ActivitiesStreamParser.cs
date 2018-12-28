using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deloitte.fileParser
{
    public interface IActivitiesStreamParser
    {
        bool generate(StreamReader reader, StreamWriter writer, int nTeams);
    }
    public class ActivitiesStreamParser : IActivitiesStreamParser
    {
        private static IActivitiesStreamParser instance = null;
        private IStreamReaderActivitiesCreator _streamReaderActivitiesCreator;
        private ActivitiesStreamParser()
        {
            _streamReaderActivitiesCreator = StreamReaderActivitiesCreator.Instance;
        }

        public static IActivitiesStreamParser Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ActivitiesStreamParser();
                }
                return (IActivitiesStreamParser)instance;
            }
        }

        public bool generate(StreamReader reader, StreamWriter writer, int nTeams)
        {
            try
            {
                return _generate(reader, writer, nTeams);
            }
            catch (ActivitiesGeneratorException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new ActivitiesGeneratorException("ActivitiesStreamParser 1", e);
            }

        }

        private bool _generate(StreamReader reader, StreamWriter writer, int nTeams)
        {
            bool retVal = false;

            List<IActivity> activities = _streamReaderActivitiesCreator.create(reader);

            int nTotalActivities = activities.Count;

            IScheduleFactory organiser = new ScheduleFactory(activities);
            IScheduleWriter scheduleWriter = new ScheduleWriter(writer);

            for (int teamN = 1; teamN <= nTeams; teamN++)
            {
                scheduleWriter.writeHeader(teamN);
                int startIdx = teamN == 1 ? 0 : nTotalActivities / teamN;
                ISchedule schedule = organiser.createSchedule(startIdx);
                scheduleWriter.write(schedule);
            }

            return retVal;
        }


    }
}
