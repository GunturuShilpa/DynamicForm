namespace DynamicForm.Models
{
    public class UserFormValuesModel
    {
        public int Id { get; set; }
     
        public string FieldValue { get; set; } = string.Empty;
        public string TemplateName { get; set; } = string.Empty;
        public string FieldName { get; set; } = string.Empty;
        public int TemplateFormId { get; set; }
        public int TemplateFormFieldId { get; set; } 

       
    }
}
