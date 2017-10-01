﻿using System.Linq;
using SmartCardApi.Infrastructure;
using SmartCardApi.Infrastructure.Interfaces;
using SmartCardApi.TVL.Cached;
using System;

namespace SmartCardApi.TVL.L
{
    /// <summary>
    /// ISO 7816-4 Annex D.3: Length field
    /// http://www.cardwerk.com/smartcards/smartcard_standard_ISO7816-4_annex-d.aspx 
    /// </summary>
    public class Len : IBinary
    {
        private readonly IBinary _berTlv;
        private readonly IBinary _cachedTag;
        private readonly byte _b8_one = 0x80; // 0b1000 0b000
        
        public Len(IBinary berTvl)
             : this(berTvl, new CachedTag(berTvl))
        {
        }
        public Len(
                IBinary berTlv,
                IBinary tag
            )
        {
            _berTlv = berTlv;
            _cachedTag = tag;
        }

        public byte[] Bytes()
        {
            try
            {
                var berTlvWithoutTag = _berTlv
                            .Bytes()
                            .Skip(
                                new BytesCount(_cachedTag)
                                        .Value()
                            );

                var firstByte = berTlvWithoutTag.First();

                if (IsLongFormOfLen(firstByte))
                {
                    return new CombinedBinaries(
                                new ShortLen(
                                    new Binary(berTlvWithoutTag)
                                ),
                                new LongLen(
                                    new Binary(berTlvWithoutTag)
                                )
                            ).Bytes();
                }
                else
                {
                    return new ShortLen(
                                new Binary(berTlvWithoutTag)
                            ).Bytes();
                }
            }
            catch(Exception ex)
            {
                return new byte[] { 0x00};
            }

        }

        
        private bool IsLongFormOfLen(byte firstByteOfBerTvlWithoutTag)
        {
            return (firstByteOfBerTvlWithoutTag & _b8_one) == _b8_one;
        }
    }
}
