using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace deloitte.fileParser
{
    public interface IStreamReaderActivitiesCreator
    {
        List<IActivity> create(StreamReader reader);
    }
    public class StreamReaderActivitiesCreator : IStreamReaderActivitiesCreator
    {
        #region singltonManagement
        private static IStreamReaderActivitiesCreator instance = null;

        private StreamReaderActivitiesCreator() { }

        public static IStreamReaderActivitiesCreator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StreamReaderActivitiesCreator();
                }
                return (IStreamReaderActivitiesCreator)instance;
            }
        }
        #endregion

        public List<IActivity> create(StreamReader reader)
        {
            try
            {
                return _create(reader);
            }
            catch (ActivitiesGeneratorException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new ActivitiesGeneratorException("StreamReaderActivitiesCreator 1", e);
            }

        }

        private List<IActivity> _create(StreamReader reader)
        {
            List<IActivity> activities = new List<IActivity>();
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                activities.Add(new Activity(line));
            }
            return activities;
        }

    }
}
