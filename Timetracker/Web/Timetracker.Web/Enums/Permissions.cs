namespace IMS_Timetracker.Enums
{
    public enum Permissions
    {
        NotSet = 0, //error condition

        ProjectAll = 0x10,
        ProjectRead = 0x11,
        ProjectCreate = 0x12,
        ProjectUpdate = 0x13,
        ProjectDelete = 0x14,
        ProjectAddMember = 0x15,
        ProjectLogTime = 0x16,

        TimelogAll = 0x20,
        TimelogRead = 0x21,
        TimelogCreate = 0x22,
        TimelogUpdate = 0x23,
        TimelogDelete = 0x24,
    }
}