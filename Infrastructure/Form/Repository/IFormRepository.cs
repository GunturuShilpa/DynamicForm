using Infrastructure.Form.Entity;

namespace Infrastructure.Form.Repository
{
    public interface IFormRepository
    {
        Task<int> Create(TemplateForms model);
        Task<IEnumerable<TemplateForms>> GetByQuery(string where);
        Task<TemplateForms> GetById(int id);
        Task<int> Delete(int id, int? userId = null);
        Task<int> Update(TemplateForms forms);
        Task<int> UpdateByQuery(string where); 
    }
}
