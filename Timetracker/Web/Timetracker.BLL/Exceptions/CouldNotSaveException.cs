namespace Timetracker.BLL.Exceptions
{

    public class CouldNotSaveException : BaseException
    {
        public CouldNotSaveException(string message, string prevMessage) : base(message, prevMessage)
        {
        }
    }
}