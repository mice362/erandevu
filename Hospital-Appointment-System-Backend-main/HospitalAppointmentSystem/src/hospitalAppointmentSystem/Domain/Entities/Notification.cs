using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;
public class Notification : Entity<int>
{
    public Notification()
    {
    }

    public Notification(int id, int appointmentID, string message, bool emailStatus, bool smsStatus)
    {
        Id = id;
        AppointmentID = appointmentID;
        Message = message;
        EmailStatus = emailStatus;
        SmsStatus = smsStatus;
    }


    public int AppointmentID { get; set; }
    public string Message { get; set; }
    public bool EmailStatus { get; set; }
    public bool SmsStatus { get; set; }

    public virtual Appointment? Appointment { get; set; }

}

