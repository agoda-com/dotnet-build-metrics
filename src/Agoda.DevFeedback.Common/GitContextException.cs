using System;

namespace Agoda.DevFeedback.Common
{
    public class GitContextException : Exception
    {
        public GitContextException(string message) : base(message)
        {
        }
        
        public GitContextException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}