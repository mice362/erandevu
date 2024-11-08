using Application.Features.BusinessPartners.Commands.Create;
using Application.Features.BusinessPartners.Commands.Delete;
using Application.Features.BusinessPartners.Commands.Update;
using Application.Features.BusinessPartners.Queries.GetList;
using Microsoft.AspNetCore.Mvc;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BusinessPartnerController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedBusinessPartnerResponse>> Add([FromBody] CreateBusinessPartnerCommand command)
    {
        CreatedBusinessPartnerResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(Delete), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedBusinessPartnerResponse>> Update([FromBody] UpdateBusinessPartnerCommand command)
    {
        UpdatedBusinessPartnerResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedBusinessPartnerResponse>> Delete([FromRoute] int id)
    {
        DeleteBusinessPartnerCommand command = new() { Id = id };

        DeletedBusinessPartnerResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListBusinessPartnerQuery>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListBusinessPartnerQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListBusinessPartnerListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}
