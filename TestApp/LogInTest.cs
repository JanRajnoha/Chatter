using System;
using Chatter.BL.DTO.LogIn;
using Chatter.Test.TestFixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Chatter.Test
{
    [TestClass]
    public class LogInTest : IClassFixture<LogInTestFixture>
    {
        private readonly LogInTestFixture fixture = new LogInTestFixture();

        public LogInTest(LogInTestFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void Create_WithNonExistingItem_DoesNotThrow()
        {
            var model = new LogInDetailModelDTO
            {
                Id = new Guid(),
                User = new BL.DTO.User.UserDetailModelDTO()
            };

            var returnedModel = fixture.Repository.AddLog(model);

            Assert.IsNotNull(returnedModel);
        }
    }
}
