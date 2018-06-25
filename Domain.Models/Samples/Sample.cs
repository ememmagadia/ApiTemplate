using System;

namespace Domain.Models.Samples
{
    public class Sample
    {
        public Guid SampleId { get; set; }
        public string SampleName { get; set; }
        public string Description { get; set; }
        public int Age { get; set; }
    }
}