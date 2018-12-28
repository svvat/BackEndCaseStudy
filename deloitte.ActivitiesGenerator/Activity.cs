using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deloitte.fileParser
{
    public interface IActivity
    {
        int getDuration();
        string ToString();
    }
    public class Activity : IActivity
    {
        private string _data;
        private ITimeExtractor _timeExtractor;

        public Activity(string data)
        {
            _data = data;
            _timeExtractor = TimeExtractor.Instance;
        }

        public int getDuration()
        {
            try
            {
                return _getDuration();
            }
            catch (ActivitiesGeneratorException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new ActivitiesGeneratorException("Activity 1", e);
            }

        }

        private int _getDuration()
        {
            return _timeExtractor.getDuration(_data);
        }

        public override string ToString()
        {
            return _data;
        }
    }
}
