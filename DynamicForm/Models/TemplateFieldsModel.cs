using System.ComponentModel.DataAnnotations;
namespace DynamicForm.Models
{
    public class TemplateFieldsModel
    {
        public int FieldId { get; set; }

        public int Id { get; set; }

        public int TemplateFormId { get; set; }

        public int OrderNo { get; set; } = 0;
        [Required]
        public string Name { get; set; } = string.Empty;

        public int ControlId { get; set; }
        public string TemplateName { get; set; }
        public string ControlName { get; set; } = string.Empty;

        public string DefaultValue { get; set; } = string.Empty;

        public bool IsRequired { get; set; } = true;

        public string RequiredMessage { get; set; } = string.Empty;

        public string RegExValue { get; set; } = string.Empty;

        public string RegExMessage { get; set; } = string.Empty;

        public int Status { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
