using System;
using System.Diagnostics;
using System.Threading;
using PCSC;
using SmartCardApi.Infrastructure;
using SmartCardApi.Infrastructure.Interfaces;
using System.Linq;

namespace SmartCardApi.SmartCardReader
{
    public class WrappedReader : IReader
    {

        private readonly ISCardReader _reader;
        private readonly int _responseApduTrailerLength = 2; // 0x02
        public WrappedReader(ISCardReader reader)
        {
            _reader = reader;
        }

        public void Dispose()
        {
           _reader.Dispose();
        }

        public IBinary Transmit(IBinary rawCommandApdu)
        {
            Debug.WriteLine("Read TreadID " + Thread.CurrentThread.ManagedThreadId);
            var receiveBuffer = new byte[1024 + _responseApduTrailerLength];
            var receivePci = new SCardPCI();
            var sendPci = SCardPCI.GetPci(SCardProtocol.T1);

            _reader.Disconnect(SCardReaderDisposition.Reset);

            string[] readers = _reader.CurrentContext.GetReaders();

            string[] SpecReaders = (from reader in readers
                                    where reader.Contains("CK") || reader.Contains("CL")
                                    select reader).ToArray();

            if (!SpecReaders.Any()) return null;

            _reader.Connect(SpecReaders.First(), SCardShareMode.Shared, SCardProtocol.Any);

            var hexStr = ByteArrayToString(rawCommandApdu.Bytes());

            var sc = _reader.Transmit(
                sendPci,
                rawCommandApdu.Bytes(),
                receivePci,
                ref receiveBuffer
            );

            if (sc != SCardError.Success)
            {
                throw new Exception("Error: " + SCardHelper.StringifyError(sc));
            }
            return new Binary(receiveBuffer);
        }

        private string ByteArrayToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }
    }
}
