using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iso18013LibV2
{
    public abstract class DataGroup : DrivingLicenseFile
    {
        protected int dataGroupTag;

        protected int dataGroupLength;

        public DataGroup()
        {
        }

        /**
        * The data group tag.
        * 
        * @return the tag of the data group
        */
        public int getTag()
        {
            return dataGroupTag;
        }

        /**
         * The length of the value of the data group.
         * 
         * @return the length of the value of the data group
         */
        public int getLength()
        {
            return dataGroupLength;
        }
    }
}
