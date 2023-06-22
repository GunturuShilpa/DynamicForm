using AutoMapper;
using Core.Services.Form.Responses;
using Infrastructure.Form.Entity;

namespace Core.Helpers
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
             CreateMap<TemplateForms, FormResponse>();
        }
    }
}
