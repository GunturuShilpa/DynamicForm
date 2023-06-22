namespace Core.Services.Form.Requests
{
    public class FormRequest
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int Ordinal { get; set; }
    }
}
