namespace Modules.DatabaseModule.Tables;

public class User : BaseEntity
{
    public int UserId { get; set; }
    public string? Username { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Password { get; set; }
}