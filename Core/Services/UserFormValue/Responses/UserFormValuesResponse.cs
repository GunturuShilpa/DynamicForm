using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.UserFormValue.Responses
{
    public class UserFormValuesResponse
    {
        public int Id { get; set; }
        public int TemplateFormId { get; set; }
        public int TemplateFormFieldId { get; set; }
        public string FieldValue { get; set; }
    }
}
