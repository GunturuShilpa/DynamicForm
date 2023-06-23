using Infrastructure.Form.Repository;
using MediatR;
using Shared.Result;

namespace Core.Services.Form.Commands
{
    public class DeleteFormCommand : IRequest<Result<string>>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
    }

    internal class DeleteFormCommandHandler : IRequestHandler<DeleteFormCommand, Result<string>>
    {
        private readonly IFormRepository _formRepository;


        public DeleteFormCommandHandler(IFormRepository formRepository)
        {
            _formRepository = formRepository;
         }
        public async Task<Result<string>> Handle(DeleteFormCommand command, CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                return await Result<string>.FailAsync("Failed to delete template form");
            }
            else
            {
                var rtn = await _formRepository.Delete(command.Id);
                if (rtn == 0)
                {
                    return await Result<string>.FailAsync("Failed to delete template form");
                }
                else
                { 
                    return await Result<string>.SuccessAsync("Template form deleted successfully"); 
                }
            }
        }
    }
}
