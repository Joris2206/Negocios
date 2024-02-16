using Negocios_API.Models;

namespace Negocios_API.Repository.IRepository
{
    public interface IOwnerRepository : IRepository<Owner>
    {
        Task<Owner> Update(Owner owner);
    }
}
