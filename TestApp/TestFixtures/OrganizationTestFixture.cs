using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chatter.BL.DBAccess;
using Chatter.BL.DBAccess.Interfaces;

namespace Chatter.Test.TestFixtures
{
    public class OrganizationTestFixture
    {
        private readonly IDBOrganization repository;

        public OrganizationTestFixture()
        {
            repository = new DBOrganization(new InMemoryDBContext());
        }

        public IDBOrganization Repository => repository;
    }
}
