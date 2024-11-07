namespace Domain.Entities;
public class Patient : User
{
    public Patient()
    {
    }

    public Patient(Guid id, int age, double height, double weight, string bloodGroup)
    {
        Id = id;
        Age = age;
        Height = height;
        Weight = weight;
        BloodGroup = bloodGroup;
    }

    public int? Age { get; set; }
    public double? Height { get; set; }
    public double? Weight { get; set; }
    public string? BloodGroup { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();



}
