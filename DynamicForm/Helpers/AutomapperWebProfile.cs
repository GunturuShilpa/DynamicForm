using Core.Helpers;
using Core.Services.Form.Responses;
using DynamicForm.Models;

namespace DynamicForm.Helpers
{
    public class AutomapperWebProfile : AutomapperProfile
    {
        public AutomapperWebProfile()
        {
            CreateMap<TemplateFormModel, FormResponse>().ReverseMap();
        }

        }
}
