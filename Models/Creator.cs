using Models.Primitives;

namespace Models;

public enum CreatorType
{
    INDIVIDUAL,
    ORGANIZATION
}
public class Creator : Entity
{
    public string FirstName { get; protected set; }
    public string LastName { get;protected set; }
    public string PhoneNo { get; protected set; }
    public string Email { get; protected set; }
    public CreatorType Type { get; protected set; }

    public Creator( string firstName, string lastName, string email) : base(email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Type = CreatorType.INDIVIDUAL;
    }

    public void UpdatePhoneNo(string phoneNo)
    {
        PhoneNo = phoneNo;
    }

    public void UpdateCreatorType(CreatorType updatedType)
    {
        Type = updatedType;
    }
}