using Iso18013LibV2.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iso18013LibV2
{
    public class CategoryInfo
    {
        static bool properISOFormat = false;

        private string SDF = "yyyyMMdd";

        private static short CATEGORY_TAG = 0x87;

        /** Raw contents of this category info. */
        private byte[] contents = null;

        /** The actual parsed contents of this catecory info. */
        private DrivingCategory category;

        private DateTime doi;

        private DateTime doe;

        private List<LimitationCode> code;

        private Sign sign;

        private string value;






    }
}
