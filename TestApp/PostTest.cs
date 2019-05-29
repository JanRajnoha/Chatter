using System;
using System.Collections.Generic;
using Chatter.BL.DTO.Comment;
using Chatter.BL.DTO.Post;
using Chatter.BL.DTO.User;
using Chatter.Common.Enum;
using Chatter.Test.TestFixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using Assert = Xunit.Assert;

namespace Chatter.Test
{
    [TestClass]
    public class PostTest : IClassFixture<PostTestFixture>
    {

        //private readonly BLTestFixture fixture = new BLTestFixture();
        private readonly PostTestFixture fixture = new PostTestFixture();

        public PostTest(PostTestFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void Create_WithNonExistingItem_DoesNotThrow()
        {
            var model = new PostDetailModelDTO
            {
                PostHeader = "Shieeet",
                User = new UserDetailModelDTO()
                {
                    Id = new Guid(),
                    Username = "Erzik",
                    FirstName = "Erik",
                    LastName = "Hudcovský",
                    Password = "HardcorePassword",
                    Email = "MyEmail@gmail.com",
                    Gender = Gender.Male
                },
                Comments = new List<CommentDetailModelDTO>(),
                Content = "Content"
            };

            var returnedModel = fixture.Repository.AddPost(model);

            Assert.NotNull(returnedModel);

            fixture.Repository.RemovePost(returnedModel.Id);
        }

        [Fact]
        public void AddPost()
        {
            var model = new PostDetailModelDTO
            {
                PostHeader = "Shieeet",
                User = new UserDetailModelDTO()
                {
                    Id = new Guid(),
                    Username = "Erzik",
                    FirstName = "Erik",
                    LastName = "Hudcovský",
                    Password = "HardcorePassword",
                    Email = "MyEmail@gmail.com",
                    Gender = Gender.Male
                },
                Comments = new List<CommentDetailModelDTO>(),
                Content = "Content"
            };

            var returnedModel = fixture.Repository.AddPost(model);

            Assert.NotNull(returnedModel);

            fixture.Repository.RemovePost(returnedModel.Id);
        }
    }
}
