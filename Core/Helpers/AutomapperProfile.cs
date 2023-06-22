using AutoMapper;
using Core.Services.Form.Responses;
using Infrastructure.Form.Entity;
using Core.Services.ControlFields.Responses;
using Infrastructure.ControlType.Entity;

namespace Core.Helpers
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<TemplateForms, FormResponse>();
            CreateMap<ControlTypes, ControlTypesResponse>();
        }
    }
}
