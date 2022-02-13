using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Result
{
    public class ErrorDataResult<T> : DataResult<T>
    {

        public ErrorDataResult(T data, string message) : base(data, succsess: false, message)
        {

        }
        public ErrorDataResult(T data) : base(data, succsess: false)
        {

        }
        public ErrorDataResult(string message) : base(default, succsess: false, message)
        {

        }
        public ErrorDataResult() : base(default, succsess: false)
        {

        }
    }
}
