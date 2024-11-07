using Application.Features.Patients.Constants;
using Application.Features.Users.Constants;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Security.Hashing;
using static Application.Features.Users.Constants.UsersOperationClaims;

namespace Application.Features.Users.Commands.ChangePassword;
public class ChangePasswordCommand : IRequest<ChangePasswordResponse>
{

    //public Guid Id { get; set; }
    public string Phone { get; set; }

    public string Email { get; set; }
    public string Password { get; set; }

    public string[] Roles => new[] { Write, UsersOperationClaims.Update, PatientsOperationClaims.Update };

    public ChangePasswordCommand()
    {
        Phone = string.Empty;
        Email = string.Empty;
        Password = string.Empty;
    }

    public ChangePasswordCommand(string phoneNumber, string email, string newPassword)
    {
        Phone = phoneNumber;
        Email = email;
        Password = newPassword;
    }
    //doktoru eklersiniz gerekirse
    // public string[] Roles => new[] { Admin, Write, UsersOperationClaims.Update, PatientsOperationClaims.Update };

    public class changePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ChangePasswordResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserBusinessRules _userBusinessRules;

        public changePasswordCommandHandler(IUserRepository userRepository, IMapper mapper, UserBusinessRules userBusinessRules)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userBusinessRules = userBusinessRules;
        }

        public async Task<ChangePasswordResponse> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetAsync(
                predicate: u => u.Phone.Equals(request.Phone) && u.Email.Equals(request.Email),
                cancellationToken: cancellationToken
            );

            if (user == null)
            {
                throw new Exception("Kullanıcı bulunamadı");
            }

            if (string.IsNullOrEmpty(request.Password))
            {
                throw new Exception("Şifreyi boş bırakamazsınız");
            }

            HashingHelper.CreatePasswordHash(request.Password, passwordHash: out byte[] newPasswordHash, passwordSalt: out byte[] newPasswordSalt);

            user!.PasswordHash = newPasswordHash;
            user.PasswordSalt = newPasswordSalt;
            await _userRepository.UpdateAsync(user);

            ChangePasswordResponse response = _mapper.Map<ChangePasswordResponse>(user);
            return response;
        }
    }
}