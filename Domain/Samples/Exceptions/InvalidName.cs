using System;

namespace Domain.Samples.Exceptions
{
    public class InvalidNameException : ApplicationException
    {
        public InvalidNameException(string message) : base(message)
        {
        }
    }
}