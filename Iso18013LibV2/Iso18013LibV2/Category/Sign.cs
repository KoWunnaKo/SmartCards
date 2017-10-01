using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iso18013LibV2.Category
{
    public class Sign
    {
        /**
 * Publicly accessable map of valid signs, indexed by their string
 * representation.
 */
        public static Dictionary<string, Sign> signs = null;

        private string sign;

        /**
         * A private constructor that initialises the object and puts it into the
         * global map defined above.
         * 
         * @param sign
         *            the string with the sign
         */
        private Sign(String sign)
        {
            this.sign = sign;
            signs.Add(sign, this);
        }

        /**
         * 
         * @return the sign embedded in this object
         */
        public String getSign()
        {
            return sign;
        }

        /**
         * @return string representation of this object
         */
        public String toString()
        {
            return sign;
        }

        /**
         * @return the result of the comparison of two sign objects
         */
        public bool equals(Object o) {
        if (o is Sign) {
            return ((Sign) o).sign.Equals(this.sign);
        } else {
            return false;
        }
    }
    }
}
