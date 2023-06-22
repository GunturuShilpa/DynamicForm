using Infrastructure.ControlType.Entity;

namespace Infrastructure.ControlType.Repository
{
    public interface IControlTypesRepository
    {
        Task<IEnumerable<ControlTypes>> GetByQuery(string where = null);
    }
}
