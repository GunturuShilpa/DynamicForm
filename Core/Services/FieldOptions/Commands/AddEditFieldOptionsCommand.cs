using AutoMapper;
using Core.Services.FieldOptions.Requests;
using Infrastructure.FieldOptions.Entity;
using Infrastructure.FieldOptions.Repository;
using MediatR;
using Shared.Result;

namespace Core.Services.FieldOptions.Commands
{
    public record AddEditFieldOptionsCommand(FieldOptionsRequest Field) : IRequest<Result<string>>
    {

    }
    internal class AddEditFieldOptionsCommandHandler : IRequestHandler<AddEditFieldOptionsCommand, Result<string>>
    {
        private readonly IMapper _mapper;
        private readonly IFormFieldOptionsRepository _fieldOptionsRepository;

        public AddEditFieldOptionsCommandHandler(IMapper mapper, IFormFieldOptionsRepository fieldOptionsRepository)
        {
            _mapper = mapper;
            _fieldOptionsRepository = fieldOptionsRepository;
        }

        public async Task<Result<string>> Handle(AddEditFieldOptionsCommand command, CancellationToken cancellationToken)
        {
            try
            {
                if (Convert.ToInt32(command.Field.Id) == 0)
                {
                    FormFieldOptions fields = new()
                    {
                        Id = command.Field.Id,
                        TemplateFormFieldId = command.Field.TemplateFormFieldId,
                        OptionValue = command.Field.OptionValue,                        
                        Status = true
                    };

                    var rtn = await _fieldOptionsRepository.Create(fields);

                    if (rtn == 0)
                    {
                        return await Result<string>.FailAsync("Failed to create field option");
                    }
                    else
                    {
                        return await Result<string>.SuccessAsync(rtn.ToString(), "Field option is created successfully");
                    }
                }

                else if (Convert.ToInt32(command.Field.Id) != 0)
                {
                    FormFieldOptions fields = new()
                    {
                        TemplateFormFieldId = command.Field.TemplateFormFieldId,
                        OptionValue = command.Field.OptionValue,
                    };

                    var rtn = await _fieldOptionsRepository.Update(fields);

                    if (rtn == 0)
                    {
                        return await Result<string>.FailAsync("Failed to update field option");
                    }
                    else
                    {
                        return await Result<string>.SuccessAsync(rtn.ToString(), "Field option is updated successfully");
                    }
                }
                else
                {
                    return await Result<string>.FailAsync("Failed to create field option");
                }
            }
            catch (Exception ex)
            {
                return await Result<string>.FailAsync(ex.Message);
            }
        }
    }
}
