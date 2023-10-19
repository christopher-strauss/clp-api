using System;
using System.Runtime.Serialization;

namespace CarLinePickup.Domain.Exceptions
{
    /// <summary>
    /// To be thrown whenever a business rule validation exception occurs in the domain layer.
    /// </summary>
    [Serializable]
    public class BusinessRuleViolationException : Exception
    {
        public string ErrorCode { get; set; }


        public BusinessRuleViolationException()
        {
        }

        public BusinessRuleViolationException(string message) : base(message)
        {
        }

        public BusinessRuleViolationException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public BusinessRuleViolationException(string message, Exception inner) : base(message, inner)
        {
        }

        public BusinessRuleViolationException(string message, string errorCode, Exception inner) : base(message, inner)
        {
            ErrorCode = errorCode;
        }

        protected BusinessRuleViolationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}