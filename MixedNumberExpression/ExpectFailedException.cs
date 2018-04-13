using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedNumber.Expression
{
    public class ExpectFailedException: Exception
    {
        public ExpectFailedException(string message) : base(message)
        { }

        public ExpectFailedException(string expected, string actual) : base(string.Format("Unexpected result: Expected <{0}>, Actual <{1}>", expected, actual))
        { }
    }
}
