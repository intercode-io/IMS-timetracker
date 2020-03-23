namespace Timetracker.Models.Data
{
    public class UserModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhotoUrl { get; set; }

        public string Token { get; set; }
    }
}