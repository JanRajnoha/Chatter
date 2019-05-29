using Chatter.BL.DBAccess;
using Chatter.BL.DBAccess.Interfaces;

namespace Chatter.Test.TestFixtures
{
    public class GroupTestFixture
    {
        private readonly IDBGroup repository;

        public GroupTestFixture()
        {
            repository = new DBGroup(new InMemoryDBContext());
        }

        public IDBGroup Repository => repository;
    }
}
