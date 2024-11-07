using NArchitecture.Core.Application.Dtos;

namespace Application.Features.DoctorSchedules.Queries.GetListByDoctorId;
public class GetListByDoctorIdDto : IDto
{
    public int Id { get; set; }

    public Guid DoctorID { get; set; }
    public string DoctorFirstName { get; set; }
    public string DoctorLastName { get; set; }

    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}
