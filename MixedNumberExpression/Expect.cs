using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedNumber.Expression
{
    public class Expect
    {
        /// <summary>
        /// Checks if the specified objects are equal using the Equals() method.
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        public static void AreEqual(object expected, object actual, string failureMessage = null)
        {
            if (((expected == null && actual != null) ||
                (actual == null && expected != null)) ||
                (expected != null && !expected.Equals(actual))
                )
            {
                if (string.IsNullOrEmpty(failureMessage))
                {
                    string expectString = expected == null ? "null" : expected.ToString();
                    string actualString = actual == null ? "null" : actual.ToString();
                    throw new ExpectFailedException(expectString, actualString);
                }
                else
                {
                    throw new ExpectFailedException(failureMessage);
                }
            }
        }

        /// <summary>
        /// Asserts that the object is an instance of the specified generic type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToCheck"></param>
        /// <param name="failureMessage"></param>
        public static void IsInstanceOfType<T>(object objectToCheck, string failureMessage = "")
        {
            if (!typeof(T).IsInstanceOfType(objectToCheck))
            {
                if (string.IsNullOrWhiteSpace(failureMessage))
                {
                    throw new ExpectFailedException(typeof(T).Name, objectToCheck?.GetType()?.Name ?? "null");
                }
                else
                {
                    throw new ExpectFailedException(failureMessage);
                }
            }
        }
    }
}
