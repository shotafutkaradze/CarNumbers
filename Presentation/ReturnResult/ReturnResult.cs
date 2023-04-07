using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doamin.ReturnResult
{
    public class ReturnResult<T> : OperationResult
    {
        public T Result { get; set; }
    }

    public class OperationResult
    {
        public bool IsSuccess { get; set; } = true;

        public string ErrorMessage { get; set; }
    }
}
