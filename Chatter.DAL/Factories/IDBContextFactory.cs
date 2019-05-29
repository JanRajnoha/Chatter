using Chatter.DAL.Data;

namespace Chatter.DAL.Factories
{
    public interface IDBContextFactory
    {
        ChatterDBContext CreateDbContext();
    }
}
