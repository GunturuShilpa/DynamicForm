namespace Core.Services.TemplateFields.Responses
{
    public class FieldResponse
    {
        public int Id { get; set; }

        public int TemplateFormId { get; set; }

        public int OrderNo { get; set; }

        public string Name { get; set; } = string.Empty;

        public int ControlId { get; set; }

        public string DefaultValue { get; set; } = string.Empty;

        public bool IsRequired { get; set; } = true;

        public string RequiredMessage { get; set; } = string.Empty;

        public string RegExValue { get; set; } = string.Empty;

        public string RegExMessage { get; set; } = string.Empty;

        public int Status { get; set; }
    }
}
