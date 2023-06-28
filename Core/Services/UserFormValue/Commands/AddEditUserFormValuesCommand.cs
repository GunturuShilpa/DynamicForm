using AutoMapper;
using Core.Services.UserFormValue.Requests;
using Infrastructure.UserFormValue.Entity;
using Infrastructure.UserFormValue.Repository;
using MediatR;
using Shared.Result;

namespace Core.Services.UserFormValue.Commands
{
    public record AddEditUserFormValuesCommand(UserFormValuesRequest UserFormValues) : IRequest<Result<string>>
    {

    }
    internal class AddEditUserFormValuesCommandHandler : IRequestHandler<AddEditUserFormValuesCommand, Result<string>>
    {
        private readonly IMapper _mapper;
        private readonly IUserFormValuesRepository _userFormRepository;

        public AddEditUserFormValuesCommandHandler(IMapper mapper, IUserFormValuesRepository userFormRepository)
        {
            _mapper = mapper;
            _userFormRepository = userFormRepository;
        }

        public async Task<Result<string>> Handle(AddEditUserFormValuesCommand command, CancellationToken cancellationToken)
        {
            try
            {
                if (Convert.ToInt32(command.UserFormValues.Id) == 0)
                {
                    UserFormValues userFormValues = new()
                    {
                        Id = command.UserFormValues.Id,
                        TemplateFormId = command.UserFormValues.TemplateFormId,
                        TemplateFormFieldId = command.UserFormValues.TemplateFormFieldId,
                        FieldValue = command.UserFormValues.FieldValue,
                    };

                    var rtn = await _userFormRepository.Create(userFormValues);

                    if (rtn == 0)
                    {
                        return await Result<string>.FailAsync("Failed to insert User Form values");
                    }
                    else
                    {
                        return await Result<string>.SuccessAsync(rtn.ToString(), "User details submitted successfully");
                    }
                }               
                else
                {
                    return await Result<string>.FailAsync("Failed to insert User Form values");
                }
            }
            catch (Exception ex)
            {
                return await Result<string>.FailAsync(ex.Message);
            }
        }
    }
}
