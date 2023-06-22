using Infrastructure.Field.Entity;
using Infrastructure.Form.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Field.Repository
{
    public interface IFieldRepository
    {
        //Task<int> Create(Entity.TemplateFields model);
        Task<int> Create(TemplateFormFields fields);
        Task<IEnumerable<TemplateFormFields>> GetByQuery(string where = null);
    }
}
