using AutoMapper;
using Core.Services.Form.Requests;
using Infrastructure.Form.Entity;
using Infrastructure.Form.Repository;
using MediatR;
using Shared.Result;

namespace Core.Services.Form.Commands
{
    public record AddEditFormCommand(FormRequest Form) : IRequest<Result<string>>
    {

    }

    internal class AddEditFormCommandHandler : IRequestHandler<AddEditFormCommand, Result<string>>
    {
        private readonly IMapper _mapper;
        private readonly IFormRepository _formRepository;


        public AddEditFormCommandHandler(IMapper mapper, IFormRepository formRepository)
        {
            _mapper = mapper;
            _formRepository = formRepository;
        }

        public async Task<Result<string>> Handle(AddEditFormCommand command, CancellationToken cancellationToken)
        {
            try
            {
                if (Convert.ToInt32(command.Form.Id) == 0)
                {
                    TemplateForms forms = new()
                    {
                        Name = command.Form.Name,
                        Description = command.Form.Description,
                        Ordinal = command.Form.Ordinal,
                        Status = true,
                        CreatedDate = DateTime.Now
                    };

                    var rtn = await _formRepository.Create(forms);

                    if (rtn == 0)
                    {
                        return await Result<string>.FailAsync("Failed to create form");
                    }
                    else
                    {
                        return await Result<string>.SuccessAsync(rtn.ToString(), "Form is created successfully");
                    }
                }
                else
                {
                    var existingObj = await _formRepository.GetById(Convert.ToInt32(command.Form.Id));

                    if (existingObj == null)
                    {
                        return await Result<string>.FailAsync("Failed to update form");
                    }
                    else
                    {
                        TemplateForms forms = new()
                        {
                            Id = command.Form.Id,
                            Name = command.Form.Name,
                            Description = command.Form.Description,
                            Ordinal = command.Form.Ordinal,
                            Status = existingObj.Status,
                            CreatedDate = existingObj.CreatedDate,
                            ModifiedDate = DateTime.Now
                        };

                        var rtn = await _formRepository.Update(forms);

                        if (rtn == 0)
                        {
                            return await Result<string>.FailAsync("Failed to Update form");
                        }
                        else
                        {
                            return await Result<string>.SuccessAsync(rtn.ToString(), "Form is Updated successfully");
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
