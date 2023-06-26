using Infrastructure.Field.Entity;
using Infrastructure.Form.Entity;
using Infrastructure.Form.Responses;
using Shared.Result;

namespace Infrastructure.Form.Repository
{
    public interface IFormRepository
    {
        Task<int> Create(TemplateForms model);

        Task<IEnumerable<TemplateForms>> GetByQuery(string where);
        Task<Result<FormResponse>> GetById(int id);
        Task<int> Delete(int id, int? userId = null);

        Task<int> Update(TemplateForms forms);
    }
}
