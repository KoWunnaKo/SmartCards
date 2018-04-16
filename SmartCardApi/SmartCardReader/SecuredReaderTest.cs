using PCSC;
using SmartCardApi.Commands;
using SmartCardApi.DataGroups.Content;
using SmartCardApi.Infrastructure;
using SmartCardApi.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCardApi.SmartCardReader
{
    public class SecuredReaderTest
    {
        public byte[] VR_Reader(string inputstring)
        {
            var cardContext = ContextFactory.Instance.Establish(SCardScope.System);

            var mrzInfo = new Symbols(inputstring);

            string[] readers = cardContext.GetReaders();

            string[] SpecReaders = (from reader in readers
                                    where reader.Contains("CK") || reader.Contains("CL")
                                    select reader).ToArray();

            if (!SpecReaders.Any()) return null;

            var conReader = new ConnectedReader(SpecReaders.First(), cardContext);

            string outString = string.Empty;

            byte[] returnBytes = null;

            //var wrapped = conReader.Content().Current;

            foreach(var reader in conReader.Content())
            {
                var screader = new SecuredReader(mrzInfo, reader);

                BacReader bcr = new BacReader(screader);

                IBinary _fid = new BinaryHex("0001");

                var _dgData = bcr.DGData(_fid).Bytes();

                var hexstr = ByteArrayToString(_dgData);

                outString = Encoding.UTF8.GetString(_dgData);

                returnBytes = _dgData;

            }

            return returnBytes;
        }

        private string ByteArrayToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }

        public byte[] IDL_ReaderDG1(string inputstring)
        {
            var cardContext = ContextFactory.Instance.Establish(SCardScope.System);

            //cardContext.GetReaders

            var mrzInfo = new Symbols(inputstring);

            string[] readers = cardContext.GetReaders();

            string[] SpecReaders = (from reader in readers
                                    where reader.Contains("CK") || reader.Contains("CL")
                                    select reader).ToArray();

            if (!SpecReaders.Any()) return null;

            var conReader = new ConnectedReader(SpecReaders.First(), cardContext);

            string outString = string.Empty;

            byte[] returnBytes = null;

            //var wrapped = conReader.Content().Current;

            foreach (var reader in conReader.Content())
            {
                var screader = new SecuredReader(mrzInfo, reader);

                BacReader bcr = new BacReader(screader);

                IBinary _fid = new BinaryHex("0001");

                var _dgData = bcr.DGData(_fid).Bytes();

                var hexstr = ByteArrayToString(_dgData);

                outString = Encoding.UTF8.GetString(_dgData);

                returnBytes = _dgData;

            }

            return returnBytes;
        }

        public byte[] IDL_ReaderDG2(string inputstring)
        {
            var cardContext = ContextFactory.Instance.Establish(SCardScope.System);

            var mrzInfo = new Symbols(inputstring);

            string[] readers = cardContext.GetReaders();

            string[] SpecReaders = (from reader in readers
                                    where reader.Contains("CK") || reader.Contains("CL")
                                    select reader).ToArray();

            if (!SpecReaders.Any()) return null;

            var conReader = new ConnectedReader(SpecReaders.First(), cardContext);

            string outString = string.Empty;

            byte[] returnBytes = null;

            //var wrapped = conReader.Content().Current;

            foreach (var reader in conReader.Content())
            {
                var screader = new SecuredReader(mrzInfo, reader);

                BacReader bcr = new BacReader(screader);

                IBinary _fid = new BinaryHex("0002");

                var _dgData = bcr.DGData(_fid).Bytes();

                var hexstr = ByteArrayToString(_dgData);

                outString = Encoding.UTF8.GetString(_dgData);

                returnBytes = _dgData;

            }

            return returnBytes;
        }

        public byte[] IDL_ReaderDG4(string inputstring)
        {
            var cardContext = ContextFactory.Instance.Establish(SCardScope.System);

            var mrzInfo = new Symbols(inputstring);

            string[] readers = cardContext.GetReaders();

            string[] SpecReaders = (from reader in readers
                                    where reader.Contains("CK") || reader.Contains("CL")
                                    select reader).ToArray();

            if (!SpecReaders.Any()) return null;

            var conReader = new ConnectedReader(SpecReaders.First(), cardContext);

            string outString = string.Empty;

            byte[] returnBytes = null;

            //var wrapped = conReader.Content().Current;

            foreach (var reader in conReader.Content())
            {
                var screader = new SecuredReader(mrzInfo, reader);

                BacReader bcr = new BacReader(screader);

                IBinary _fid = new BinaryHex("0004");

                var _dgData = bcr.DGData(_fid).Bytes();

                var hexstr = ByteArrayToString(_dgData);

                outString = Encoding.UTF8.GetString(_dgData);

                returnBytes = _dgData;

            }

            return returnBytes;
        }

        public byte[] IDL_ReaderDG5(string inputstring)
        {
            var cardContext = ContextFactory.Instance.Establish(SCardScope.System);

            var mrzInfo = new Symbols(inputstring);

            string[] readers = cardContext.GetReaders();

            string[] SpecReaders = (from reader in readers
                                    where reader.Contains("CK") || reader.Contains("CL")
                                    select reader).ToArray();

            if (!SpecReaders.Any()) return null;

            var conReader = new ConnectedReader(SpecReaders.First(), cardContext);

            string outString = string.Empty;

            byte[] returnBytes = null;

            //var wrapped = conReader.Content().Current;

            foreach (var reader in conReader.Content())
            {
                var screader = new SecuredReader(mrzInfo, reader);

                BacReader bcr = new BacReader(screader);

                IBinary _fid = new BinaryHex("0005");

                var _dgData = bcr.DGData(_fid).Bytes();

                var hexstr = ByteArrayToString(_dgData);

                outString = Encoding.UTF8.GetString(_dgData);

                returnBytes = _dgData;

            }

            return returnBytes;
        }
    }
}
