using Infrastructure.FieldOptions.Entity;

namespace Infrastructure.FieldOptions.Repository
{
    public interface IFormFieldOptionsRepository
    {
        Task<int> Create(FormFieldOptions fields);

        Task<int> Update(FormFieldOptions fields);

        Task<FormFieldOptions> GetById(int id);

        Task<IEnumerable<FormFieldOptions>> GetByQuery(string where);

        Task<int> Delete(int id, int? userId = null);
    }
}
