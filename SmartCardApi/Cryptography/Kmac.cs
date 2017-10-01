﻿using System.Linq;
using SmartCardApi.Infrastructure;
using SmartCardApi.Infrastructure.Interfaces;

namespace SmartCardApi.Cryptography
{
    public class Kmac : IBinary
    {
        private readonly IBinary _c = new BinaryHex("00000002");
        private readonly IBinary _kSeed;

        public Kmac(IBinary kSeed)
        {
            _kSeed = kSeed;
        }

        public byte[] Bytes()
        {
            return new AdjustedParity(
                        new SHA1(
                            new D(_kSeed, _c)
                        )
                        .Bytes()
                        .Skip(0)
                        .Take(16)
                        .ToArray()
                    )
                    .Bytes();
        }
    }
}
