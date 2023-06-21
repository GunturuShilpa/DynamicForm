using Core.BaseResponse;

namespace Core.Services.Form.Responses
{
    public class FormResponse : BaseEntityResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public int Ordinal { get; set; }
    }
}
