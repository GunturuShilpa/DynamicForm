using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FieldOptions.Entity
{
    public class FormFieldOptions
    {
        public int Id { get; set; }
        public int TemplateFormFieldId { get; set; }
        public string OptionValue { get; set; }
        public bool Status { get; set; }
    }
}
