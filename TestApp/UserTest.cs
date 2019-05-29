using System;
using System.Collections.Generic;
using Chatter.BL;
using Chatter.BL.DTO.Group;
using Chatter.BL.DTO.LogIn;
using Chatter.BL.DTO.Organization;
using Chatter.BL.DTO.User;
using Chatter.Common.Enum;
using Chatter.Test.TestFixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Chatter.Test
{
    [TestClass]
    public class UserTest : IClassFixture<UserTestFixture>
    {
        private readonly UserTestFixture fixture = new UserTestFixture();

        public UserTest(UserTestFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void Create_WithNonExistingItem_DoesNotThrow()
        {
            var res = Crypto.Encrypt("testPass");

            var model = new UserDetailModelDTO
            {
                Id = new Guid(),
                Groups = new List<GroupListModelDTO>()
                {
                    new GroupListModelDTO()
                    {
                        Id = new Guid()
                    }
                },
                Description = "Test Description",
                Email = "email@email.com",
                Hash = res.Hash,
                Gender = Gender.Female,
                LastName = "test",
                FirstName = "test",
                Username = "test",
                AdminGroups = new List<GroupListModelDTO>()
                {
                    new GroupListModelDTO()
                    {
                        Id = new Guid()
                    }
                },
                AdminOrganizations = new List<OrganizationListModelDTO>()
                {
                    new OrganizationListModelDTO()
                    {
                        Id = new Guid()
                    }
                },
                Organizations = new List<OrganizationListModelDTO>()
                {
                    new OrganizationListModelDTO()
                    {
                        Id = new Guid()
                    }
                },
                RegistrationDate = DateTime.Today,
                Password = "Asdasdasd",
                Salt = res.Salt,
                SignInLogs = new List<LogInDetailModelDTO>()
                {
                    new LogInDetailModelDTO()
                    {
                        Id = new Guid(),
                        User = new UserDetailModelDTO()
                    }
                }
                
            };

            var returnedModel = fixture.Repository.AddUser(model);

            Assert.IsNotNull(returnedModel);
        }
    }
}
