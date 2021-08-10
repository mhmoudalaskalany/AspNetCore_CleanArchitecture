using System;

namespace BackendCore.Common.Exceptions
{
    public class BaseException :Exception
    {
        public BaseException():base()
        {

        }

        public BaseException(string message) : base(message)
        {

        }
        public BaseException(string message,Exception innerException) : base(message,innerException)
        {
            
        }

    }
}
