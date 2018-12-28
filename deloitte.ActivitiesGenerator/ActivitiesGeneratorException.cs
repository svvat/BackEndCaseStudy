using System;
using System.Runtime.InteropServices;

namespace deloitte.fileParser
{
    interface IParseException : _Exception
    {
        string getMessage();
        Exception getParent();
    }

    public class ActivitiesGeneratorException : Exception, IParseException
    {

        private string _msg;
        private Exception _parent;
        public ActivitiesGeneratorException(string msg, Exception parent = null)
        {
            _msg = msg;
            _parent = parent;
        }

        public string getMessage()
        {
            return _msg;
        }

        public Exception getParent()
        {
            return _parent;
        }
    }
}
