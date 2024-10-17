using Application.Features.Appointments.Commands.Create;
using Application.Features.Appointments.Commands.Delete;
using Application.Features.Appointments.Commands.Update;
using Application.Features.Appointments.Queries.GetById;
using Application.Features.Appointments.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Appointments.Queries.GetByPatientId;
using Application.Features.Appointments.Queries.GetListByDoctor;
using Application.Features.Appointments.Queries.GetListByDoctorId;
using Domain.Entities;
using Application.Features.Patients.Queries.GetById;
using Nest;
using Application.Features.Reports.Queries.GetById;
using Application.Features.Appointments.Queries.GetListByDoctorDate;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentsController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedAppointmentResponse>> Add([FromBody] CreateAppointmentCommand command)
    {
        CreatedAppointmentResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UpdatedAppointmentResponse>> Update(int id, [FromBody] UpdateAppointmentCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        UpdatedAppointmentResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedAppointmentResponse>> Delete([FromRoute] int id)
    {
        DeleteAppointmentCommand command = new() { Id = id };

        DeletedAppointmentResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdAppointmentResponse>> GetById([FromRoute] int id)
    {
        GetByIdAppointmentQuery query = new() { Id = id };

        GetByIdAppointmentResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListAppointmentQuery>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListAppointmentQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListAppointmentListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }



    [HttpGet("getByPatientId")]
    public async Task<IActionResult> GetListByIPatient([FromQuery] PageRequest pageRequest, [FromQuery] Guid patientId)
    {
        GetListByPatientQuery query = new() { PageRequest = pageRequest, PatientId = patientId };
        GetListResponse<GetListByPatientDto> response = await Mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("getByDoctorId")]
    public async Task<IActionResult> GetListByIDoctor([FromQuery] PageRequest pageRequest, [FromQuery] Guid doctorId)
    {
        GetListByDoctorQuery query = new() { PageRequest = pageRequest, DoctorId = doctorId };
        GetListResponse<GetListByDoctorDto> response = await Mediator.Send(query);
        return Ok(response);
    }




    [HttpGet("getByDoctorDate")]
    public async Task<IActionResult> GetListByDoctorDate([FromQuery] PageRequest pageRequest, [FromQuery] Guid doctorId, [FromQuery] DateOnly date)
    {
        GetListByDoctorDateQuery query = new() { PageRequest = pageRequest, DoctorId = doctorId, Date = date };
        GetListResponse<GetListByDoctorDateDto> response = await Mediator.Send(query);
        return Ok(response);
    }

}