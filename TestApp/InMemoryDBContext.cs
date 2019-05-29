using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chatter.DAL.Data;
using Chatter.DAL.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.Extensions.DependencyInjection;

namespace Chatter.Test
{
    public class InMemoryDBContext : IDBContextFactory
    {
        public ChatterDBContext CreateDbContext()
        {
            var serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

            var optionsBuilder = new DbContextOptionsBuilder<ChatterDBContext>();
            optionsBuilder.UseInMemoryDatabase("InMemoryChatterDB").UseInternalServiceProvider(serviceProvider);
            return new ChatterDBContext(optionsBuilder.Options);
        }
    }
}
