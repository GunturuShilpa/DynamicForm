using Infrastructure.UserForm.Entity;
using Infrastructure.UserFormValue.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UserForm.Repository
{
    public interface IUserFormRepository
    {
        Task<int> Create(UserForms userForms);

        Task<int> Update(UserForms userForms);

        Task<UserForms> GetById(int id);

        Task<IEnumerable<UserForms>> GetByQuery(string where);

        Task<int> Delete(int id, int? userId = null);
    }
}
