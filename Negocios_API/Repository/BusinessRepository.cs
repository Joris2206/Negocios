using Negocios_API.Datos;
using Negocios_API.Models;
using Negocios_API.Repository.IRepository;

namespace Negocios_API.Repository
{
    public class BusinessRepository : Repository<Business>, IBusinessRepository
    {
        private readonly ApplicationDbContext _db;

        public BusinessRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public async Task<Business> Update(Business entity)
        {
            entity.FechaActualizacion = DateTime.Now;
            _db.Businesses.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
