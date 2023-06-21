using AutoMapper;
//using Core.Services.Form.Requests;
//using Core.Services.Form.Responses;
using Infrastructure.Form.Entity;
using Infrastructure.Form.Responses;

namespace Core.Helpers
{
    public class AutomapperProfile: Profile
    {
        public AutomapperProfile()
        {
            CreateMap<TemplateForms, FormResponse>().ReverseMap();
        }
        }
}
