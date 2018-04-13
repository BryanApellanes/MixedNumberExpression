using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedNumber.Expression
{
    public abstract class MixedNumberExpression
    {
        static MixedNumberExpression()
        {
            OperatorToString = new Dictionary<Operator, string>
            {
                { Operator.Invalid, "INVALID" },
                { Operator.Multiply, "*" },
                { Operator.Divide, "/" },
                { Operator.Add, "+" },
                { Operator.Subtract, "-" }
            };

            StringToOperator = new Dictionary<string, Operator>
            {
                { "INVALID", Operator.Invalid },
                { "*", Operator.Multiply },
                { "/", Operator.Divide },
                { "+", Operator.Add },
                { "-", Operator.Subtract }
            };

            Parsers = new Dictionary<Operator, Func<string, string, string, MixedNumberExpression>>
            {
                { Operator.Invalid, (left, op, right) => throw new InvalidOperationException("Invalid operator") },
                { Operator.Multiply, (left, op, right) =>  Create<MixedNumberMultiplicationExpression>(left, op, right)},
                { Operator.Divide, (left, op, right) =>  Create<MixedNumberDivisionExpression>(left, op, right)},
                { Operator.Add, (left, op, right) =>  Create<MixedNumberAdditionExpression>(left, op, right)},
                { Operator.Subtract, (left, op, right) =>  Create<MixedNumberSubtractionExpression>(left, op, right)},
            };
        }

        public MixedNumber LeftOperand { get; set; }
        public Operator Operator { get; protected set; }
        public MixedNumber RightOperand { get; set; }

        public abstract MixedNumber Evaluate();

        /// <summary>
        /// Evaluate the specified expression.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static MixedNumber EvaluateString(string expression)
        {
            return Parse(expression).Evaluate();
        }

        /// <summary>
        /// Parse the specified expression.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static MixedNumberExpression Parse(string expression)
        {
            string[] componentsOfValue = expression.Split(' ');
            if (componentsOfValue.Length != 3)
            {
                throw new ArgumentException(string.Format("Unrecognized mixed number expression specified: {0}", expression));
            }
            return Parsers[StringToOperator[componentsOfValue[1]]](componentsOfValue[0], componentsOfValue[1], componentsOfValue[2]);
        }

        public static T Create<T>(string leftOperand, string op, string rightOperand) where T : MixedNumberExpression, new()
        {
            T result = new T
            {
                LeftOperand = MixedNumber.Parse(leftOperand),
                Operator = StringToOperator[op],
                RightOperand = MixedNumber.Parse(rightOperand)
            };
            return result;
        }

        public override string ToString()
        {
            return $"{LeftOperand.ToString()} {OperatorToString[Operator]} {RightOperand.ToString()}";
        }
        
        protected static Dictionary<Operator, Func<string, string, string, MixedNumberExpression>> Parsers { get; set; }
        protected static Dictionary<Operator, string> OperatorToString { get; set; }
        protected static Dictionary<string, Operator> StringToOperator { get; set; }
    }
}
