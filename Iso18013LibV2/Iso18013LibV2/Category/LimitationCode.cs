using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iso18013LibV2.Category
{
    class LimitationCode
    {
        static bool domesticNotInternational = false;

        /**
         * Publicly accessable map of valid limitation codes, indexed by their
         * string representation.
         */
        public static Dictionary<string, LimitationCode> limitationCodes = null;

        private string code;

        private string description;

        private bool domestic;

        private bool needSign;

        private bool needValue;

        /**
         * A private constructor that initialises the object and puts it into the
         * global map defined above.
         * 
         * @param code
         *            limitation code, e.g. "01", "03", "S01", see ISO18013
         * @param description
         *            the description of this limitation code
         * @param domestic
         *            whether the code is a domestic type
         * @param numLimit
         *            whether a numerical limit should accompany this limitation
         *            code
         */
        private LimitationCode(string code, string description, bool domestic,
                bool needSign, bool needValue)
        {
            this.code = code;
            this.description = description;
            this.domestic = domestic;
            this.needSign = needSign;
            this.needValue = needValue;
            limitationCodes.Add(code, this);
        }

        /**
         * 
         * @return the code of this limiatation
         */
        public string getCode()
        {
            return code;
        }

        /**
         * 
         * @return the description of this limiatation code
         */
        public string getDescription()
        {
            return description;
        }

        /**
         * 
         * @return whether this limiatation code is type domestic
         */
        public bool isDomestic()
        {
            return domestic;
        }

        /**
         * 
         * @return whether this code requires a sign
         */
        public bool NeedSign()
        {
            return needSign;
        }

        /**
         * 
         * @return whether this code requires a value
         */
        public bool NeedValue()
        {
            return needValue;
        }

        /**
         * @return string representation of this object
         */
        public string toString()
        {
            return code;
        }

        /**
         * @return the result of the comparison of two limitation code objects
         */
        public bool equals(Object o) 
        {
            if (o is LimitationCode) {
            return ((LimitationCode)o).code.Equals(this.code);
            } else {
                return false;
            }
        }

    }
}
