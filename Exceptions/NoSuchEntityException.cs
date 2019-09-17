using System;

namespace IMS_Timetracker.Exceptions
{

    public class NoSuchEntityException : BaseException 
    {
        public NoSuchEntityException(string message,  string prevMessage) : base(message, prevMessage)
        {
        }
    }
}