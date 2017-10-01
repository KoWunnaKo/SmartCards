using ISO7816;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iso18013LibV2
{
    public class DG1File : DataGroup
    {
        private const short DEMOGRAPHIC_INFO_TAG = 0x5F1F;

        private const short CATEGORIES_INFO_TAG = 0x7F63;

        private DriverDemographicInfo driverInfo;

        private List<CategoryInfo> categories = new List<CategoryInfo>();


        /**
         * Constructs a new file.
         * 
         * @param driverInfo
         *            the driver info object
         * @param categories
         *            the list of driving categories
         */
        public DG1File(DriverDemographicInfo driverInfo,
                List<CategoryInfo> categories)
        {
            this.driverInfo = driverInfo;
            this.categories.AddRange(categories);
        }

        public int getTag()
        {
            return EF_DG1_TAG;
        }

        /**
         * Gets the Driver information stored in this file.
         * 
         * @return the Driver information
         */
        public DriverDemographicInfo getDriverInfo()
        {
            return driverInfo;
        }

        public string toString()
        {
            return "DG1File: " + driverInfo.toString() + "\n" + "      "
                    + categories;
        }

        /**
         * Gets the list of driving categories information
         * 
         * @return category info list
         */
        public List<CategoryInfo> getCategories()
        {
            List<CategoryInfo> result = new List<CategoryInfo>();
            result.AddRange(categories);
            return result;
        }

        //public byte[] getEncoded()
        //{
        //    TLV result = new TLV();

        //    result.elems.Add(new TLV)

        //    return result.GetBytes();
        //}

 ///**
 //* Gets the BERTLV encoded form of this file.
 //*/
 //       public byte[] getEncoded() {
 //       if (isSourceConsistent) {
 //           return sourceObject.getEncoded();
 //       }
 //       try {
 //           BERTLVObject result = new BERTLVObject(EF_DG1_TAG,
 //                   new BERTLVObject(DEMOGRAPHIC_INFO_TAG, driverInfo
 //                           .getEncoded()));

 //           BERTLVObject num = new BERTLVObject(BERTLVObject.INTEGER_TYPE_TAG,
 //                   new byte[] { (byte) categories.size() });

 //           BERTLVObject cats = new BERTLVObject(CATEGORIES_INFO_TAG, num);
 //           for(CategoryInfo c : categories) {
 //               cats.addSubObject(c.getTLVObject());                
 //           }
 //           cats.reconstructLength();
 //           result.addSubObject(cats);
 //           sourceObject = result;
 //           result.reconstructLength();
 //           isSourceConsistent = true;
 //           return result.getEncoded();
 //       } catch (Exception e) {
 //           e.printStackTrace();
 //           return null;
 //       }
 //   }


    //        public static void main(String[] args) {
    //    try {
    //        byte[] testArray = new byte[] { 0x61, (byte) 0x81, (byte) 0xB1,
    //                0x5F, 0x1F, (byte) 0x81, (byte) 0x8D, 0x0F, 0x53, 0x6D,
    //                0x69, 0x74, 0x68, 0x65, 0x2D, 0x57, 0x69, 0x6C, 0x6C, 0x69,
    //                0x61, 0x6D, 0x73, 0x17, 0x41, 0x6C, 0x65, 0x78, 0x61, 0x6E,
    //                0x64, 0x65, 0x72, 0x20, 0x47, 0x65, 0x6F, 0x72, 0x67, 0x65,
    //                0x20, 0x54, 0x68, 0x6F, 0x6D, 0x61, 0x73, 0x19, 0x70, 0x03,
    //                0x01, 0x20, 0x02, 0x09, 0x15, 0x20, 0x07, 0x09, 0x30, 0x4A,
    //                0x50, 0x4E, 0x43, 0x48, 0x4F, 0x4B, 0x4B, 0x41, 0x49, 0x44,
    //                0x4F, 0x20, 0x50, 0x52, 0x45, 0x46, 0x45, 0x43, 0x54, 0x55,
    //                0x52, 0x41, 0x4C, 0x20, 0x50, 0x4F, 0x4C, 0x49, 0x43, 0x45,
    //                0x20, 0x41, 0x53, 0x41, 0x48, 0x49, 0x4B, 0x41, 0x57, 0x41,
    //                0x20, 0x41, 0x52, 0x45, 0x41, 0x20, 0x53, 0x41, 0x46, 0x45,
    //                0x54, 0x59, 0x20, 0x50, 0x55, 0x42, 0x4C, 0x49, 0x43, 0x20,
    //                0x43, 0x4F, 0x4D, 0x4D, 0x49, 0x53, 0x53, 0x49, 0x4F, 0x4E,
    //                0x11, 0x41, 0x32, 0x39, 0x30, 0x36, 0x35, 0x34, 0x33, 0x39,
    //                0x35, 0x31, 0x36, 0x34, 0x32, 0x37, 0x33, 0x58, 0x7F, 0x63,
    //                0x1D, 0x02, 0x01, 0x02, (byte) 0x87, 0x01, 0x43,
    //                (byte) 0x87, 0x15, 0x43, 0x31, 0x3B, 0x20, 0x00, 0x03,
    //                0x15, 0x3B, 0x20, 0x10, 0x03, 0x14, 0x3B, 0x39, 0x33, 0x3B,
    //                0x3C, 0x3D, 0x3B, (byte) 0x80, 0x00, };
    //        DG1File f = new DG1File(new ByteArrayInputStream(testArray));
    //        System.out.println(f.toString());
    //        System.out.println("org0: " + Hex.bytesToHexString(testArray));
    //        byte[] enc = f.getEncoded();
    //        byte[] enc2 = f.getEncoded();
    //        System.out.println("enc1: " + Hex.bytesToHexString(enc));
    //        System.out.println("enc2: " + Hex.bytesToHexString(enc2));
    //        System.out.println("Compare1: " + Arrays.equals(testArray, enc));
    //        System.out.println("Compare2: " + Arrays.equals(enc, enc2));
    //    } catch (Exception e) {
    //        e.printStackTrace();
    //    }
    //}

    }
}
