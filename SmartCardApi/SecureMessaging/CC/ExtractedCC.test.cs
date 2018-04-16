﻿using NUnit.Framework;
using SmartCardApi.Cryptography;
using SmartCardApi.Infrastructure;

namespace SmartCardApi.SecureMessaging.CC
{
    [TestFixture]
    public class ExtractedCCTests
    {
        [Test]
        [TestCase("FA855A5D4C50A8ED", "887022120C06C228", "990290008E08FA855A5D4C50A8ED9000")]
        [TestCase("AD55CC17140B2DED", "887022120C06C22A", "8709019FF0EC34F9922651990290008E08AD55CC17140B2DED9000")]
        [TestCase("C8B2787EAEA07D74", "887022120C06C22C", "871901FB9235F4E4037F2327DCC8964F1F9B8C30F42C8E2FFF224A990290008E08C8B2787EAEA07D749000")]
        public void ExtractCC_from_protectedResponseApdu(string exc, string incrementedSsc, string protectedResponseApdu)
        {
            Assert.AreEqual(
                    exc,
                    new Hex(
                        new ExtractedCC(
                            new BinaryHex(incrementedSsc),
                            new FkKSmac(), 
                            new BinaryHex(protectedResponseApdu)
                        )
                    ).ToString()
                );
        }
    }
}
