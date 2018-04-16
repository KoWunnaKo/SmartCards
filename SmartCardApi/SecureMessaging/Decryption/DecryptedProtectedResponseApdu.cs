﻿using SmartCardApi.Cryptography;
using SmartCardApi.Infrastructure;
using SmartCardApi.Infrastructure.Interfaces;
using SmartCardApi.SecureMessaging.DataObjects.Extracted;

namespace SmartCardApi.SecureMessaging.Decryption
{
    public class DecryptedProtectedResponseApdu : IBinary
    {
        private readonly IBinary _protectedResponseApdu;
        private readonly IBinary _kSenc;

        public DecryptedProtectedResponseApdu(
                IBinary protectedResponseApdu,
                IBinary kSenc
            )
        {
            _protectedResponseApdu = protectedResponseApdu;
            _kSenc = kSenc;
        }
        public byte[] Bytes()
        {
            return new WithoutPad(
                        new TripleDES(
                            _kSenc,
                            new ExtractedDO87(_protectedResponseApdu)
                                .EncryptedData()
                        ).Decrypted()
                    ).Bytes();
        }
    }
}
