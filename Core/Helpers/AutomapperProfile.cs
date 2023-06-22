using AutoMapper;
//using Core.Services.Form.Requests;
using Core.Services.Form.Responses;
using Core.Services.TemplateFields.Responses;
using Infrastructure.Field.Entity;
using Infrastructure.Form.Entity;
//using Infrastructure.Form.Responses;
namespace Core.Helpers
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<TemplateForms, FormResponse>();
            CreateMap<TemplateFormFields, FieldResponse>();
        }
    }
}