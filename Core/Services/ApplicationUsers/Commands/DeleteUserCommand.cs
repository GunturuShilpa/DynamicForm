using Infrastructure.ApplicationUsers.Repository;
using MediatR;
using Shared.Result;

namespace Core.Services.ApplicationUsers.Commands
{

    public class DeleteUserCommand : IRequest<Result<string>>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
    }

    internal class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<string>>
    {
        private readonly IApplicationUserRepository _applicationUserRepository;


        public DeleteUserCommandHandler(IApplicationUserRepository applicationUserRepository)
        {
            _applicationUserRepository = applicationUserRepository;
        }
        public async Task<Result<string>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                return await Result<string>.FailAsync("Failed to delete user");
            }
            else
            {
                var rtn = await _applicationUserRepository.Delete(command.Id, command.UserId);
                if (rtn == 0)
                {
                    return await Result<string>.FailAsync("Failed to delete user");
                }
                else
                {
                    return await Result<string>.SuccessAsync("User deleted successfully");
                }
            }
        }
    }
}
