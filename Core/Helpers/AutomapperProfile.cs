using AutoMapper;
using Core.Services.ControlFields.Responses;
using Core.Services.Form.Responses;
using Core.Services.TemplateFields.Responses;
using Infrastructure.ControlType.Entity;
using Infrastructure.Field.Entity;
using Infrastructure.Form.Entity;

namespace Core.Helpers
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<TemplateForms, FormResponse>();
            CreateMap<ControlTypes, ControlTypesResponse>();
            CreateMap<TemplateFormFields, FieldResponse>();
        }
    }
}