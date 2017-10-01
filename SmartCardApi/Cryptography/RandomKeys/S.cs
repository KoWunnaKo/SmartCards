﻿using SmartCardApi.Infrastructure;
using SmartCardApi.Infrastructure.Interfaces;

namespace SmartCardApi.Cryptography.RandomKeys
{
    public class S : IBinary
    {
        private readonly IBinary _RNDifd;
        private readonly IBinary _RNDic;
        private readonly IBinary _Kifd;
        public S(
                IBinary rndIfd,
                IBinary rndIc,
                IBinary kIfd
            )
        {
            _Kifd = kIfd;
            _RNDic = rndIc;
            _RNDifd = rndIfd;
        }

        public byte[] Bytes()
        {
            return new CombinedBinaries(
                    _RNDifd,
                    _RNDic,
                    _Kifd
                ).Bytes();
        }
    }
}
