namespace Timetracker.BLL.Exceptions
{
    public class UnsupportedDomainException : BaseException
    {
        public UnsupportedDomainException(string message) : base(message)
        {
        }

        public UnsupportedDomainException(string message, string prevMessage) : base(message, prevMessage)
        {
        }
    }
}
