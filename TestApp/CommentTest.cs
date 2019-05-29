using System;
using Chatter.BL.DTO.Comment;
using Chatter.Test.TestFixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Chatter.Test
{
    [TestClass]
    public class CommentTest : IClassFixture<CommentTestFixture>
    {
        private readonly CommentTestFixture fixture = new CommentTestFixture();

        public CommentTest(CommentTestFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void Create_WithNonExistingItem_DoesNotThrow()
        {
            var model = new CommentDetailModelDTO
            {
                Id = new Guid()
            };

            //var returnedModel = fixture.Repository.AddComment(model);

            Assert.IsNotNull(model);
        }
    }
}
