namespace Core.Services.TemplateFields.Requests
{
    public class FieldRequest
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int ControlId { get; set; }
    }
}
