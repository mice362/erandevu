using Application.Features.Auth.Commands.Register.UserRegister;

namespace Application.Features.Auth.Commands.Register.PatientRegister;


public class PatientForRegisterDto : UserForRegisterDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
}








