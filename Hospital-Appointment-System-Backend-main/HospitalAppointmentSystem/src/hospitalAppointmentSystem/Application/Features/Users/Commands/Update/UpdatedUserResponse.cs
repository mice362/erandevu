using NArchitecture.Core.Application.Responses;

namespace Application.Features.Users.Commands.Update;

public class UpdatedUserResponse : IResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string NationalIdentity { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool Status { get; set; }

    public UpdatedUserResponse()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        DateOfBirth = new DateOnly(1, 1, 1); // Varsay�lan olarak 1 Ocak 0001
        NationalIdentity = string.Empty;
        Phone = string.Empty;
        Address = string.Empty;
        Email = string.Empty;
        Password = string.Empty;
        Status = true;
    }

    public UpdatedUserResponse(Guid �d, string firstName, string lastName, DateOnly dateOfBirth, string nationalIdentity, string phone, string address, string email, string password, bool status)
    {
        Id = �d;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        NationalIdentity = nationalIdentity;
        Phone = phone;
        Address = address;
        Email = email;
        Password = password;
        Status = status;
    }
}
