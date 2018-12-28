using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deloitte.fileParser
{

    public interface ITimeManager
    {
        DateTime getStartTime();
        DateTime getLunchTime();
        DateTime getLunchEndTime();
        DateTime getTimeSlot(DateTime requestTime, int duration);
    }
    public class TimeManager : ITimeManager
    {
        #region constants
        public const int STARTHOUR = 9;
        public const int STARTLUNCHHOUR = 12;
        public const int ENDLUNCHHOUR = 13;
        public const int EARLIESTPRESENTATIONHOUR = 16;
        public const int LATESTPRESENTATIONHOUR = 17;
        #endregion

        private static ITimeManager instance = null;

        private DateTime _startTime;
        private DateTime _lunchTime;
        private DateTime _endLunchTime;
        private DateTime _earlyPresentationTime;
        private DateTime _lastPresentationTime;

        private TimeManager()
        {
            DateTime today = DateTime.Today;
            _startTime = today.Add(new TimeSpan(STARTHOUR, 0, 0));
            _lunchTime = today.Add(new TimeSpan(STARTLUNCHHOUR, 0, 0));
            _endLunchTime = today.Add(new TimeSpan(ENDLUNCHHOUR, 0, 0));
            _earlyPresentationTime = today.Add(new TimeSpan(EARLIESTPRESENTATIONHOUR, 0, 0));
            _lastPresentationTime = today.Add(new TimeSpan(LATESTPRESENTATIONHOUR, 0, 0));
        }

        public static ITimeManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TimeManager();
                }
                return (ITimeManager)instance;
            }
        }

        public DateTime getTimeSlot(DateTime requestTime, int duration)
        {
            try
            {

                return _getTimeSlot(requestTime, duration);

            }
            catch (ActivitiesGeneratorException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new ActivitiesGeneratorException("TimeManager 1", e);
            }

        }


        private DateTime _getTimeSlot(DateTime requestTime, int duration)
        {
            DateTime time = requestTime;
            if (timeIsBeforeStart(time))
            {
                time = _startTime;
            }
            else
            {
                if (!activityEndsBeforeLunch(requestTime, duration))
                {
                    if (activityStartsBeforeLunchEnds(requestTime))
                    {
                        time = _endLunchTime;
                    }
                    else
                    {
                        if (!activityEndsBeforeLastPresentation(time, duration))
                        {
                            return DateTime.MinValue;
                        }
                    }
                }
            }
            return time;
        }

        private bool timeIsBeforeStart(DateTime time)
        {
            return (time.Ticks < _startTime.Ticks);
        }

        private bool activityEndsBeforeLunch(DateTime time, int duration)
        {
            DateTime endTime = time.Add(new TimeSpan(0, duration, 0));
            return (endTime.Ticks < _lunchTime.Ticks);
        }

        private bool activityEndsBeforeLastPresentation(DateTime time, int duration)
        {
            DateTime endTime = time.Add(new TimeSpan(0, duration, 0));
            return (endTime.Ticks <= _lastPresentationTime.Ticks);
        }

        private bool activityStartsBeforeLunchEnds(DateTime time)
        {
            return (time.Ticks < _endLunchTime.Ticks);
        }

        public DateTime getStartTime()
        {
            return _startTime;
        }

        public DateTime getLunchTime()
        {
            return _lunchTime;
        }

        public DateTime getLunchEndTime()
        {
            return _endLunchTime;
        }



    }
}
