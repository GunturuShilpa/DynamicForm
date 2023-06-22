using AutoMapper;
using Core.Services.Form.Commands;
using Core.Services.Form.Requests;
using Core.Services.TemplateFields.Requests;
using Infrastructure.Field.Repository;
using Infrastructure.Form.Entity;
using Infrastructure.Form.Repository;
using MediatR;
using Shared.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Field.Entity;

namespace Core.Services.TemplateFields.Commands
{
    public record AddEditFieldCommand(FieldRequest Field) : IRequest<Result<string>>
    {

    }

    internal class AddEditFieldCommandHandler : IRequestHandler<AddEditFieldCommand, Result<string>>
    {
        private readonly IMapper _mapper;
        private readonly IFieldRepository _fieldRepository;


        public AddEditFieldCommandHandler(IMapper mapper, IFieldRepository fieldRepository)
        {
            _mapper = mapper;
            _fieldRepository = fieldRepository;
        }

        public async Task<Result<string>> Handle(AddEditFieldCommand command, CancellationToken cancellationToken)
        {
            try
            {
                if (Convert.ToInt32(command.Field.Id) == 0)
                {
                    TemplateFormFields fields = new()
                    {
                        Name = command.Field.Name,
                        ControlId = command.Field.ControlId,                        
                        //Status = true
                    };
                    var rtn = await _fieldRepository.Create(fields);

                    if (rtn == 0)
                    {
                        return await Result<string>.FailAsync("Failed to create field");
                    }
                    else
                    {
                        return await Result<string>.SuccessAsync(rtn.ToString(), "field is created successfully");
                    }
                }
                else
                {
                    return await Result<string>.FailAsync("Failed to create field");
                }

            }
            catch (Exception ex)
            {
                return await Result<string>.FailAsync(ex.Message);
            }
        }
    }
}
