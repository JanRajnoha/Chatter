using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chatter.BL.DBAccess;
using Chatter.BL.DBAccess.Interfaces;

namespace Chatter.Test.TestFixtures
{
    public class UserTestFixture
    {
        private readonly IDBUser repository;

        public UserTestFixture()
        {
            repository = new DBUser(new InMemoryDBContext());
        }

        public IDBUser Repository => repository;
    }
}
