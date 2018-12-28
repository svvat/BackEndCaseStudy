using System;

namespace deloitte.fileParser
{

    public interface ITimeExtractor
    {
        int getDuration(string rec);
    }

    public class TimeExtractor : ITimeExtractor
    {
        private const string SPRINTTEXT = "sprint";
        private const int SPRINTVALUE = 15;
        private const string MINTEXT = "min";
        private readonly int MINLENGTH = MINTEXT.Length;

        private static ITimeExtractor instance = null;

        private TimeExtractor() { }

        public static ITimeExtractor Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TimeExtractor();
                }
                return (ITimeExtractor) instance;
            }
        }

        public int getDuration(string rec)
        {
            try
            {
                return _getDuration(rec);
            }
            catch (Exception e)
            {
                throw new ActivitiesGeneratorException("TimeExtractor 1", e);
            }
        }

        private int _getDuration(string rec)
        {
            int duration = 0;
            char[] delim = { ' ' };

            if (rec.Trim().Length > 0)
            {
                string[] sLines = rec.Split(delim);

                string time = sLines[sLines.Length - 1];

                if (time.Equals(SPRINTTEXT))
                {
                    duration = SPRINTVALUE;
                }
                else
                {
                    if (time.EndsWith(MINTEXT))
                    {
                        string digits = time.Substring(0, time.Length - MINLENGTH);
                        duration = Int32.Parse(digits);
                    }
                }
            }
            else
            {
                throw new ActivitiesGeneratorException("TimeExtractor 2");
            }

            return duration;
        }
    }
}
