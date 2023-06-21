using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BaseResponse
{
    public abstract class BaseEntityResponse
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
    }
}
