using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chatter.BL.DBAccess;
using Chatter.BL.DBAccess.Interfaces;

namespace Chatter.Test.TestFixtures
{
    public class FileTestFixture
    {
        private readonly IDBFile repository;

        public FileTestFixture()
        {
            repository = new DBFile(new InMemoryDBContext());
        }

        public IDBFile Repository => repository;
    }
}
