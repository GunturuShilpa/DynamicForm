using Infrastructure.Field.Entity;
using Infrastructure.UserFormValue.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UserFormValue.Repository
{
    public interface IUserFormValuesRepository
    {
        Task<int> Create(UserFormValues userFormValues);

        Task<int> Update(UserFormValues userFormValues);

        Task<UserFormValues> GetById(int id);

        Task<IEnumerable<UserFormValues>> GetByQuery(string where);

        Task<int> Delete(int id, int? userId = null);
    }
}
