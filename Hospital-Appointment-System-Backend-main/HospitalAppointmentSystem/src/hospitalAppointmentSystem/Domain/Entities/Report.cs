using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;
public class Report : Entity<int>
{
    public Report()
    {
    }

    public Report(int id, int appointmentID, string text)
    {
        Id = id;
        AppointmentID = appointmentID;
        Text = text;
    }

    public int AppointmentID { get; set; }
    public string Text { get; set; }

    public virtual Appointment? Appointment { get; set; }
}