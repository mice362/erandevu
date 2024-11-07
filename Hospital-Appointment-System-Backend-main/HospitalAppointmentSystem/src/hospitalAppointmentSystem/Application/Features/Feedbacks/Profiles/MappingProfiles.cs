using Application.Features.Feedbacks.Commands.Create;
using Application.Features.Feedbacks.Commands.Delete;
using Application.Features.Feedbacks.Commands.Update;
using Application.Features.Feedbacks.Queries.GetById;
using Application.Features.Feedbacks.Queries.GetList;
using Application.Features.Feedbacks.Queries.GetListByUser;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Feedbacks.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateFeedbackCommand, Feedback>();
        CreateMap<Feedback, CreatedFeedbackResponse>();

        CreateMap<UpdateFeedbackCommand, Feedback>();
        CreateMap<Feedback, UpdatedFeedbackResponse>();

        CreateMap<DeleteFeedbackCommand, Feedback>();
        CreateMap<Feedback, DeletedFeedbackResponse>();

        CreateMap<Feedback, GetByIdFeedbackResponse>();

        CreateMap<Feedback, GetListFeedbackListItemDto>();
        CreateMap<IPaginate<Feedback>, GetListResponse<GetListFeedbackListItemDto>>();

        CreateMap<Feedback, GetListByUserDto>();
        CreateMap<IPaginate<Feedback>, GetListResponse<GetListByUserDto>>();
    }
}