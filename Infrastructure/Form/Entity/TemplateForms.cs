using Infrastructure.Base.Entity;

namespace Infrastructure.Form.Entity
{
    public class TemplateForms : TableEntity
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int Ordinal { get; set; }
    }
}
