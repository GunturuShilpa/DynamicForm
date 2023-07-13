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
                        CreatedBy = command.User.CreatedBy,
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
                    var existingObj = await _applicationUserRepository.GetById(Convert.ToInt32(command.User.Id));

                    if (existingObj == null)
                    {
                        return await Result<string>.FailAsync("Failed to update user");
                    }
                    else
                    {
                        Users user = new()
                        {
                            Id = command.User.Id,
                            UserName = command.User.UserName,
                            Email = command.User.Email,
                            RoleId = existingObj.RoleId,
                            PasswordHash = existingObj.PasswordHash,
                            PasswordSalt = existingObj.PasswordSalt,
                            Status = existingObj.Status,
                            CreatedBy = existingObj.CreatedBy,
                            CreatedDate = existingObj.CreatedDate,
                            ModifiedBy = 0,
                            ModifiedDate = DateTime.UtcNow
                        };

                        var rtn = await _applicationUserRepository.Update(user);

                        if (rtn == 0)
                        {
                            return await Result<string>.FailAsync("Failed to Update user");
                        }
                        else
                        {
                            return await Result<string>.SuccessAsync(rtn.ToString(), "User is Updated successfully");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return await Result<string>.FailAsync(ex.Message);
            }
        }
    }
}
