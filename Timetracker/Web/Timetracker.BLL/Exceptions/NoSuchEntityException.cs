namespace Timetracker.BLL.Exceptions
{

    public class NoSuchEntityException : BaseException
    {
        public NoSuchEntityException(string message, string prevMessage) : base(message, prevMessage)
        {
        }

        public NoSuchEntityException(string message) : base(message)
        {
        }
    }
}