using System;
using System.Net;
using System.Runtime.Serialization;

namespace CarLinePickup.Domain.Exceptions
{
    [Serializable]
    public class TileException : Exception
    {
        public HttpStatusCode HttpStatus { get; set; }
        public string ErrorCode { get; set; }

        public TileException() 
        {
        }

        public TileException(string message) : base(message)
        {
        }

        public TileException(string message, HttpStatusCode code) : base(message)
        {
            HttpStatus = code;
        }

        public TileException(string message, string errorCode, HttpStatusCode code) : base(message)
        {
            HttpStatus = code;
            ErrorCode = errorCode;
        }


        public TileException(string message, Exception inner) : base(message, inner)
        {
        }

        public TileException(string message, string errorCode, Exception inner) : base(message, inner)
        {
            ErrorCode = errorCode;
        }

        protected TileException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
