using NArchitecture.Core.Application.Responses;

namespace Application.Features.Clinics.Commands.Delete;

public class DeletedClinicResponse : IResponse
{
    public int Id { get; set; }
}