using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Common
{
    public class CustomException: Exception
    {
        public CustomException(string message) : base(message) { }
        public CustomException(string message, Exception ex) : base(message, ex) { }
    }
}
