using NArchitecture.Core.Application.Responses;

namespace Application.Features.BusinessPartners.Commands.Update;
public class UpdatedBusinessPartnerResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string About { get; set; }
    public byte[] Logo { get; set; }
    public string LogoName { get; set; }
}