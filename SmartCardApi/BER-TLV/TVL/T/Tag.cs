using System.Linq;
using SmartCardApi.Infrastructure;
using SmartCardApi.Infrastructure.Interfaces;
using System;

namespace SmartCardApi.TVL.T
{
  
    public class Tag : IRest, IBinary
    {
        private readonly IBinary _berTlv;
        private readonly IBinary _tag;
        public Tag(IBinary berTlv)
        {
            _berTlv = berTlv;
            _tag = new CombinedBinaries(
                        new Binary(berTlvFirstByte()),
                        new TagSubsequentBytes(_berTlv)
                    );
        }
        public byte[] Bytes()
        {
            return _tag.Bytes();
        }

        private byte berTlvFirstByte()
        {
            try
            {
                return _berTlv.Bytes().First();
            }
            catch(Exception ex)
            {
                return 0x00;
            }
        }

        public IBinary Rest()
        {
            return new Binary(
                        _berTlv
                            .Bytes()
                            .Skip(new BytesCount(_tag).Value())
                   );
        }
    }
}
