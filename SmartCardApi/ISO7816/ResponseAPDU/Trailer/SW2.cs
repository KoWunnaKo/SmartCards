﻿using System.Linq;
using SmartCardApi.Infrastructure;
using SmartCardApi.Infrastructure.Interfaces;

namespace SmartCardApi.ISO7816.ResponseAPDU.Trailer
{
    public class SW2 : IBinary
    {
        private readonly IBinary _responseApduTrailer;

        public SW2(IBinary responseApduTrailer)
        {
            _responseApduTrailer = responseApduTrailer;
        }
        public byte[] Bytes()
        {
            return _responseApduTrailer
                .Bytes()
                .Skip(1)
                .Take(1)
                .ToArray();
        }
    }
}
