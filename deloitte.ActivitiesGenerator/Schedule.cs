using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deloitte.fileParser
{

    public interface ISchedule
    {
        List<IScheduledActivity> getActivites();
        DateTime getStartTime();
        bool addIfSlotAvailable(IActivity activity);
        IScheduledActivity getPresentation();
    }

    public class Schedule : ISchedule
    {
        private static IScheduledActivity _lunch = null;
        private IScheduledActivity _presentation = null;
        private List<IScheduledActivity> _activites;
        private DateTime _startTime;

        private ITimeManager _scheduleManager;

        public Schedule(int startHour)
        {
            _startTime = DateTime.Today.Add(new TimeSpan(startHour, 0, 0));
            _activites = new List<IScheduledActivity>();
            _scheduleManager = TimeManager.Instance;
        }

        public List<IScheduledActivity> getActivites()
        {
            return _activites;
        }
        
        public DateTime getStartTime()
        {
            return _startTime;
        }

        public bool addIfSlotAvailable(IActivity activity)
        {
            try
            {
                return _addIfSlotAvailable(activity);
            }
            catch (ActivitiesGeneratorException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new ActivitiesGeneratorException("Schedule 1", e);
            }
        }

        private bool _addIfSlotAvailable(IActivity activity)
        {
            bool slotAvailable = true;
            DateTime nextSlot = getNextSlot(activity);
            slotAvailable = !nextSlot.Equals(DateTime.MinValue);
            if (slotAvailable)
            {
                checkIfLunchIsRequired(nextSlot);
                createSlot(activity, nextSlot);
            }
            else
            {
                addPresentation();
            }
            return slotAvailable;
        }

        private DateTime getNextSlot(IActivity activity)
        {
            DateTime nextTime = _scheduleManager.getStartTime();
            if (_activites.Count > 0)
            {
                IScheduledActivity lastActivity = _activites.Last();
                if (lastActivity != null)
                {
                    DateTime lastTime = lastActivity.getEnd();
                    int duration = activity.getDuration();
                    nextTime = _scheduleManager.getTimeSlot(lastTime, duration);
                }
            }
            return nextTime;
        }

        private void checkIfLunchIsRequired(DateTime time)
        {
            if (time.Equals(_scheduleManager.getLunchEndTime()))
            {
                _activites.Add(getLunch());
            }
        }
        
        private void createSlot(IActivity activity, DateTime time)
        {
            IScheduledActivity newActivity = new ScheduledActivity(activity, time);
            _activites.Add(newActivity);
        }

        private void addPresentation()
        {
            _activites.Add(getPresentation());
        }

        public IScheduledActivity getPresentation()
        {
            try
            {
                return _getPresentation();
            }
            catch (ActivitiesGeneratorException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new ActivitiesGeneratorException("Schedule 2", e);
            }

        }
        private IScheduledActivity _getPresentation()
        {
            if (_presentation == null)
            {
                IScheduledActivity lastActivity = _activites.Last();
                DateTime lastTime = lastActivity.getEnd();
                _presentation = new ScheduledActivity(
                                                        new Activity("Staff Motivation Presentation"),
                                                        lastActivity.getEnd()
                                                        );
            }
            return _presentation;
        }

        public IScheduledActivity getLunch()
        {
            try
            {
                return _getLunch();
            }
            catch (ActivitiesGeneratorException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new ActivitiesGeneratorException("Schedule 3", e);
            }
        }

        private IScheduledActivity _getLunch()
        {
            if (_lunch == null)
            {
                _lunch = new ScheduledActivity(
                    new Activity("Lunch Break 60min"),
                    _scheduleManager.getLunchTime()
                    );
            }
            return _lunch;
        }
    }
}
