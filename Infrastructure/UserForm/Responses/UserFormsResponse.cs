using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UserForm.Responses
{
    public class UserFormsResponse
    {
        public int Id { get; set; }
        public int TemplateFormId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
