namespace Timetracker.Entities.Data
{
    public class UserProjectsEntity
    {
        public int UserId { get; set; }

        public int ProjectId { get; set; }

        public UserEntity User { get; set; }

        public ProjectEntity Project { get; set; }
    }
}
