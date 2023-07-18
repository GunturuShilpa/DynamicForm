using Infrastructure.Base.Entity;

namespace Infrastructure.Field.Entity
{
    public class TemplateFormFields : TableEntity
    {
        public int TemplateFormId { get; set; }

        public int OrderNo { get; set; } = 0;

        public string Name { get; set; } = string.Empty;

        public int ControlId { get; set; }

        public string DefaultValue { get; set; } = string.Empty;

        public bool IsRequired { get; set; } = true;

        public string RequiredMessage { get; set; } = string.Empty;

        public string RegExValue { get; set; } = string.Empty;

        public string RegExMessage { get; set; } = string.Empty;
        public int Orientation { get; set; } = 0;
    }
}
