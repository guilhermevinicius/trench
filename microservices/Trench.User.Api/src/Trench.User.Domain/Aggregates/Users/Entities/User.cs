using Trench.User.Domain.SeedWorks;

namespace Trench.User.Domain.Aggregates.Users.Entities;

public class User : Entity
{
    public string? PictureUrl { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Username { get; private set; }
    public string IdentityId { get; private set; }
    public DateTime Birthdate { get; private set; }
    public string? Bio { get; private set; }
    public bool IsActive { get; private set; } = true;
    public bool IsPublic { get; private set; } = true;

    private User(string firstName, string lastName, string email, string username, DateTime birthdate)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Username = username;
        Birthdate = birthdate;
    }

    private User()
    { }

    public static User Create(string firstName, string lastName, string email, string username,
        DateTime birthday)
    {
        return new User(firstName, lastName, email, username, birthday);
    }

    public void UpdateBio(string bio)
    {
        Bio = bio;
    }

    public void UpdatePictureUrl(string pictureUrl)
    {
        PictureUrl = pictureUrl;
    }

    public void SetIdentityId(string identityId)
    {
        IdentityId = identityId;
    }
    
    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void MakePublic()
    {
        IsPublic = true;
    }

    public void MakePrivate()
    {
        IsPublic = false;
    }
}