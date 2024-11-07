using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Clinics.Queries.GetList;
public class GetListClinicListItemDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string About { get; set; }
}
