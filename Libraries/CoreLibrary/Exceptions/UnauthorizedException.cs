using System;
namespace CoreLibrary.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string msg = null): base(msg)
        {     
        }
    }
}
