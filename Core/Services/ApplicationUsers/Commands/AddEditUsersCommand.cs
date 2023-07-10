using AutoMapper;
using Core.Services.ApplicationUsers.Requests;
using Infrastructure.ApplicationUsers.Entity;
using Infrastructure.ApplicationUsers.Repository;
using MediatR;
using Shared;
using Shared.Result;

namespace Core.Services.ApplicationUsers.Commands
{
    public record AddEditUsersCommand(CreateUserRequest User) : IRequest<Result<string>>
    {

    }

    internal class AddEditUsersCommandHandler : IRequestHandler<AddEditUsersCommand, Result<string>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationUserRepository _applicationUserRepository;


        public AddEditUsersCommandHandler(IMapper mapper, IApplicationUserRepository applicationUserRepository)
        {
            _mapper = mapper;
            _applicationUserRepository = applicationUserRepository;
        }

        public async Task<Result<string>> Handle(AddEditUsersCommand command, CancellationToken cancellationToken)
        {
            try
            {
               
                if (Convert.ToInt32(command.User.Id) == 0)
                {
                    byte[] passwordHash, passwordSalt;
                    PasswordHash.CreatePasswordHash(command.User.Password, out passwordHash, out passwordSalt);
                    Users user = new()
                    {
                        UserName = command.User.UserName,
                        Email = command.User.Email,
                        RoleId = command.User.RoleId,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        Status = 1,
                        CreatedBy = 0,
                        CreatedDate = DateTime.UtcNow
                    };

                    var rtn = await _applicationUserRepository.Create(user);

                    if (rtn == 0)
                    {
                        return await Result<string>.FailAsync("Failed to create user");
                    }
                    else
                    {
                        return await Result<string>.SuccessAsync(rtn.ToString(), "User is created successfully");
                    }
                }
                else
                {
                    return await Result<string>.SuccessAsync("", "User is created successfully");
                }
            }
            catch (Exception ex)
            {
                return await Result<string>.FailAsync(ex.Message);
            }
        }
    }
}
