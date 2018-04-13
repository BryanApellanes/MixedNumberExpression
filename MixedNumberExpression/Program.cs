using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedNumber.Expression
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (args[0] == "-t")
                {
                    MixedNumberTests.Run();
                }
                else
                {
                    string expression = string.Join(" ", args);
                    Console.WriteLine(MixedNumberExpression.EvaluateString(expression));
                }
            }
            else
            {
                Usage();
            }
            Console.Write("press enter to end");
            Console.ReadLine();
        }

        static void Usage()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("[whole]_[numerator]/[denominator] [operator] [whole]_[numerator]/[denominator]");
            Console.WriteLine("\twhere [operator] is one of * / + -");
        }
    }
}
