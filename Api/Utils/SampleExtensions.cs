using Domain.Models.Samples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Utils
{
    public static class SampleExtensions
    {
        public static Sample ApplyChanges(this Sample sample, Sample from)
        {
            sample.SampleName = from.SampleName;
            sample.Description = from.Description;

            return sample;
        }
    }
}
