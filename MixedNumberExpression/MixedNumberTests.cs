using System;
using System.Collections.Generic;
using System.Reflection;

namespace MixedNumber.Expression
{
    public class MixedNumberTests
    {
        public static void Run()
        {
            try
            {
                CanParseMixedNumber();
                CanParseMixedNumberExpressions();
                CanFindGreatestCommonFactor();
                CanReduceToLowestTerms();
                CanFindLowestCommonFactor();
                CanEvaluateString();
                CanMultiplyMixedNumbers();
                CanDivideMixedNumbers();
                CanAddMixedNumbers();
                CanSubtractMixedNumbers();
                CanSubtractResultingInNegativeNumber();
            }
            catch (Exception ex)
            {
                Console.WriteLine("A test failed: {0}", ex.StackTrace);
            }
        }

        public static void CanParseMixedNumber()
        {
            string value = "3_1/4";
            int expectedWhole = 3;
            int expectedNumerator = 1;
            int expectedDenominator = 4;
            MixedNumber parsedValue = MixedNumber.Parse(value);

            Expect.AreEqual(expectedWhole, parsedValue.Whole, string.Format("MixedNumber.Whole was not parsed correctly: expected ({0}), actual ({1})", expectedWhole, parsedValue.Whole));
            Expect.AreEqual(expectedWhole, parsedValue.Whole, string.Format("MixedNumber.Whole was not parsed correctly: expected ({0}), actual ({1})", expectedWhole, parsedValue.Whole));
            Expect.AreEqual(expectedNumerator, parsedValue.Numerator, string.Format("MixedNumber.Numerator was not parsed correctly: ({0}), actual ({1})", expectedNumerator, parsedValue.Numerator));
            Expect.AreEqual(expectedDenominator, parsedValue.Denominator, string.Format("MixedNumber.Denominator was not parsed correctly: ({0}), actual ({1})", expectedDenominator, parsedValue.Denominator));
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} Passed");
        }
        
        public static void CanParseMixedNumberExpressions()
        {
            MixedNumberExpression multiplyExpression = MixedNumberExpression.Parse("1/2 * 3_3/4");
            Expect.IsInstanceOfType<MixedNumberMultiplicationExpression>(multiplyExpression);

            MixedNumberExpression divideExpression = MixedNumberExpression.Parse("5/8 / 2/3");
            Expect.IsInstanceOfType<MixedNumberDivisionExpression>(divideExpression);

            MixedNumberExpression addExpression = MixedNumberExpression.Parse("5/8 + 2/3");
            Expect.IsInstanceOfType<MixedNumberAdditionExpression>(addExpression);

            MixedNumberExpression subtractExpression = MixedNumberExpression.Parse("5/8 - 2/3");
            Expect.IsInstanceOfType<MixedNumberSubtractionExpression>(subtractExpression);
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} Passed");
        }
        
        public static void CanFindGreatestCommonFactor()
        {
            MixedNumber value = MixedNumber.Parse("18/24");
            int gcf = value.FindGreatestCommonFactor();
            Expect.AreEqual(6, gcf);
        }
        
        public static void CanReduceToLowestTerms()
        {
            MixedNumber value = MixedNumber.Parse("24/18");
            MixedNumber reduced = value.ToLowestTerms();
            Expect.AreEqual(4, reduced.Numerator);
            Expect.AreEqual(3, reduced.Denominator);
            Expect.AreEqual("4/3", reduced.ToString());
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} Passed");
        }
        
        public static void CanFindLowestCommonFactor()
        {
            Expect.AreEqual(4, MixedNumber.FindLowestCommonFactor(2, 4));
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} Passed");
        }
        
        public static void CanEvaluateString()
        {
            MixedNumber value = MixedNumberExpression.EvaluateString("2_3/8 + 9/8");
            Expect.AreEqual("3_1/2", value.ToString());
            Console.WriteLine($"{MethodInfo.GetCurrentMethod().Name} Passed");
        }
        
        public static void CanMultiplyMixedNumbers()
        {
            MixedNumber left = "2/5";
            MixedNumber right = "3/4";

            MixedNumberMultiplicationExpression multiply = new MixedNumberMultiplicationExpression { LeftOperand = left, RightOperand = right };
            MixedNumber result = multiply.Evaluate();
            Expect.AreEqual("3/10", result.ToString());
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} Passed");
        }
        
        public static void CanDivideMixedNumbers()
        {
            MixedNumber left = "6_1/2";
            MixedNumber right = "2_1/4";

            MixedNumberDivisionExpression multiply = new MixedNumberDivisionExpression { LeftOperand = left, RightOperand = right };
            MixedNumber result = multiply.Evaluate();
            Expect.AreEqual("2_8/9", result.ToString());
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} Passed");
        }
        
        public static void CanAddMixedNumbers()
        {
            MixedNumber left = "1_1/2";
            MixedNumber right = "2_3/4";

            MixedNumberAdditionExpression add = new MixedNumberAdditionExpression { LeftOperand = left, RightOperand = right };
            MixedNumber value = add.Evaluate();
            Expect.AreEqual("4_1/4", value.ToString());
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} Passed");
        }
        
        public static void CanSubtractMixedNumbers()
        {
            MixedNumber left = "2_3/4";
            MixedNumber right = "1_1/2";
            MixedNumberSubtractionExpression subtract = new MixedNumberSubtractionExpression { LeftOperand = left, RightOperand = right };
            MixedNumber result = subtract.Evaluate();
            Expect.AreEqual("1_1/4", result.ToString());
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} Passed");
        }

        public static void CanSubtractResultingInNegativeNumber()
        {
            MixedNumber left = "1_1/2";
            MixedNumber right = "2_3/4"; 
            MixedNumberSubtractionExpression subtract = new MixedNumberSubtractionExpression { LeftOperand = left, RightOperand = right };
            MixedNumber result = subtract.Evaluate();
            Expect.AreEqual("-1_1/4", result.ToString());
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} Passed");
        }
    }
}