﻿using System;
using System.Linq;
using SmartCardApi.Infrastructure;
using SmartCardApi.Infrastructure.Interfaces;
using SmartCardApi.SecureMessaging.CC;
using SmartCardApi.SecureMessaging.DataObjects.Extracted;

namespace SmartCardApi.SecureMessaging
{
    public class VerifiedProtectedResponseApdu : IBinary
    {
        private readonly IBinary _responseApdu;
        private readonly IBinary _incrementedSsc;
        private readonly IBinary _kSmac;

        public VerifiedProtectedResponseApdu(
                IBinary responseApdu,
                IBinary incrementedSsc,
                IBinary kSmac
            )
        {
            _responseApdu = responseApdu;
            _incrementedSsc = incrementedSsc;
            _kSmac = kSmac;
        }
        public byte[] Bytes()
        {
            try
            {
                var extractedCC = new ExtractedCC(
                        _incrementedSsc,
                        _kSmac,
                        _responseApdu
                    ).Bytes();
                var encryptedDO8E = new ExtractedDO8E(_responseApdu)
                                            .EncryptedData()
                                            .Bytes();
                if (
                    !extractedCC
                        .SequenceEqual(encryptedDO8E)
                )
                {
                    throw new Exception(
                        String.Format(
                            "CC not equal of DO‘8E’ of RAPDU\n{0} != {1}",
                            new Hex(new Binary(extractedCC)),
                            new Hex(new Binary(encryptedDO8E))
                        )
                    );
                }
                else
                {
                    //Console.WriteLine(
                    //        "CC equal of DO‘8E’ of RAPDU\n{0} == {1}",
                    //        new Hex(new Binary(extractedCC)),
                    //        new Hex(new Binary(encryptedDO8E))
                    //    );
                    return _responseApdu.Bytes();
                }
            }
            catch(Exception ex)
            {
                return new byte[] { 0x00 };
            }

        }
    }
}
