using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedNumber.Expression
{
    public class MixedNumberMultiplicationExpression : MixedNumberExpression
    {
        public MixedNumberMultiplicationExpression()
        {
            Operator = Operator.Multiply;
        }

        public override MixedNumber Evaluate()
        {
            MixedNumber leftImproperFraction = LeftOperand.ToImproperFraction();
            MixedNumber rightImproperFraction = RightOperand.ToImproperFraction();
            MixedNumber result = new MixedNumber { Numerator = leftImproperFraction.Numerator * rightImproperFraction.Numerator, Denominator = leftImproperFraction.Denominator * rightImproperFraction.Denominator };
            return result.ToLowestTerms().FromImproperFraction().ReduceFraction();
        }
    }
}
