using Infrastructure.Field.Repository;
using MediatR;
using Shared.Result;

namespace Core.Services.TemplateFields.Commands
{
    public class DeleteFieldCommand : IRequest<Result<string>>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
    }

    internal class DeleteFieldCommandHandler : IRequestHandler<DeleteFieldCommand, Result<string>>
    {
        private readonly IFieldRepository _fieldRepository;
        public DeleteFieldCommandHandler(IFieldRepository fieldRepository)
        {
            _fieldRepository = fieldRepository;
        }
        public async Task<Result<string>> Handle(DeleteFieldCommand command, CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                return await Result<string>.FailAsync("Failed to delete template field");
            }
            else
            {
                var rtn = await _fieldRepository.Delete(command.Id, command.UserId);
                if (rtn == 0)
                {
                    return await Result<string>.FailAsync("Failed to delete template field");
                }
                else
                {
                    return await Result<string>.SuccessAsync("Template field deleted successfully");
                }
            }
        }
    }
}
