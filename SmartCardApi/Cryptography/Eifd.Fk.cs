﻿using SmartCardApi.Infrastructure;
using SmartCardApi.Infrastructure.Interfaces;

namespace SmartCardApi.Cryptography
{
    public class FkEifd : IBinary
    {
        public byte[] Bytes()
        {
            return new BinaryHex("72C29C2371CC9BDB65B779B8E8D37B29ECC154AA56A8799FAE2F498F76ED92F2").Bytes();
        }
    }
}
