using System;
using System.Collections.Generic;
using System.Text;

namespace MixedNumber.Expression
{
    public class MixedNumber
    {        
        public MixedNumber()
        {
            Whole = 0;
            Numerator = 0;
            Denominator = 1;
        }

        public static implicit operator MixedNumber(string value)
        {
            return Parse(value);
        }

        public int Whole { get; set; }
        public int Numerator { get; set; }
        public int Denominator { get; set; }

        /// <summary>
        /// Clone the MixedNumber.
        /// </summary>
        /// <returns></returns>
        public MixedNumber Clone() // defined Clone() to help avoid possible bugs due to passing references
        {
            return new MixedNumber { Whole = Whole, Numerator = Numerator, Denominator = Denominator };
        }

        /// <summary>
        /// Parse the specified value as a MixedNumber.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static MixedNumber Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("value");
            }

            string[] componentsOfValue = value.Split('_', '/');
            MixedNumber result = new MixedNumber();

            // if no delimiters were specified, we assume that the value is the whole number
            if (componentsOfValue.Length == 1) 
            {
                result.Whole = int.Parse(componentsOfValue[0]);
                return result;
            }
            if(componentsOfValue.Length == 2)
            {
                // if there are only two values in the array after splitting and the original 
                // value contains an underscore, we assume the value at index 0 is the whole 
                // number and the value at index 1 is the numerator.  The denominator will 
                // have it's default value of 1.
                if (value.Contains("_")) 
                {
                    result.Whole = int.Parse(componentsOfValue[0]);
                    result.Numerator = int.Parse(componentsOfValue[1]);
                    return result;
                }
                // otherwise there must have been a "/" character meaning that the numerator is at 
                // index 0 and the denominator at index 1.
                result.Numerator = int.Parse(componentsOfValue[0]);
                result.Denominator = int.Parse(componentsOfValue[1]);
            }
            if(componentsOfValue.Length == 3)
            {
                result.Whole = int.Parse(componentsOfValue[0]);
                result.Numerator = int.Parse(componentsOfValue[1]);
                result.Denominator = int.Parse(componentsOfValue[2]);
            }
            return result;
        }
        
        /// <summary>
        /// Get the reciprocal of the MixedNumber
        /// after converting to improper fraction.
        /// </summary>
        /// <returns></returns>
        public MixedNumber ToReciprocal()
        {
            MixedNumber mixedNumber = this.Clone();
            if(mixedNumber.Whole > 0)
            {
                mixedNumber = mixedNumber.ToImproperFraction();
            }
            return new MixedNumber
            {
                Numerator = mixedNumber.Denominator,
                Denominator = mixedNumber.Numerator
            };
        }

        /// <summary>
        /// Convert the MixedNumber to it's equivalent improper fraction.
        /// </summary>
        /// <returns></returns>
        public MixedNumber ToImproperFraction()
        {
            int newNumerator = (Whole * Denominator) + Numerator;
            return new MixedNumber { Numerator = newNumerator, Denominator = Denominator };
        }

        /// <summary>
        /// Reduce only the numerator and denominator to lowest
        /// terms leaving the whole number as is.
        /// </summary>
        /// <returns></returns>
        public MixedNumber ReduceFraction()
        {
            MixedNumber result = this.Clone();
            MixedNumber fraction = new MixedNumber { Numerator = result.Numerator, Denominator = result.Denominator }.ToLowestTerms();
            result.Numerator = fraction.Numerator;
            result.Denominator = fraction.Denominator;
            return result;
        }

        /// <summary>
        /// Reduce this MixedNumber to it's lowest term after 
        /// converting to an improper fraction.
        /// </summary>
        /// <returns></returns>
        public MixedNumber ToLowestTerms()
        {
            MixedNumber mixedNumber = this.Clone();
            if(mixedNumber.Whole > 0)
            {
                mixedNumber = mixedNumber.ToImproperFraction();
            }
            int greatestCommonFactor = mixedNumber.FindGreatestCommonFactor();
            return new MixedNumber
            {
                Whole = Whole,
                Numerator = Numerator / greatestCommonFactor,
                Denominator = Denominator / greatestCommonFactor
            };
        }

        /// <summary>
        /// Extracts whole numbers if the Numerator is greater
        /// than the Denominator.
        /// </summary>
        /// <returns></returns>
        public MixedNumber FromImproperFraction()
        {
            MixedNumber mixedNumber = this.Clone();
            if(mixedNumber.Numerator > mixedNumber.Denominator || mixedNumber.Numerator * -1 > mixedNumber.Denominator)
            {
                int numerator = mixedNumber.Numerator;
                int denominator = mixedNumber.Denominator;
                int whole = (int)Math.Floor((decimal)(numerator / denominator));
                int newNumerator = numerator % denominator;
                mixedNumber.Whole = mixedNumber.Whole + whole;
                mixedNumber.Numerator = newNumerator;                
            }
            return mixedNumber;
        }

        public override string ToString()
        {
            if (Whole != 0)
            {
                int numerator = Whole < 0 ? Numerator * -1 : Numerator;
                return $"{Whole}_{numerator}/{Denominator}";
            }
            return $"{Numerator}/{Denominator}";
        }

        public int FindGreatestCommonFactor()
        {
            return FindGreatestCommonFactor(Numerator, Denominator);
        }

        public static int FindLowestCommonFactor(int valOne, int valTwo)
        {
            if(valOne == valTwo)
            {
                return valOne;
            }
            return (valOne * valTwo) / FindGreatestCommonFactor(valOne, valTwo);
        }

        public static List<int> FindPrimeFactors(int number)
        {
            List<int> factors = new List<int>();
            while (number % 2 == 0)
            {
                factors.Add(2);
                number /= 2;
            }

            int factor = 3;
            while (factor * factor <= number)
            {
                if (number % factor == 0)
                {
                    factors.Add(factor);
                    number /= factor;
                }
                else
                {
                    factor += 2;
                }
            }

            if (number > 1)
            {
                factors.Add(number);
            }

            return factors;
        }

        public static int FindGreatestCommonFactor(int valOne, int valTwo)
        {
            List<int> valOneFactors = FindPrimeFactors(valOne);
            List<int> valTwoFactors = FindPrimeFactors(valTwo);
            HashSet<int> common = new HashSet<int>();
            foreach (int outerVal in valOneFactors)
            {
                foreach (int innerVal in valTwoFactors)
                {
                    if (outerVal == innerVal)
                    {
                        common.Add(outerVal);
                    }
                }
            }

            int result = 1;
            foreach (int factor in common)
            {
                result *= factor;
            }
            return result;
        }
    }
}
