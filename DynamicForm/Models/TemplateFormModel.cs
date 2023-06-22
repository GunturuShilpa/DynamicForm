namespace DynamicForm.Models
{
    public class TemplateFormModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int Ordinal { get; set; }
    }
}
