using NArchitecture.Core.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BusinessPartners.Commands.Delete;
public class DeletedBusinessPartnerResponse : IResponse
{
    public int Id { get; set; }
}