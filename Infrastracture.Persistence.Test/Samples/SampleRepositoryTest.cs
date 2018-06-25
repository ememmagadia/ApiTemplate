using Domain.Models.Samples;
using Infrastracture.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Infrastracture.Persistence.Test
{
    public class SampleRepositoryTest
    {

        [TestClass]
        public class VenueRepositoryTest
        {
            private Sample sample;
            private DbContextOptions<ApiDbContext> dbOptions;
            private ApiDbContext dbContext;
            private SampleRepository sut;
            private string connectionString = @"Data Source=.; Database=ApiTemplate; Integrated Security=true;";

            [TestInitialize]
            public void Initialize()
            {
                sample = new Sample
                {
                    SampleId = Guid.NewGuid(),
                    SampleName = "Sample Venue",
                    Description = "Desc"
                    
                };

                dbOptions = new DbContextOptionsBuilder<ApiDbContext>()
                                .UseSqlServer(connectionString)
                                    .Options;

                dbContext = new ApiDbContext(dbOptions);
                dbContext.Database.EnsureCreated();
                sut = new SampleRepository(dbContext);

            }

            [TestCleanup]
            public void Cleanup()
            {
                dbContext.Dispose();
                dbContext = null;
            }

            [TestMethod]
            [Description("Integration Testing")]

            public void Create_WithValiData_ShouldSaveDataToDatabase()
            {
                // arrange

                // act
                var newItem = sut.Create(sample);
                // assert
                Assert.IsNotNull(newItem);
                Assert.IsTrue(newItem.SampleId != Guid.Empty);

                // cleanup
                sut.Delete(newItem.SampleId);
            }

            [TestMethod] // test stubs
            [Description("Integration Testing")]

            public void Delete_WithAnExistingData_ShouldDeleteFromDatabase() // ServiceMethodToBeTest_ExpectedInput_ExpectedBehavior()
            {
                // arrange
                var newItem = sut.Create(sample);

                // act
                sut.Delete(sample.SampleId);
                var found = sut.Retrieve(sample.SampleId);
                // assert
                Assert.IsNull(found);
            }


            [TestMethod] // test stubs
            [Description("Integration Testing")]

            public void Retrieve_WithExistingData_ShouldRetrieveFromDatabase() // ServiceMethodToBeTest_ExpectedInput_ExpectedBehavior()
            {
                // arrange
                var newItem = sut.Create(sample);
                // act
                var found = sut.Retrieve(sample.SampleId);
                // assert
                Assert.IsNotNull(found);

                // cleanup
                sut.Delete(found.SampleId);
            }


            [TestMethod] // test stubs
            [Description("Integration Testing")]
            public void Update_WithExistingData_shouldUpdateFromDatabase() // ServiceMethodToBeTest_ExpectedInput_ExpectedBehavior()
            {
                // arrange
                var newItem = sut.Create(sample);
                var expectedName = "emem";
                // act
                newItem.SampleName = expectedName;

                sut.Update(newItem.SampleId, newItem);

                var updatedItem = sut.Retrieve(newItem.SampleId);
                // assert
                Assert.AreEqual(updatedItem.SampleName, expectedName);

                //clean up
                sut.Delete(updatedItem.SampleId);

            }


            [TestMethod] // test stubs
            public void LinqTest_WithValidData_ShouldCallRepositoryLinqTest() // ServiceMethodToBeTest_ExpectedInput_ExpectedBehavior()
            {
                // arrange

                // act
                var found = sut.LinqTest();
                // assert
                Assert.IsNotNull(found);

            }
        }

    }
}
