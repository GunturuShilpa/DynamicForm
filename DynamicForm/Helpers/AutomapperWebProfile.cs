using Core.Helpers;
using Core.Services.Form.Responses;
using Core.Services.TemplateFields.Responses;
using DynamicForm.Models;

namespace DynamicForm.Helpers
{
    public class AutomapperWebProfile : AutomapperProfile
    {
        public AutomapperWebProfile()
        {
            CreateMap<FormResponse, TemplateFormModel>();
            CreateMap<FieldResponse, TemplateFieldsModel>();
            CreateMap<TemplateFieldsModel, FieldResponse>();
        }
    }
}
