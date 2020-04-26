using System;

namespace CoreLibrary.Exceptions
{
    public class InternalServerException : Exception
    {
        public InternalServerException(string msg = null) : base(msg)
        {
        }
    }
}
