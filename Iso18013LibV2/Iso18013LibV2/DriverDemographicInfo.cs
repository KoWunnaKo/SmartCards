﻿using Iso18013LibV2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iso18013LibV2
{
    public class DriverDemographicInfo
    {
        public string familyName = string.Empty;

        public string givenNames = string.Empty;

        public string dob = string.Empty;

        public string doi = string.Empty;

        public string doe = string.Empty;

        public string country = string.Empty;

        public string authority = string.Empty;

        public string number = string.Empty;

        public DriverDemographicInfo(string familyName, string givenNames,
        string dob, string doi, string doe, string country,
        string authority, string number)
        {
            this.familyName = familyName;
            this.givenNames = givenNames;
            this.dob = dob;
            this.doi = doi;
            this.doe = doe;
            this.country = country;
            this.authority = authority;
            this.number = number;
        }


        public string toString()
        {
            return familyName + "<" + givenNames + "<" + dob + "<" + doi + "<"
                    + doe + "<" + country + "<" + authority + "<" + number;
        }

        public byte[] getEncoded() 
        {
            string[] data = { familyName, givenNames, dob, doi, doe, country,
                    authority, number };
            int total = 0;
            foreach (string s in data) {
                total += s.Length + 1;
            }
            total -= 16;
            byte[] result = new byte[total];
            int offset = 0;
            foreach (string s in data) {
                if (s != dob && s != doi && s != doe && s != country) {
                    result[offset++] = (byte)s.Length;
                    Array.Copy(Encoding.UTF8.GetBytes(s), 0, result, offset, s.Length);
                    offset += s.Length;
                } else {
                    if (s == country) {
                        Array.Copy(Encoding.UTF8.GetBytes(s), 0, result, offset, 3);
                        offset += 3;
                    } else {
                        Array.Copy(Hex.hexStringToBytes(s), 0, result,
                                offset, 4);
                        offset += 4;
                    }
                }
            }
            return result;
       }


    }
}