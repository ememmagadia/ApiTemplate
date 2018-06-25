using Api.Controllers;
using Domain.Models.Samples;
using Domain.Samples;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace Api.Test
{
    public class SampleControllerTest
    {

        [TestClass]
        public class SampleServiceTest
        {
            private Mock<ISampleRepository> mockRepo; // Mock Interface to be tested
            private Mock<ISampleService> mockService;
            private SampleController sut; // Service / System under test

            private Sample sample; // model


            private Guid existingId = Guid.NewGuid();
            private Guid nonExistingId = Guid.Empty;
            // Put Global Variables Here


            [TestInitialize] // Initialize TEst
            public void Initialize()
            {
                mockRepo = new Mock<ISampleRepository>(); // Mock Interface to be tested
                mockService = new Mock<ISampleService>();
                sut = new SampleController(mockRepo.Object, mockService.Object); // Service / System under test

                sample = new Sample // model
                {
                    SampleId = new Guid(),
                    SampleName = "Emmanuel",
                    Description = "Description"
                };


                // set up 

                mockRepo
                     .Setup(
                         r => r.Retrieve(existingId)
                     )
                     .Returns(sample);

                mockRepo
                   .Setup(
                       r => r.Retrieve(nonExistingId)
                   )
                   .Returns<Sample>(null);
            }

            [TestCleanup] // Clean every method
            public void Cleanup()
            {

            }

            [TestMethod] // test stubs
            public void Get_WithNullId_ShouldReturnObjectResult() // ServiceMethodToBeTest_ExpectedInput_ExpectedBehavior()
            {
                // arrange
                mockRepo
                    .Setup(
                        r => r.Retrieve()
                    )
                    .Returns(new List<Sample>());
                // act
                var result = sut.GetSample(null);
                // assert
                mockRepo
                    .Verify(
                        r => r.Retrieve(), Times.Once
                    );

                Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            }


            [TestMethod] // test stubs
            public void Get_WithExistingId_ShouldReturnOkObjectResult() // ServiceMethodToBeTest_ExpectedInput_ExpectedBehavior()
            {
                // arrange

                // act
                var result = sut.GetSample(existingId);
                // assert
                mockRepo
                    .Verify(
                        r => r.Retrieve(existingId), Times.Once
                    );

                Assert.IsInstanceOfType(result, typeof(OkObjectResult));

            }


            [TestMethod] // test stubs
            public void Create_WithNullSample_ShouldReturnBadRequestResult() // ServiceMethodToBeTest_ExpectedInput_ExpectedBehavior()
            {
                // arrange
                sample = null;
                // act
                var result = sut.CreateSample(sample);
                // assert

                Assert.IsInstanceOfType(result, typeof(BadRequestResult));

                mockService
                    .Verify(
                        s => s.Save(Guid.Empty, sample), Times.Never
                    );
            }

            [TestMethod]
            public void Create_WithValidData_ShouldReturnOkObjectResult()
            {
                // arrange 

                // act
                var result = sut.CreateSample(sample);

                // Assert
                Assert.IsInstanceOfType(result, typeof(OkObjectResult));

                mockService
                    .Verify(
                        s => s.Save( Guid.Empty, sample), Times.Once
                    );
            }

            [TestMethod] // test stubs
            public void Delete_WithNonExistingId_ShouldReturnNotFound() // ServiceMethodToBeTest_ExpectedInput_ExpectedBehavior()
            {
                // arrange
               
                // act
                var result = sut.DeleteSample(nonExistingId);
                // assert
                mockRepo
                    .Verify(
                        r => r.Retrieve(nonExistingId), Times.Once
                    );

                mockRepo
                    .Verify(
                        r => r.Delete(nonExistingId), Times.Never
                    );

                Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            }


            [TestMethod] // test stubs
            public void Delete_WithExistingId_ShouldReturnNoContentResult() // ServiceMethodToBeTest_ExpectedInput_ExpectedBehavior()
            {
                // arrange

                // act
                var result = sut.DeleteSample(existingId);
                // assert
                mockRepo
               .Verify(
                   r => r.Retrieve(existingId), Times.Once
               );

                mockRepo
               .Verify(
                   r => r.Delete(existingId), Times.Once
               );

                Assert.IsInstanceOfType(result, typeof(NoContentResult));
            }



            [TestMethod] // test stubs
            public void Update_WithNullSample_ShouldReturnBadRequiest() // ServiceMethodToBeTest_ExpectedInput_ExpectedBehavior()
            {
                // arrange
                sample = null;
                // act
                var result = sut.UpdateSample(sample, existingId);
                // assert
                Assert.IsInstanceOfType(result, typeof(BadRequestResult));

                mockRepo
                    .Verify(
                        r => r.Retrieve(existingId), Times.Never()
                    );

                mockRepo
                    .Verify(
                        r => r.Update(existingId, sample), Times.Never
                    );
            }


            [TestMethod] // test stubs
            public void Update_WithNonExistingId_ShouldReturnNotFound() // ServiceMethodToBeTest_ExpectedInput_ExpectedBehavior()
            {
                // arrange

                // act
                var result = sut.UpdateSample(sample, nonExistingId);
                // assert
                Assert.IsInstanceOfType(result, typeof(NotFoundResult));

                mockRepo
                    .Verify(
                        r => r.Retrieve(nonExistingId), Times.Once()
                    );

                mockRepo
                    .Verify(
                        r => r.Update(nonExistingId, sample), Times.Never
                    );

            }


            [TestMethod] // test stubs
            public void Update_WithValidData_ShouldReturnOkObjectResult() // ServiceMethodToBeTest_ExpectedInput_ExpectedBehavior()
            {
                // arrange

                // act
                var result = sut.UpdateSample(sample, existingId);
                // assert
                Assert.IsInstanceOfType(result, typeof(OkObjectResult));

                mockRepo
                    .Verify(
                        r => r.Retrieve(existingId), Times.Once()
                    );

                mockService
                    .Verify(
                        r => r.Save(existingId, sample), Times.Once
                    );

            }
        }

    }
}
