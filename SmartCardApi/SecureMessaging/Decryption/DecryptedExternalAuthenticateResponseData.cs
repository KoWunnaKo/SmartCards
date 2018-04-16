﻿using SmartCardApi.Cryptography;
using SmartCardApi.Infrastructure;
using SmartCardApi.Infrastructure.Interfaces;

namespace SmartCardApi.SecureMessaging.Decryption
{
    public class DecryptedExternalAuthenticateResponseData : IBinary
    {
        private readonly IBinary _externalAuthRespData;
        private readonly ISymbols _mrzInformation;

        public DecryptedExternalAuthenticateResponseData(
                IBinary externalAuthRespData,
                ISymbols mrzInformation
            )
        {
            _externalAuthRespData = externalAuthRespData;
            _mrzInformation = mrzInformation;
        }
        public byte[] Bytes()
        {
            return new TripleDES(
                    new Kenc(_mrzInformation),
                    new Eic(_externalAuthRespData)
                )
                .Decrypted()
                .Bytes();
        }
    }
}
