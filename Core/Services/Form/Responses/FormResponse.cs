using Core.BaseResponse;

namespace Core.Services.Form.Responses
{
    public class FormResponse : BaseEntityResponse
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int Ordinal { get; set; }
    }
}
