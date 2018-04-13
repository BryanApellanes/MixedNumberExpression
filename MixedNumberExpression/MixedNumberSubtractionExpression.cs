using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedNumber.Expression
{
    public class MixedNumberSubtractionExpression : MixedNumberExpression
    {
        public MixedNumberSubtractionExpression()
        {
            Operator = Operator.Subtract;
        }

        public override MixedNumber Evaluate()
        {
            int lowestCommonFactor = MixedNumber.FindLowestCommonFactor(LeftOperand.Denominator, RightOperand.Denominator);
            int leftMultiplier = lowestCommonFactor / LeftOperand.Denominator;
            int rightMultiplier = lowestCommonFactor / RightOperand.Denominator;

            MixedNumber leftImproperFraction = LeftOperand.ToImproperFraction();
            leftImproperFraction.Numerator = leftImproperFraction.Numerator * leftMultiplier;
            
            MixedNumber rightImproperFraction = RightOperand.ToImproperFraction();
            rightImproperFraction.Numerator = rightImproperFraction.Numerator * rightMultiplier;            

            MixedNumber result = new MixedNumber
            {
                Numerator = leftImproperFraction.Numerator - rightImproperFraction.Numerator,
                Denominator = LeftOperand.Denominator * leftMultiplier // denominator will be the same if we use the left or the right with associated multiplier
            };

            return result.ToLowestTerms().FromImproperFraction().ReduceFraction();
        }
    }
}
