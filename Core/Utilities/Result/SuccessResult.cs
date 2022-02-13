using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Result
{
    public class SuccessResult : Result
    {
        public SuccessResult(string message) : base(succsess: true, message)
        {

        }
        public SuccessResult() : base(succsess: true)
        {

        }
    }

}
