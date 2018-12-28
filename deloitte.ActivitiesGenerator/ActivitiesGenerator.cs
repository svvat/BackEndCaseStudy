using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deloitte.fileParser
{

    public interface IActivitiesGenerator
    {
        bool generate(string fileIn, string fileOut, int nTeams);
    }

    public class ActivitiesGenerator : IActivitiesGenerator
    {

        private static IActivitiesGenerator instance = null;
        private IActivitiesStreamParser _activitiesStreamParser;
        private ActivitiesGenerator()
        {
            _activitiesStreamParser = ActivitiesStreamParser.Instance;
        }
        public static IActivitiesGenerator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ActivitiesGenerator();
                }
                return (IActivitiesGenerator)instance;
            }
        }
               
        public bool generate(string fileIn, string fileOut, int nTeams)
        {
            bool generated = false;
            try
            {
                IFileManager fileManager = new FileManager(fileIn, fileOut);

                generated = generate(nTeams, fileManager);

                fileManager.Dispose();
            }
            catch (ActivitiesGeneratorException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new ActivitiesGeneratorException("ActivitiesGenerator 1", e);
            }
            return generated;
        }

        private bool generate(int nTeams, IFileManager fileManager)
        {
            StreamReader reader = fileManager.getReader();
            StreamWriter writer = fileManager.getWriter();

            return _activitiesStreamParser.generate(reader, writer, nTeams);
        }
    }
}
