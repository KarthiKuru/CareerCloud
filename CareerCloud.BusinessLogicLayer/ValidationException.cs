using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ValidationException:Exception
    {
        public ValidationException(int code, string message) : base(message)
        {
            Code = code;

        }

        public int Code { get; private set; }
    }
}
