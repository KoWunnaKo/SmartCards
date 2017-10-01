﻿using System.Linq;
using SmartCardApi.Infrastructure;
using SmartCardApi.Infrastructure.Interfaces;

namespace SmartCardApi.Cryptography
{
    public class Kic : IBinary
    {
        private readonly IBinary _r;

        public Kic(IBinary r)
        {
            _r = r;
        }
        public byte[] Bytes()
        {
            return _r
                .Bytes()
                .Skip(16)
                .ToArray();
        }
    }
}
