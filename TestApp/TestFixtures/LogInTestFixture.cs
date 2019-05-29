using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chatter.BL.DBAccess;
using Chatter.BL.DBAccess.Interfaces;

namespace Chatter.Test.TestFixtures
{
    public class LogInTestFixture
    {
        private readonly IDBLogIn repository;

        public LogInTestFixture()
        {
            repository = new DBLogIn(new InMemoryDBContext());
        }

        public IDBLogIn Repository => repository;
    }
}
