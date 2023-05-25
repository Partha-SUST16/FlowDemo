namespace Models;

public enum CreatorType
{
    INDIVIDUAL,
    ORGANIZATION
}
public class Creator
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNo { get; set; }
    public string Email { get; set; }
    public CreatorType Type { get; set; }
}