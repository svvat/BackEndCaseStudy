using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using deloitte.fileParser;

namespace deloitte
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string inFilename = args.Length > 0 ? args[0] : null;
                int nTeams = args.Length > 1 ? int.Parse(args[1]) : 2;
                string outFilename = args.Length > 2 ? args[2] : null;

                if(ActivitiesGenerator.Instance.generate(inFilename, outFilename, nTeams))
                {
                    Console.WriteLine("Application executed correctly");
                }
                else
                {
                    Console.WriteLine("Application execution failed");
                }

            }
            catch (ActivitiesGeneratorException e)
            {
                reportError("Runtime Error executing the ActivitiesGenerator", e);
            }
            catch (Exception e)
            {
                reportError("Error executing the ActivitiesGenerator", e);
            }
        }

        private static void reportError(string msg, Exception e)
        {
            
            Console.WriteLine(msg);
            Console.WriteLine("Exception Message reads:" + e.Message);
            Console.WriteLine("Either drag and drop the activities file on the exe file, or");
            Console.WriteLine("execute from the command line, thus:");
            Console.WriteLine("> ActivitiesGenerator \"[drive:][path]\\activities.txt\" \"[drive:][path][output filename]\"");
        }
    }
}
