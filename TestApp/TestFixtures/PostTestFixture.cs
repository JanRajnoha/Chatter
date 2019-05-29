using Chatter.BL.DBAccess;
using Chatter.BL.DBAccess.Interfaces;

namespace Chatter.Test.TestFixtures
{
    public class PostTestFixture
    {
        private readonly IDBPost repository;

        public PostTestFixture()
        {
            repository = new DBPost(new InMemoryDBContext());
        }

        public IDBPost Repository => repository;

    }
}
