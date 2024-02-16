using Negocios_API.Datos;
using Negocios_API.Models;
using Negocios_API.Repository.IRepository;

namespace Negocios_API.Repository
{
    public class OwnerRepository : Repository<Owner>, IOwnerRepository
    {
        private readonly ApplicationDbContext _db;

        public OwnerRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Owner> Update(Owner entity)
        {
            entity.FechaActualizacion = DateTime.Now;
            _db.Owners.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
