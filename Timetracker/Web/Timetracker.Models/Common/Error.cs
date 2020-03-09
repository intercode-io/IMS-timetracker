using System;

namespace Timetracker.Models.Data
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