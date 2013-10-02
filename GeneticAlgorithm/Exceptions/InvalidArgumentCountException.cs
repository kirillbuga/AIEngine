using System;
using System.Runtime.Serialization;

namespace Common.Entities.Exceptions
{
    public class InvalidArgumentCountException : Exception
    {
        public InvalidArgumentCountException()
        {
        }

        public InvalidArgumentCountException(string message) : base(message)
        {
        }

        public InvalidArgumentCountException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidArgumentCountException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}