using SmartCardApi.Infrastructure;
using SmartCardApi.Infrastructure.Interfaces;
using SmartCardApi.SecureMessaging.Pipe;
using SmartCardApi.SmartCardReader;

namespace SmartCardApi.DataGroups.DG
{
    public class DGData : IBinary
    {

        private readonly IBinary _cachedSecureMessagingPipe;
        public DGData(
                IBinary fid,
                IReader securedReader
            )
        {
            _cachedSecureMessagingPipe = new Cached(
                                            new SecureMessagingPipe(
                                                    fid,
                                                    new DGDataHexLength(
                                                        fid,
                                                        securedReader
                                                    ),
                                                    securedReader
                                               )
                                           );
        }
        public byte[] Bytes()
        {
            return _cachedSecureMessagingPipe.Bytes();
        }
    }
}
