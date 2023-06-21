namespace Core.Services.Form.Requests
{
    public class FormRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public int Ordinal { get; set; }
    }
}
