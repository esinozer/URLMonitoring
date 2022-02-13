using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Result
{
    public class SuccessDataResult<T> : DataResult<T>
    {

        public SuccessDataResult(T data, string message) : base(data, succsess: true, message)
        {

        }
        public SuccessDataResult(T data) : base(data, succsess: true)
        {

        }
        public SuccessDataResult(string message) : base(default, succsess: true, message)
        {

        }
        public SuccessDataResult() : base(default, succsess: true)
        {

        }
    }
}
