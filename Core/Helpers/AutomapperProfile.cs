using AutoMapper;
using Core.Services.ControlFields.Responses;
using Core.Services.Form.Responses;
using Core.Services.TemplateFields.Responses;
using Infrastructure.ControlType.Entity;
using Infrastructure.Field.Entity;
using Infrastructure.Form.Entity;
using Core.Services.FieldOptions.Responses;
using Infrastructure.FieldOptions.Entity;
using Core.Services.UserFormValue.Responses;
using Core.Services.UserForm.Responses;
using Infrastructure.UserFormValue.Entity;
using Infrastructure.UserForm.Entity;
using Core.Services.ApplicationUsers.Responses;
using Infrastructure.ApplicationUsers.Entity;

namespace Core.Helpers
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<TemplateForms, FormResponse>();
            CreateMap<ControlTypes, ControlTypesResponse>();
            CreateMap<TemplateFormFields, FieldResponse>();
            CreateMap<FormFieldOptions, FieldOptionsResponse>();
            CreateMap<UserFormValues, UserFormValuesResponse>();
            CreateMap<UserForms, UserFormsResponse>();
            CreateMap<Users, UserResponse>();

        }
    }
}