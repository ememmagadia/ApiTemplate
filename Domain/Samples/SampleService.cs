using Domain.Models.Samples;
using Domain.Samples.Exceptions;
using System;

namespace Domain.Samples
{
    public class SampleService : ISampleService
    {
        private ISampleRepository repo;

        public SampleService(ISampleRepository repo)
        {
            this.repo = repo;
        }

        public Sample Save(Guid sampleId, Sample sample)
        {
            Sample result;

            if (string.IsNullOrWhiteSpace(sample.SampleName)) throw new InvalidNameException("Name is required!");

            var retrieved = repo.Retrieve(sampleId);

            if (retrieved == null)
            {
                result = repo.Create(sample);
            }
            else
            {
                result = repo.Update(sampleId, sample);
            }

            return result;
        }
    }
}