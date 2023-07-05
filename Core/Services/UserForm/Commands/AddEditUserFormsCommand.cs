using AutoMapper;
using Core.Services.UserForm.Requests;
using Core.Services.UserFormValue.Commands;
using Core.Services.UserFormValue.Requests;
using Infrastructure.UserForm.Entity;
using Infrastructure.UserForm.Repository;
using Infrastructure.UserFormValue.Entity;
using Infrastructure.UserFormValue.Repository;
using MediatR;
using Shared.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.UserForm.Commands
{
    public record AddEditUserFormsCommand(UserFormsRequest UserFormValues) : IRequest<Result<string>>
    {
    }
    internal class AddEditUserFormsCommandHandler : IRequestHandler<AddEditUserFormsCommand, Result<string>>
    {
        private readonly IMapper _mapper;
        private readonly IUserFormRepository _userFormRepository;

        public AddEditUserFormsCommandHandler(IMapper mapper, IUserFormRepository userFormRepository)
        {
            _mapper = mapper;
            _userFormRepository = userFormRepository;
        }

        public async Task<Result<string>> Handle(AddEditUserFormsCommand command, CancellationToken cancellationToken)
        {
            try
            {
                if (Convert.ToInt32(command.UserFormValues.Id) == 0)
                {
                    UserForms userForms = new()
                    {
                        Id = command.UserFormValues.Id,
                        TemplateFormId = command.UserFormValues.TemplateFormId,                        
                        CreatedDate = DateTime.UtcNow,
                    };

                    var rtn = await _userFormRepository.Create(userForms);

                    if (rtn == 0)
                    {
                        return await Result<string>.FailAsync("Failed to create User Form");
                    }
                    else
                    {
                        return await Result<string>.SuccessAsync(rtn.ToString(), "User Form is created successfully");
                    }
                }
                else
                {
                    return await Result<string>.FailAsync("Failed to create User Form");
                }
            }
            catch (Exception ex)
            {
                return await Result<string>.FailAsync(ex.Message);
            }
        }
    }
}
