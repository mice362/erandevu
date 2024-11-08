using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using static Application.Features.BusinessPartners.Constants.BusinessPartnerOperationClaims;

namespace Application.Features.BusinessPartners.Queries.GetList;
public class GetListBusinessPartnerQuery : IRequest<GetListResponse<GetListBusinessPartnerListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListBusinessPartners({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetBusinessPartners";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListBusinessPartnerQueryHandler : IRequestHandler<GetListBusinessPartnerQuery, GetListResponse<GetListBusinessPartnerListItemDto>>
    {
        private readonly IBusinessPartnerRepository _businessPartnerRepository;
        private readonly IMapper _mapper;

        public GetListBusinessPartnerQueryHandler(IBusinessPartnerRepository businessPartnerRepository, IMapper mapper)
        {
            _businessPartnerRepository = businessPartnerRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListBusinessPartnerListItemDto>> Handle(GetListBusinessPartnerQuery request, CancellationToken cancellationToken)
        {
            IPaginate<BusinessPartner> clinics = await _businessPartnerRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            // S�NEM Foreach ile d�ndurunce  Ipaginat ekleme i�lemine izin vermiyor ,hata veriyor .

            for (int i = 0; i < clinics.Items.Count; i++)
            {
                clinics.Items[i].Name = clinics.Items[i].Name;
                clinics.Items[i].Phone = clinics.Items[i].Phone;
                clinics.Items[i].Address = clinics.Items[i].Address;
                clinics.Items[i].Email = clinics.Items[i].Email;
                clinics.Items[i].About = clinics.Items[i].About;
                clinics.Items[i].Logo = clinics.Items[i].Logo;
                clinics.Items[i].LogoName = clinics.Items[i].LogoName;
            }

            GetListResponse<GetListBusinessPartnerListItemDto> response = _mapper.Map<GetListResponse<GetListBusinessPartnerListItemDto>>(clinics);
            return response;
        }
    }
}
