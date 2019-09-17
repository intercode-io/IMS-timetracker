using System;

namespace IMS_Timetracker.Exceptions
{

    public class CouldNotSaveException : BaseException 
    {
        public CouldNotSaveException(string message, string prevMessage) : base(message, prevMessage)
        {
        }
    }
}