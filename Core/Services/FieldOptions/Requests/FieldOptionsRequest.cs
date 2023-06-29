namespace Core.Services.FieldOptions.Requests
{
    public class FieldOptionsRequest
    {
        public int Id { get; set; }
        public int TemplateFormFieldId { get; set; }
        public List<string> OptionValues { get; set; }
    }
}
