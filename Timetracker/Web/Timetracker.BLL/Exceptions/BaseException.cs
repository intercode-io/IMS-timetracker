using System;

namespace Timetracker.BLL.Exceptions
{
    public class BaseException : Exception
    {
        public BaseException(string message) : base(message)
        {

        }

        public BaseException(string message, string previousError) : base(message + "\nError: " + previousError)
        {

        }
    }
}