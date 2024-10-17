using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Appointments.Queries.GetList;

public class GetListAppointmentListItemDto : IDto
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public bool Status { get; set; }


    public Guid DoctorID { get; set; }
    public string DoctorFirstName { get; set; }
    public string DoctorLastName { get; set; }
    public string DoctorTitle { get; set; }
    public string BranchName { get; set; }

    public Guid PatientID { get; set; }
    public string PatientFirstName { get; set; } //burda gerekli konfig�rasyonu yap
    public string PatientLastName { get; set; }
    public string PatientNationalIdentity { get; set; }

    

}