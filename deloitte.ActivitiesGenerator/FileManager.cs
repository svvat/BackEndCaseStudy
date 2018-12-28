using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace deloitte.fileParser
{
    public interface IFileManager
    {
        StreamReader getReader();
        StreamWriter getWriter();
        void Dispose();
    } 

    public class FileManager : IFileManager, IDisposable
    {
        private string _inFileName = null;
        private string _outFileName = null;

        private StreamReader _reader;
        private StreamWriter _writer;

        private string getInFilename()
        {
            if (_inFileName == null)
            {
                _inFileName = ".\\activities.txt";
            }
            return _inFileName;
        }

        private string getOutFilename()
        {
            if (_outFileName == null)
            {
                _outFileName = String.Concat(Path.GetDirectoryName(getInFilename()), "\\schedule.txt");
            }
            return _outFileName;
        }

        public FileManager(string inFileName, string outFileName = null)
        {
            _inFileName = inFileName;
            _outFileName = outFileName;
        }

        public void Dispose()
        {
            if (_reader != null)
            {
                _reader.Close();
            }
            if (_writer != null)
            {
                _writer.Close();
            }
        }

        public StreamReader getReader()
        {
            try
            {
                return _getReader();
            }
            catch (ActivitiesGeneratorException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new ActivitiesGeneratorException("FileManager 1", e);
            }
        }

        public StreamWriter getWriter()
        {
            try
            {
                return _getWriter();
            }
            catch (ActivitiesGeneratorException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new ActivitiesGeneratorException("FileManager 2", e);
            }
        }

        private StreamReader _getReader()
        {
            if (_reader == null)
            {
                _reader = new StreamReader(getInFilename());
            }
            return _reader;
        }

        private StreamWriter _getWriter()
        {
            if (_writer == null)
            {
                _writer = new StreamWriter(getOutFilename());
            }
            return _writer;
        }
    }
}
