using System;
using Chatter.BL.DTO.Organization;
using Chatter.Test.TestFixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Chatter.Test
{
    [TestClass]
    public class OrganizationTest : IClassFixture<OrganizationTestFixture>
    {
        private readonly OrganizationTestFixture fixture = new OrganizationTestFixture();

        public OrganizationTest(OrganizationTestFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void Create_WithNonExistingItem_DoesNotThrow()
        {
            var model = new OrganizationDetailModelDTO()
            {
                Id = new Guid()
            };

            var returnedModel = fixture.Repository.AddOrganization(model);

            Assert.IsNotNull(returnedModel);


        }
    }
}
