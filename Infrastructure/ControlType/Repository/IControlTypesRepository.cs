using Infrastructure.ControlType.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ControlType.Repository
{
    public interface IControlTypesRepository
    {
        Task<IEnumerable<ControlTypes>> GetByQuery(string where = null);
    }
}
