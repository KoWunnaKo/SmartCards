﻿using SmartCardApi.Infrastructure;
using SmartCardApi.Infrastructure.Interfaces;

namespace SmartCardApi.Cryptography
{
    public class FkS : IBinary
    {
        public byte[] Bytes()
        {
            return new BinaryHex("781723860C06C2264608F919887022120B795240CB7049B01C19B33E32804F0B").Bytes();
        }
    }
}
