using Application.Features.Clinics.Commands.Create;
using Application.Features.Clinics.Commands.Delete;
using Application.Features.Clinics.Commands.Update;
using Application.Features.Doctors.Commands.Create;
using Application.Features.Doctors.Commands.Delete;
using Application.Features.Doctors.Commands.Update;
using Application.Features.Doctors.Queries.GetById;
using Application.Features.Doctors.Queries.GetList;
using Microsoft.AspNetCore.Mvc;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClinicController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedClinicResponse>> Add([FromBody] CreateClinicCommand command)
    {
        CreatedClinicResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(Delete), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedClinicResponse>> Update([FromBody] UpdateClinicCommand command)
    {
        UpdatedClinicResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedClinicResponse>> Delete([FromRoute] int id)
    {
        DeleteClinicCommand command = new() { Id = id };

        DeletedClinicResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListDoctorQuery>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListDoctorQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListDoctorListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}
