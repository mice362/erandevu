using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;
public class Clinic : Entity<int>
{
    public Clinic()
    {
    }

    public Clinic(int id, string name, string phone, string address, string email, string about)
    {
        Id = id;
        Name = name;
        Phone = phone;
        Address = address;
        Email = email;
        About = about;
    }

    public string Name { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string About { get; set; }
    public byte[] Logo { get; set; }
    public string LogoName { get; set; }
}
