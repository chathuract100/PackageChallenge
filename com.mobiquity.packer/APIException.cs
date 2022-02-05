using System;

namespace com.mobiquity.packer
{
    /// <summary>
    /// Implementation class to handle library level API exceptions
    /// </summary>
    public class APIException : Exception
    {
        public APIException(string message) : base(message)
        {
        }

    }
}
