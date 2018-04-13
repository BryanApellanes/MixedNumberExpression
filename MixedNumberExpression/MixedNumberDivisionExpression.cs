using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedNumber.Expression
{
    public class MixedNumberDivisionExpression : MixedNumberExpression
    {
        public override MixedNumber Evaluate()
        {
            MixedNumber left = LeftOperand.ToImproperFraction();
            MixedNumber right = RightOperand.ToReciprocal();
            MixedNumberMultiplicationExpression multiply = new MixedNumberMultiplicationExpression { LeftOperand = left, RightOperand = right };
            return multiply.Evaluate();
        }
    }
}
