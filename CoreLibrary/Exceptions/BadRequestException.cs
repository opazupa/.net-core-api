using System;
namespace CoreLibrary.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string msg = null): base(msg)
        {     
        }
    }
}
