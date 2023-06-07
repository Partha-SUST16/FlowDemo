namespace Models.Dtos;

public class CreatorDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNo { get; set; }
    public string Email { get; set; }
    public CreatorType Type { get; set; }
}