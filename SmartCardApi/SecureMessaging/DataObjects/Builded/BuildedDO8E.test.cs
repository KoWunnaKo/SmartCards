﻿using NUnit.Framework;
using SmartCardApi.Cryptography;
using SmartCardApi.Infrastructure;

namespace SmartCardApi.SecureMessaging.DataObjects.Builded
{
    [TestFixture]
    public class BuildedDO8ETests
    {
        [Test]
        [TestCase("8E08BF8B92D635FF24F8", "00A4020C02011E", "887022120C06C227")]
        [TestCase("8E08ED6705417E96BA55", "00B0000004", "887022120C06C229")]
        [TestCase("8E082EA28A70F3C7B535", "00B0000412", "887022120C06C22B")]
        public void Build_DO8E_from_rawCommandApdu(string exc, string rawCommandApdu, string incrementedSSC)
        {
            Assert.AreEqual(
                    exc,
                    new Hex(
                        new BuildedDO8E(
                            new BinaryHex(rawCommandApdu), // rawCommandApdu
                            new BinaryHex(incrementedSSC), // incrementedSSC
                            new FkKSmac(), 
                            new FkKSenc()
                        )
                    ).ToString()
                );
        }
    }
}
