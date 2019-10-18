using System;

namespace IMS_Timetracker.Dto
{
    public class Error
    {
        public string Message { get; set; }

        public string Type { get; set; }
        public Error(string message, Type type)
        {
            Message = message;
            Type = type.Name;
        }
    }
}