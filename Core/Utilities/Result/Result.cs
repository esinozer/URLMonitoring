using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Result
{
    public class Result : IResult
    {
        public Result(bool succsess, string message) : this(succsess)
        {
            Message = message;
        }
        public Result(bool succsess)
        {
            Succsess = succsess;
        }
        public bool Succsess { get; }

        public string Message { get; }
    }
}
