using Infrastructure.Form.Entity;

namespace Infrastructure.Form.Repository
{
    public interface IFormRepository
    {
        Task<int> Create(TemplateForms model);
    }
}
