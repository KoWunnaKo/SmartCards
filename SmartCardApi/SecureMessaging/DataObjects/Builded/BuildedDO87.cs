﻿using SmartCardApi.Cryptography;
using SmartCardApi.Infrastructure;
using SmartCardApi.Infrastructure.Interfaces;
using SmartCardApi.ISO7816.CommandAPDU.Body;
using SmartCardApi.SecureMessaging.DataObjects.DO;

namespace SmartCardApi.SecureMessaging.DataObjects.Builded
{
    public class BuildedDO87 : IBinary
    {
        private readonly IBinary _kSenc;
        private readonly IBinary _rawCommandApdu;
        public BuildedDO87(
                IBinary rawCommandApdu,
                IBinary kSenc
            )
        {
            _kSenc = kSenc;
            _rawCommandApdu = rawCommandApdu;
        }

        public byte[] Bytes()
        {
            //If no Data is available, leave building DO ‘87’ out
            var data = new CommandApduData(
                            new CommandApduBody(_rawCommandApdu)
                        );
            if (new BytesCount(data).Is(0))
            {
                return new Binary().Bytes();
            }
            return new DO87(
                        new EncryptedCommandApduData(
                            _kSenc,
                            new Padded(data)
                        )
                   ).Bytes();
        }
    }
}
