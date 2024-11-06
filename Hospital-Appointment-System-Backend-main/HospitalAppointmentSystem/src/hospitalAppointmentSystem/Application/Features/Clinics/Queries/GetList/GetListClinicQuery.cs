using Application.Features.Doctors.Queries.GetList;
using Application.Services.Encryptions;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using static Application.Features.Clinics.Constants.ClinicsOperationClaims;

namespace Application.Features.Clinics.Queries.GetList;
public class GetListClinicQuery : IRequest<GetListResponse<GetListClinicListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListClinics({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetClinics";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListClinicQueryHandler : IRequestHandler<GetListClinicQuery, GetListResponse<GetListClinicListItemDto>>
    {
        private readonly IClinicRepository _clinicRepository;
        private readonly IMapper _mapper;

        public GetListClinicQueryHandler(IClinicRepository clinicRepository, IMapper mapper)
        {
            _clinicRepository = clinicRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListClinicListItemDto>> Handle(GetListClinicQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Clinic> clinics = await _clinicRepository.GetListAsync(
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
            }


            GetListResponse<GetListClinicListItemDto> response = _mapper.Map<GetListResponse<GetListClinicListItemDto>>(clinics);
            return response;
        }
    }
}
