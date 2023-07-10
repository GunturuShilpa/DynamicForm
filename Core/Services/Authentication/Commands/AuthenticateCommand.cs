using AutoMapper;
using Core.Services.Authentication.Requests;
using Core.Services.Authentication.Responses;
using Infrastructure.ApplicationUsers.Entity;
using Infrastructure.ApplicationUsers.Repository;
using MediatR;
using Shared.Result;

namespace Core.Services.Authentication.Commands
{
    public record AuthenticateCommand(LoginRequest Login) : IRequest<Result<SessionObject>>
    {

    }

    internal class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, Result<SessionObject>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationUserRepository _userRepository;



        public AuthenticateCommandHandler(IApplicationUserRepository userRepository
            , IMapper mapper
            )
        {
            _userRepository = userRepository;
            _mapper = mapper;

        }

        public async Task<Result<SessionObject>> Handle(AuthenticateCommand command, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(command.Login.UserName) || string.IsNullOrEmpty(command.Login.Password))
                {
                    return await Result<SessionObject>.FailAsync("Login failed. Incorrect credentials.");
                }

                Users user = await _userRepository.AuthenticateAsync(command.Login.UserName);
                if (user == null)
                {
                    return await Result<SessionObject>.FailAsync("Login failed. Incorrect credentials.");
                }
                else if (!VerifyPasswordHash(command.Login.Password, user.PasswordHash, user.PasswordSalt))
                {
                    return await Result<SessionObject>.FailAsync("Login failed. Incorrect credentials.");
                }
                else
                {
                    AuthResponse result = new AuthResponse();
                    SessionObject sessionObj = new SessionObject(); 

                    sessionObj.roleId = user.RoleId;
                    sessionObj.userId = user.Id;
                    sessionObj.userName = user.UserName;
                    sessionObj.email = user.Email;
                  

                    return await Result<SessionObject>.SuccessAsync(sessionObj);
                }
            }
            catch (Exception ex)
            {
                return await Result<SessionObject>.FailAsync(ex.Message);
            }
        }

        //Private methods
        internal bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
