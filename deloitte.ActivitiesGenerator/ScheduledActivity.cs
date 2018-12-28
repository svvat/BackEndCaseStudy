using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deloitte.fileParser
{
    public interface IScheduledActivity
    {
        DateTime getStart();
        DateTime getEnd();
        IActivity getActivity();
    }

    public class ScheduledActivity : IScheduledActivity
    {
        private IActivity _activity;
        private DateTime _startTime;

        public ScheduledActivity(IActivity activity, DateTime startTime)
        {
            _activity = activity;
            _startTime = startTime;
        }

        public IActivity getActivity()
        {
            return _activity;
        }

        public DateTime getStart()
        {
            return _startTime;
        }

        public DateTime getEnd()
        {
            try
            {
                return _getEnd();
            }
            catch (ActivitiesGeneratorException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new ActivitiesGeneratorException("ScheduledActivity 1", e);
            }
        }

        private DateTime _getEnd()
        {
            return _startTime.Add(new TimeSpan(0, _activity.getDuration(), 0));
        }

        public override string ToString()
        {
            try
            {
                return _ToString();
            }
            catch (ActivitiesGeneratorException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new ActivitiesGeneratorException("ScheduledActivity 2", e);
            }
        }

        private string _ToString()
        {
            return String.Concat(
                getStart().ToString("hh:mm tt : "),
                getActivity().ToString()
                );
        }
    }
}
