namespace Timetracker.BLL.Configuration
{
    public class JwtSettings
    {
        public string Secret { get; set; }

        public double ExpireDays { get; set; }
    }
}
