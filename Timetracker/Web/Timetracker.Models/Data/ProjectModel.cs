using System.Collections.Generic;

namespace Timetracker.Models.Data
{
    public class ProjectModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Color { get; set; }

        public List<UserModel> Members { get; set; }
    }
}