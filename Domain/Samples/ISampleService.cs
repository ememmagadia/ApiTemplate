using System;
using Domain.Models.Samples;

namespace Domain.Samples
{
    public interface ISampleService
    {
        Sample Save(Guid sampleId, Sample sample);
    }
}