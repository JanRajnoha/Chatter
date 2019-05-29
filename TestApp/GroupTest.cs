using System;
using System.Collections.Generic;
using Chatter.BL.DTO.Group;
using Chatter.BL.DTO.Organization;
using Chatter.BL.DTO.Post;
using Chatter.BL.DTO.User;
using Chatter.Test.TestFixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Chatter.Test
{
    [TestClass]
    public class GroupTest : IClassFixture<GroupTestFixture>
    {
        private readonly GroupTestFixture fixture = new GroupTestFixture();

        public GroupTest(GroupTestFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void Create_WithNonExistingItem_DoesNotThrow()
        {
            Guid GroupId = new Guid();

            var model = new GroupDetailModelDTO
            {
                Posts = new List<PostDetailModelDTO>()
                {
                    new PostDetailModelDTO()
                    {
                        Id = GroupId
                    }
                },
                Description = "test description",
                Name = "test name"
            };

            var returnedModel = fixture.Repository.AddGroup(model);

            Assert.IsNotNull(returnedModel);

            fixture.Repository.RemoveGroup(returnedModel.Id);
        }
    }
}

