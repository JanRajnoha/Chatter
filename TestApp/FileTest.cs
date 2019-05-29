using System;
using Chatter.BL.DTO.File;
using Chatter.BL.DTO.User;
using Chatter.Common.Enum;
using Chatter.Test.TestFixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Chatter.Test
{
    [TestClass]
    public class FileTest : IClassFixture<FileTestFixture>
    {
        private readonly FileTestFixture fixture = new FileTestFixture();

        public FileTest(FileTestFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void Create_WithNonExistingItem_DoesNotThrow()
        {
            var model = new FileDetailModelDTO()
            {
                Path = "test path",
                Id = new Guid(),
                Post = null,
                Comment = null,
                FileType = FileType.File,
                Name = "test name",
                Size = 15,
                UploadedBy = new UserDetailModelDTO() { Password = "asdasdasd" },
                DateTime = DateTime.Today
            };

            var returnedModel = fixture.Repository.AddFile(model);

            Assert.IsNotNull(returnedModel);
        }
    }
}
