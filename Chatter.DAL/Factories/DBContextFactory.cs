using Chatter.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace Chatter.DAL.Factories
{
    public class DBContextFactory : IDBContextFactory
    {
        public ChatterDBContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ChatterDBContext>();
            optionsBuilder.UseSqlServer("Server = tcp:chatter.database.windows.net, 1433; Initial Catalog = ChatterDB; Persist Security Info = False; User ID = JR; Password =ChatterDB1; MultipleActiveResultSets = False; Encrypt = True;Database=EFProviders.InMemory; TrustServerCertificate = False; Connection Timeout = 30;");
            return new ChatterDBContext(optionsBuilder.Options);
        }
    }
}
