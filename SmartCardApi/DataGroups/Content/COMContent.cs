﻿using System.Linq;
using System.Text;
using SmartCardApi.Infrastructure;
using SmartCardApi.Infrastructure.Interfaces;

namespace SmartCardApi.DataGroups.Content
{
    public class COMContent
    {
        private readonly IBerTLV _comDataBerTLV;
        public COMContent(IBinary comData)
        {
            _comDataBerTLV = new BerTLV(comData);
        }

        public string LDSVersion
        {
            get
            {
                return Encoding.ASCII.GetString(
                    new BinaryHex(_comDataBerTLV.Data.First(tvl => tvl.T == "5F01").V).Bytes()
                );
            }
        }

        public string UnicodeVersion
        {
            get
            {
                return Encoding.ASCII.GetString(
                    new BinaryHex(_comDataBerTLV.Data.First(tvl => tvl.T == "5F36").V).Bytes()
                );
            }
        }

        public string TagList
        {
            get
            {
                return Encoding.ASCII.GetString(
                    new BinaryHex(_comDataBerTLV.Data.First(tvl => tvl.T == "5C").V).Bytes()
                );
            }
        }
    }
}
