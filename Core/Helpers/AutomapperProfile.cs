using AutoMapper;
using Core.Services.ControlFields.Responses;
using Core.Services.Form.Responses;
using Core.Services.TemplateFields.Responses;
using Infrastructure.ControlType.Entity;
using Infrastructure.Field.Entity;
using Infrastructure.Form.Entity;
using Core.Services.FieldOptions.Responses;
using Infrastructure.FieldOptions.Entity;
using Core.Services.FieldOptions.Requests;
using Infrastructure.UserFormValue.Entity;
using Infrastructure.UserFormValue.Responses;
using Infrastructure.UserForm.Entity;
using Infrastructure.UserForm.Responses;

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


        }
    }
}