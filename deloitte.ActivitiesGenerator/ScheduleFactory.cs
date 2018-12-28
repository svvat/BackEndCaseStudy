using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deloitte.fileParser
{
    public interface IScheduleFactory
    {
        ISchedule createSchedule(int startIndex = 0);
    }

    public class ScheduleFactory : IScheduleFactory
    {
        private List<IActivity> _activities;

        public ScheduleFactory(List<IActivity> activities)
        {
            _activities = activities;
        }

        public ISchedule createSchedule(int startIndex = 0)
        {
            try
            {
                return _createSchedule(startIndex);
            }
            catch (ActivitiesGeneratorException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new ActivitiesGeneratorException("ScheduleFactory 1", e);
            }
        }

        private ISchedule _createSchedule(int startIndex)
        {

            ISchedule schedule = new Schedule(TimeManager.STARTHOUR);
            int len = _activities.Count;

            for (int count = 0; count < len; count++)
            {
                int idx = startIndex + count;
                while (idx >= len)
                {
                    idx -= len;
                }
                IActivity activity = _activities[idx];

                if (!schedule.addIfSlotAvailable(activity))
                {
                    break;
                }
            }
            return schedule;
        }
    }
}
