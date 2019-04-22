using System;
namespace CoreLibrary.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string msg) : base(msg)
        {
        }
    }
}
