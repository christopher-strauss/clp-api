using System;
using System.Runtime.Serialization;

namespace CarLinePickup.Domain.Exceptions
{
    /// <summary>
    /// Represents an exception that occurred as a result of an unexpected service response.
    /// </summary>
    [Serializable]
    public class UnexpectedResponseFromServiceException : Exception
    {
        public UnexpectedResponseFromServiceException()
        {
        }

        public UnexpectedResponseFromServiceException(string message) : base(message)
        {
        }

        public UnexpectedResponseFromServiceException(string message, Exception inner) : base(message, inner)
        {
        }

        protected UnexpectedResponseFromServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
