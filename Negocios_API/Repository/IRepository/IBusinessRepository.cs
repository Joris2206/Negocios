using Negocios_API.Models;

namespace Negocios_API.Repository.IRepository
{
    public interface IBusinessRepository :IRepository<Business>
    {
        Task<Business> Update(Business business);
    }
}
