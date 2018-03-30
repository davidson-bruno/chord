using System;

namespace Transposer.Exceptions
{
    public class NotAKeyException : Exception
    {
        public NotAKeyException() : base() { }
        public NotAKeyException(string message) : base(message) { }
        public NotAKeyException(string message, Exception inner) : base(message, inner) { }
        protected NotAKeyException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
