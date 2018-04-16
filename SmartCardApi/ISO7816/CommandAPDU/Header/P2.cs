﻿using System.Linq;
using SmartCardApi.Infrastructure;
using SmartCardApi.Infrastructure.Interfaces;

namespace SmartCardApi.ISO7816.CommandAPDU.Header
{
    public class P2 : IBinary
    {
        private readonly IBinary _commandApduHeader;

        public P2(IBinary commandApduHeader)
        {
            _commandApduHeader = commandApduHeader;
        }
        public byte[] Bytes()
        {
            return _commandApduHeader
                .Bytes()
                .Skip(3)
                .Take(1)
                .ToArray();
        }
    }
}
