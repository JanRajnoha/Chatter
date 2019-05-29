using Chatter.DAL.Data;
using System;
using Xunit;

namespace Chatter.Test
{
    public class DBTest
    {
        [Fact]
        public void ConnectionSimpleTest()
        {
            using (var chatterConnection = new ChatterDBContext())
            {
                Assert.NotNull(chatterConnection);
            }
        }

        [Fact]
        public void ConnectionNormalTest()
        {
            using (var chatterConnection = new ChatterDBContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ChatterDBContext>()))
            {
                Assert.NotNull(chatterConnection);
            }
        }
    }
}
