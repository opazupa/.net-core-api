using System;

namespace CoreLibrary.Exceptions
{
    public class UnAuthorizedException : Exception
    {
        public UnAuthorizedException(string msg = null): base(msg)
        {     
        }
    }
}
