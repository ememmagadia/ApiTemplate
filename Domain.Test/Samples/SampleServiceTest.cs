using Domain.Samples;
using Domain.Models.Samples;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using Domain.Samples.Exceptions;

namespace Domain.Test
{

    [TestClass]
    public class SampleServiceTest
    {
        // Put Global Variables Here

        private Mock<ISampleRepository> mockRepo; // Mock Interface to be tested

        private SampleService sut; // Service / System under test

        private Sample sample; // model


        private readonly Guid existingId = Guid.NewGuid();
        private readonly Guid nonExistingId = Guid.Empty;


        [TestInitialize] // Initialize TEst
        public void Initialize()
        {
            mockRepo = new Mock<ISampleRepository>(); // Mock Interface to be tested

            sut = new SampleService(mockRepo.Object); // Service / System under test

            sample = new Sample // model
            {
                SampleId = new Guid(),
                SampleName = "Emmanuel",
                Description = "Description"
            };


            // Existing Id

            mockRepo
                .Setup(
                    r => r.Retrieve(existingId)
                )
                .Returns(new Sample());

            // Non Existing Id
            mockRepo
                .Setup(
                    r => r.Retrieve(nonExistingId)
                ).Returns<Sample>(null);
        }

        [TestCleanup] // Clean every method
        public void Cleanup()
        {

        }



        [TestMethod] // test stubs
        public void Save_WithValidData_ShouldCallRepositoryCreateAndReturnId() // ServiceMethodToBeTest_ExpectedInput_ExpectedBehavior()
        {
            // arrange
            mockRepo
                 .Setup(
                     r => r.Create(sample)
                 )
                 .Callback(
                     () =>
                     {
                         sample.SampleId = Guid.NewGuid();
                     }
                 )
                 .Returns(sample);

            // act
            var result = sut.Save(sample.SampleId, sample);

            // assert
            mockRepo
                .Verify(
                    r => r.Create(sample), Times.Once
                );


            Assert.IsTrue(result.SampleId != Guid.Empty);
        }


        [TestMethod] // test stubs
        public void Save_WithBlankSampleName_ShouldThrowInvalidNameException() // ServiceMethodToBeTest_ExpectedInput_ExpectedBehavior()
        {
            // arrange
            sample.SampleName = "";
            // act
            Assert.ThrowsException<InvalidNameException>(
                    () => sut.Save(sample.SampleId, sample)
                );
            // assert
            mockRepo
                .Verify(
                    r => r.Create(sample), Times.Never
                );
        }


        [TestMethod] // test stubs
        public void Save_WithNonExistingId_ShouldCallRepositoryRetrieveAndCreate() // ServiceMethodToBeTest_ExpectedInput_ExpectedBehavior()
        {
            // arrange

            ////var existingId = Guid.NewGuid();
            //mockRepo
            //    .Setup(
            //        r => r.Retrieve(existingId)
            //    )
            //    .Returns(sample);

            // act
            sut.Save(nonExistingId, sample);

            // assert
            mockRepo
                .Verify(
                    r => r.Retrieve(nonExistingId), Times.Once
                );

            mockRepo
                .Verify(
                    r => r.Create(sample), Times.Once
                );

            mockRepo
                .Verify(
                    r => r.Update(nonExistingId, sample), Times.Never
                );
        }

        [TestMethod] // test stubs
        public void Save_WithExistingId_ShouldCallRepositoryRetrieveAndUpdate() // ServiceMethodToBeTest_ExpectedInput_ExpectedBehavior()
        {
            // arrange

            // act
            sut.Save(existingId, sample);

            // assert
            mockRepo
                .Verify(
                    r => r.Retrieve(existingId), Times.Once
                );

            mockRepo
                .Verify(
                    r => r.Update(existingId, sample), Times.Once
                );

            mockRepo
                .Verify(
                    r => r.Create(sample), Times.Never
                );
        }
    }


}
