using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.UserForm.Requests
{
    public class UserFormsRequest
    {
        public int Id { get; set; }
        public int TemplateFormId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
