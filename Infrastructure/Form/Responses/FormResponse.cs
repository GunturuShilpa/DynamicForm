namespace Infrastructure.Form.Responses
{
    public class FormResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int Ordinal { get; set; }

        public bool Status { get; set; }
    }
}
