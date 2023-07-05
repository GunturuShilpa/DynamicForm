using Infrastructure.Form.Repository;
using MediatR;
using Shared.Enum;
using Shared.Result;

namespace Core.Services.Form.Commands
{
    public class PublishFormCommand : IRequest<Result<string>>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
    }

    internal class PublishFormCommandHandler : IRequestHandler<PublishFormCommand, Result<string>>
    {
        private readonly IFormRepository _formRepository;


        public PublishFormCommandHandler(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }
        public async Task<Result<string>> Handle(PublishFormCommand command, CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                return await Result<string>.FailAsync("Failed to publish template form");
            }
            else
            {
                string sql = String.Format("Status={0} ,ModifiedDate='{1}',ModifiedBy={2} where  Id = {3}",
                          (int)FormAccountStatus.Published, DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"), 0, command.Id);

                var rtn = await _formRepository.UpdateByQuery(sql);
                if (rtn == 0)
                {
                    return await Result<string>.FailAsync("Failed to publish template form");
                }
                else
                {
                    return await Result<string>.SuccessAsync("Template form is published successfully");
                }
            }
        }
    }
}
