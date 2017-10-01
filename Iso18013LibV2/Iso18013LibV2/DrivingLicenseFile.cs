using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iso18013LibV2
{
    public class DrivingLicenseFile
    {



        /** ISO18013 specific datagroup tag. */
        public const int EF_COM_TAG = 0x60, EF_DG1_TAG = 0x61,
            EF_DG2_TAG = 0x6B, EF_DG3_TAG = 0x6C, EF_DG4_TAG = 0x65,
            EF_DG5_TAG = 0x67, EF_DG6_TAG = 0x75, EF_DG7_TAG = 0x63,
            EF_DG8_TAG = 0x76, EF_DG9_TAG = 0x70, EF_DG10_TAG = '?',
            EF_DG11_TAG = 0x6D, EF_DG11_TAG_ALT = 0x62, EF_DG12_TAG = 0x71, EF_DG13_TAG = 0x6F,
            EF_DG14_TAG = 0x6E, EF_SOD_TAG = 0x77;



                /**
         * Corresponds to Table C.2 in ISO18013-2.
         * 
         * @param tag
         *            the first byte of the EF.
         * 
         * @return the file identifier.
         */
        public static short lookupFIDByTag(int tag)
        {
            switch (tag)
            {
                case EF_COM_TAG:
                    return DrivingLicenseService.EF_COM;
                case EF_DG1_TAG:
                    return DrivingLicenseService.EF_DG1;
                case EF_DG2_TAG:
                    return DrivingLicenseService.EF_DG2;
                case EF_DG3_TAG:
                    return DrivingLicenseService.EF_DG3;
                case EF_DG4_TAG:
                    return DrivingLicenseService.EF_DG4;
                case EF_DG5_TAG:
                    return DrivingLicenseService.EF_DG5;
                case EF_DG6_TAG:
                    return DrivingLicenseService.EF_DG6;
                case EF_DG7_TAG:
                    return DrivingLicenseService.EF_DG7;
                case EF_DG8_TAG:
                    return DrivingLicenseService.EF_DG8;
                case EF_DG9_TAG:
                    return DrivingLicenseService.EF_DG9;
                case EF_DG10_TAG:
                    return DrivingLicenseService.EF_DG10;
                case EF_DG11_TAG:
                case EF_DG11_TAG_ALT:
                    return DrivingLicenseService.EF_DG11;
                case EF_DG12_TAG:
                    return DrivingLicenseService.EF_DG12;
                case EF_DG13_TAG:
                    return DrivingLicenseService.EF_DG13;
                case EF_DG14_TAG:
                    return DrivingLicenseService.EF_DG14;
                case EF_SOD_TAG:
                    return DrivingLicenseService.EF_SOD;
                default:
                    throw new FormatException("Unknown tag ");
            }
        }


        /**
        * Corresponds to Table C.2 in ISO18013-2.
        * 
        * @param tag
        *            the first byte of the EF.
        * 
        * @return the file identifier.
        */
        public static byte lookupSIDByTag(int tag)
        {
            switch (tag)
            {
                case EF_COM_TAG:
                    return DrivingLicenseService.SF_COM;
                case EF_DG1_TAG:
                    return DrivingLicenseService.SF_DG1;
                case EF_DG2_TAG:
                    return DrivingLicenseService.SF_DG2;
                case EF_DG3_TAG:
                    return DrivingLicenseService.SF_DG3;
                case EF_DG4_TAG:
                    return DrivingLicenseService.SF_DG4;
                case EF_DG5_TAG:
                    return DrivingLicenseService.SF_DG5;
                case EF_DG6_TAG:
                    return DrivingLicenseService.SF_DG6;
                case EF_DG7_TAG:
                    return DrivingLicenseService.SF_DG7;
                case EF_DG8_TAG:
                    return DrivingLicenseService.SF_DG8;
                case EF_DG9_TAG:
                    return DrivingLicenseService.SF_DG9;
                case EF_DG10_TAG:
                    return DrivingLicenseService.SF_DG10;
                case EF_DG11_TAG:
                case EF_DG11_TAG_ALT:
                    return DrivingLicenseService.SF_DG11;
                case EF_DG12_TAG:
                    return DrivingLicenseService.SF_DG12;
                case EF_DG13_TAG:
                    return DrivingLicenseService.SF_DG13;
                case EF_DG14_TAG:
                    return DrivingLicenseService.SF_DG14;
                case EF_SOD_TAG:
                    return DrivingLicenseService.SF_SOD;
                default:
                    throw new FormatException("Unknown tag ");
            }
        }


         /**
         * Gets a data group number for a given tag.
         * 
         * @param tag
         *            the tag of a data group
         * @return the number
         */
        public static int lookupDataGroupNumberByTag(int tag)
        {
            switch (tag)
            {
                case EF_DG1_TAG:
                    return 1;
                case EF_DG2_TAG:
                    return 2;
                case EF_DG3_TAG:
                    return 3;
                case EF_DG4_TAG:
                    return 4;
                case EF_DG5_TAG:
                    return 5;
                case EF_DG6_TAG:
                    return 6;
                case EF_DG7_TAG:
                    return 7;
                case EF_DG8_TAG:
                    return 8;
                case EF_DG9_TAG:
                    return 9;
                case EF_DG10_TAG:
                    return 10;
                case EF_DG11_TAG:
                case EF_DG11_TAG_ALT:
                    return 11;
                case EF_DG12_TAG:
                    return 12;
                case EF_DG13_TAG:
                    return 13;
                case EF_DG14_TAG:
                    return 14;
                default:
                    throw new FormatException("Unknown tag ");
            }
        }


        /**
        * Gets a data group number for a given file identifier.
        * 
        * @param fid the file identifier
        * @return the number
        */
        public static int lookupDataGroupNumberByFID(short fid)
        {
            switch (fid)
            {
                case DrivingLicenseService.EF_DG1:
                    return 1;
                case DrivingLicenseService.EF_DG2:
                    return 2;
                case DrivingLicenseService.EF_DG3:
                    return 3;
                case DrivingLicenseService.EF_DG4:
                    return 4;
                case DrivingLicenseService.EF_DG5:
                    return 5;
                case DrivingLicenseService.EF_DG6:
                    return 6;
                case DrivingLicenseService.EF_DG7:
                    return 7;
                case DrivingLicenseService.EF_DG8:
                    return 8;
                case DrivingLicenseService.EF_DG9:
                    return 9;
                case DrivingLicenseService.EF_DG10:
                    return 10;
                case DrivingLicenseService.EF_DG11:
                    return 11;
                case DrivingLicenseService.EF_DG12:
                    return 12;
                case DrivingLicenseService.EF_DG13:
                    return 13;
                case DrivingLicenseService.EF_DG14:
                    return 14;
                default:
                    return -1;
            }
        }


        /**
         * Gets a tag for a given data group number
         * 
         * @param num
         *            number of the data group
         * @return associated tag
         */
        public static int lookupTagByDataGroupNumber(int num)
        {
            switch (num)
            {
                case 1:
                    return EF_DG1_TAG;
                case 2:
                    return EF_DG2_TAG;
                case 3:
                    return EF_DG3_TAG;
                case 4:
                    return EF_DG4_TAG;
                case 5:
                    return EF_DG5_TAG;
                case 6:
                    return EF_DG6_TAG;
                case 7:
                    return EF_DG7_TAG;
                case 8:
                    return EF_DG8_TAG;
                case 9:
                    return EF_DG9_TAG;
                case 10:
                    return EF_DG10_TAG;
                case 11:
                    return EF_DG11_TAG;
                case 12:
                    return EF_DG12_TAG;
                case 13:
                    return EF_DG13_TAG;
                case 14:
                    return EF_DG14_TAG;
                default:
                    throw new FormatException("Unknown DG" + num);
            }
        }
    }
}
