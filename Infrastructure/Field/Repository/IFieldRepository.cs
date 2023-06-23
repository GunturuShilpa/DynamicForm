using Infrastructure.Field.Entity;

namespace Infrastructure.Field.Repository
{
    public interface IFieldRepository
    {
        Task<int> Create(TemplateFormFields fields);

        Task<int> Update(TemplateFormFields fields);

        Task<TemplateFormFields> GetById(int id);

        Task<IEnumerable<TemplateFormFields>> GetByQuery(string where);

        Task<int> Delete(int id, int? userId = null);
    }
}
