using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UserForm.Entity
{
    public class UserForms
    {
        public int Id { get; set; }
        public int TemplateFormId { get; set; }    
        public DateTime CreatedDate { get; set; }
    }
}
