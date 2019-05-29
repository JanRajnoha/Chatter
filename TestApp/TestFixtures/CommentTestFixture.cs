using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chatter.BL.DBAccess;
using Chatter.BL.DBAccess.Interfaces;

namespace Chatter.Test.TestFixtures
{
    public class CommentTestFixture
    {
        private readonly IDBComment repository;

        public CommentTestFixture()
        {
            repository = new DBComment(new InMemoryDBContext());
        }

        public IDBComment Repository => repository;
    }
}
