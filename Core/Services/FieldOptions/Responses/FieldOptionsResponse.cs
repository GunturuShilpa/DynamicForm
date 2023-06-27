using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.FieldOptions.Responses
{
    public class FieldOptionsResponse
    {
        public int Id { get; set; }
        public int TemplateFormFieldId { get; set; }
        public string OptionValue { get; set; }
        public bool status { get; set; }
    }
}
