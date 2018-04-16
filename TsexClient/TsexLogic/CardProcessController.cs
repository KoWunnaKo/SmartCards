using CardAPILib.InterfaceCL;
using GemCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TsexClient.Model;

namespace TsexClient.TsexLogic
{
    public class CardProcessController
    {
        private CardNative _card;
        private List<Thread> threadList { get; set; }

        public List<TerminalModel> terminalsList { get; set; }

        private string[] ReadersList { get; set; }

        private int counter = 0;

        public CardProcessController()
        {
            threadList = new List<Thread>();

            _card = new CardNative();

            terminalsList = new List<TerminalModel>();
        }

        public void StartPros()
        {
            string[] readers;

            _card.OnCardInserted += _card_OnCardInserted;
            _card.OnCardRemoved += _card_OnCardRemoved;

            readers = _card.ListReaders();

            ReadersList = (from reader in readers
                           where reader.Contains("CK") || reader.Contains("CL")
                           select reader).ToArray();

            if (ReadersList.Length == 0)
            {
                throw new ApplicationException("Отсуствует ридер или не установленны драйвера!!! Проблемы с устройством");
            }


            //New version 22.01.2018
            foreach (var reader in ReadersList)
            {
                var term = new TerminalModel();

                term.terminalName = reader;
                term.state = TerminalState.Empty;

                terminalsList.Add(term);

                _card.StartCardEventsMulti(reader);
            }
        }

        private void _card_OnCardRemoved(string reader)
        {
            //throw new NotImplementedException();
        }

        private void _card_OnCardInserted(string reader)
        {
            var termf = terminalsList.FirstOrDefault(z => z.terminalName == reader);

            if (termf != null)
            {
                termf.state = TerminalState.CardInserted;
            }
            else
            {
                throw new ApplicationException("Undefined reader");
            }

            var m_threadx = new Thread(new ParameterizedThreadStart(ReadCardApplet));
            threadList.Add(m_threadx);
            m_threadx.SetApartmentState(ApartmentState.STA);
            m_threadx.Start(reader);
        }

        public void ReadCardApplet(object reader)
        {
            string m_reader = (string)reader;

            CardNative _cardx = new CardNative();

            _cardx.Connect(m_reader, SHARE.Shared, PROTOCOL.T0orT1);

            while (true)
            {
                counter++;

                Console.WriteLine("Reader: " + reader);
                Console.WriteLine(counter);

                if (CallApduCommandLe(0x00, 0xA4, 0x04, 0x00, new byte[07] { 0xA0, 0x00, 0x00, 0x02, 0x48, 0x02, 0x00 }, 28, m_reader, _cardx) != 0)
                {
                    Console.WriteLine("Error ");
                }
            }
        }

        public void OpenTsex(object reader)
        {
            string m_reader = (string)reader;

            CardNative _cardx = new CardNative();

            _cardx.Connect(m_reader, SHARE.Shared, PROTOCOL.T0orT1);

            var AtrBytes = CallApduCommandLeData(0xFF, 0xCA, 0x00, 0x00, null, 5, _cardx, m_reader);

            StringBuilder hex1 = new StringBuilder((5) * 2);
            foreach (byte b in AtrBytes)
                hex1.AppendFormat("{0:X2}", b);
            var uid_temp = hex1.ToString();

            TsexCommands(m_reader, _cardx);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Cla"></param>
        /// <param name="Ins"></param>
        /// <param name="P1"></param>
        /// <param name="P2"></param>
        /// <param name="data"></param>
        /// <param name="_recieveLength"></param>
        /// <param name="readerInfo"></param>
        /// <returns></returns>
        private int CallApduCommandLe(byte Cla, byte Ins, byte P1, byte P2, byte[] data, uint _recieveLength, string readerInfo, CardNative _cardx)
        {
            APDUResponse apduResp;

            const ushort SC_OK = 0x9000;
            const byte SC_PENDING = 0x9F;

            APDUCommand apduSize5 = new APDUCommand(Cla, Ins, P1, P2, null, 0);

            APDUParam apduParam5 = new APDUParam();

            apduParam5.Data = data;

            apduSize5.Update(apduParam5);

            //_card.Connect(readerInfo, SHARE.Shared, PROTOCOL.T0orT1);

            apduResp = _cardx.TransmitLe(apduSize5, _recieveLength);
            if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
            {

                return -1;
            }

            StringBuilder hex1 = new StringBuilder((5) * 2);
            foreach (byte b in apduResp.Data)
                hex1.AppendFormat("{0:X2}", b);
            var uid_temp = hex1.ToString();

            Console.WriteLine(uid_temp);

            return 0;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Cla"></param>
        /// <param name="Ins"></param>
        /// <param name="P1"></param>
        /// <param name="P2"></param>
        /// <param name="data"></param>
        /// <param name="_recieveLength"></param>
        /// <param name="readerInfo"></param>
        /// <returns></returns>
        private int CallApduCommandLe(byte Cla, byte Ins, byte P1, byte P2, byte Le, uint _recieveLengt, string readerInfo, CardNative _cardx)
        {
            APDUResponse apduResp;

            const ushort SC_OK = 0x9000;
            const byte SC_PENDING = 0x9F;

            APDUCommand apduSize5 = new APDUCommand(Cla, Ins, P1, P2, null, Le);

            APDUParam apduParam5 = new APDUParam();

            apduParam5.Data = null;

            apduSize5.Update(apduParam5);

            //_card.Connect(readerInfo, SHARE.Shared, PROTOCOL.T0orT1);

            apduResp = _cardx.TransmitLe(apduSize5, _recieveLengt);
            if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
            {

                return -1;
            }

            StringBuilder hex1 = new StringBuilder((5) * 2);
            foreach (byte b in apduResp.Data)
                hex1.AppendFormat("{0:X2}", b);
            var uid_temp = hex1.ToString();

            Console.WriteLine(uid_temp);

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Cla"></param>
        /// <param name="Ins"></param>
        /// <param name="P1"></param>
        /// <param name="P2"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private int CallApduCommand(byte Cla, byte Ins, byte P1, byte P2, byte[] data, string readerInfo, CardNative _cardx)
        {
            APDUResponse apduResp;

            const ushort SC_OK = 0x9000;
            const byte SC_PENDING = 0x9F;

            APDUCommand apduSize5 = new APDUCommand(Cla, Ins, P1, P2, null, 0);

            APDUParam apduParam5 = new APDUParam();

            apduParam5.Data = data;

            apduSize5.Update(apduParam5);

            //_card.Connect(readerInfo, SHARE.Shared, PROTOCOL.T0orT1);

            apduResp = _cardx.Transmit(apduSize5);
            if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
            {

                return -1;
            }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private int TsexCommands(object reader, CardNative _cardx)
        {
            var ext = new ExternalAuthentificate(_cardx);

            string m_reader = (string)reader;

            if (CallApduCommand(0x00, 0xA4, 0x04, 0x00, new byte[16] { 0xD0, 0xAD, 0x56, 0x5F, 0xDA, 0x50, 0xC0, 0x53, 0xBD, 0x61, 0x0B, 0xAB, 0x5F, 0x71, 0x10, 0x72 }, m_reader, _cardx) != 0)
            {
                Console.WriteLine("Error ");
                return -1;
            }

            //2
            if (CallApduCommand(0x00, 0xF0, 0x00, 0x00, null, m_reader, _cardx) != 0)
            {
                Console.WriteLine("Error ");
                return -1;
            }

            //3
            if (CallApduCommandLe(0xC0, 0xB0, 0x05, 0xB6, 0x10, 16, m_reader, _cardx) != 0)
            {
                Console.WriteLine("Error ");
                return -1;
            }

            if (CallApduCommand(0xC0, 0xD6, 0x05, 0xB6, new byte[16] { 0x40, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48, 0x49, 0x4A, 0x4B, 0x4C, 0x4D, 0x4E, 0x4F }, m_reader, _cardx) != 0)
            {
                Console.WriteLine("Error ");
                return -1;
            }

            if (CallApduCommandLe(0xC0, 0xB0, 0x05, 0xE6, 0x10, 16, m_reader, _cardx) != 0)
            {
                Console.WriteLine("Error ");
                return -1;
            }

            if (CallApduCommand(0xC0, 0xD6, 0x05, 0xE6, new byte[16] { 0x40, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48, 0x49, 0x4A, 0x4B, 0x4C, 0x4D, 0x4E, 0x4F }, m_reader, _cardx) != 0)
            {
                Console.WriteLine("Error ");
                return -1;
            }

            if (CallApduCommandLe(0xC0, 0xB0, 0x06, 0x16, 0x10, 16, m_reader, _cardx) != 0)
            {
                Console.WriteLine("Error ");
                return -1;
            }

            if (CallApduCommand(0xC0, 0xD6, 0x06, 0x16, new byte[16] { 0x40, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48, 0x49, 0x4A, 0x4B, 0x4C, 0x4D, 0x4E, 0x4F }, m_reader, _cardx) != 0)
            {
                Console.WriteLine("Error ");
                return -1;
            }

            if (CallApduCommandLe(0xC0, 0xB0, 0x02, 0xFC, 0x01, 1, m_reader, _cardx) != 0)
            {
                Console.WriteLine("Error ");
                return -1;
            }

            if (CallApduCommand(0xC0, 0xD6, 0x02, 0xFC, new byte[1] { 0x96 }, m_reader, _cardx) != 0)
            {
                Console.WriteLine("Error ");
                return -1;
            }

            if (CallApduCommandLe(0xC0, 0xB0, 0x03, 0x1F, 0x01, 1, m_reader, _cardx) != 0)
            {
                Console.WriteLine("Error ");
                return -1;
            }

            if (CallApduCommand(0xC0, 0xD6, 0x03, 0x1F, new byte[1] { 0x96 }, m_reader, _cardx) != 0)
            {
                Console.WriteLine("Error ");
                return -1;
            }

            if (CallApduCommand(0x00, 0x10, 0x00, 0x00, null, m_reader, _cardx) != 0)
            {
                Console.WriteLine("Error ");
                return -1;
            }

            _cardx.Disconnect(DISCONNECT.Reset);

            _cardx.Connect(m_reader, SHARE.Shared, PROTOCOL.T0orT1);

            if (ext.ExternalAuth() != 0)
            {
                return -1;
            }

            //AppletPackege Open
            if (CallApduCommand(0x80, 0xE6, 0x02, 0x00, new byte[23] { 0x07, 0xA0, 0x00, 0x00, 0x00, 0x62, 0x02, 0x02, 0x08, 0xA0, 0x00, 0x00, 0x01, 0x51, 0x00, 0x00, 0x00, 0x00, 0x03, 0x35, 0x01, 0x08, 0x00 }, m_reader, _cardx) != 0)
            {
                Console.WriteLine("Error ");
                return -1;
            }

            if (CallApduCommand(0x80, 0xE6, 0x02, 0x00, new byte[23] { 0x07, 0xA0, 0x00, 0x00, 0x01, 0x32, 0x00, 0x01, 0x08, 0xA0, 0x00, 0x00, 0x01, 0x51, 0x00, 0x00, 0x00, 0x00, 0x03, 0x35, 0x01, 0x0A, 0x00 }, m_reader, _cardx) != 0)
            {
                Console.WriteLine("Error ");
                return -1;
            }

            if (CallApduCommand(0x80, 0xE6, 0x02, 0x00, new byte[24] { 0x08, 0xD2, 0x76, 0x00, 0x00, 0x98, 0x6C, 0x69, 0x62, 0x08, 0xA0, 0x00, 0x00, 0x01, 0x51, 0x00, 0x00, 0x00, 0x00, 0x03, 0x35, 0x01, 0x0C, 0x00 }, m_reader, _cardx) != 0)
            {
                Console.WriteLine("Error ");
                return -1;
            }

            if (CallApduCommand(0x80, 0xE6, 0x02, 0x00, new byte[25] { 0x09, 0xD2, 0x76, 0x00, 0x00, 0x98, 0x63, 0x6F, 0x72, 0x65, 0x08, 0xA0, 0x00, 0x00, 0x01, 0x51, 0x00, 0x00, 0x00, 0x00, 0x03, 0x35, 0x01, 0x0E, 0x00 }, m_reader, _cardx) != 0)
            {
                Console.WriteLine("Error ");
                return -1;
            }

            if (CallApduCommand(0x80, 0xE6, 0x02, 0x00, new byte[24] { 0x08, 0xD2, 0x76, 0x00, 0x00, 0x98, 0x65, 0x78, 0x74, 0x08, 0xA0, 0x00, 0x00, 0x01, 0x51, 0x00, 0x00, 0x00, 0x00, 0x03, 0x35, 0x01, 0x10, 0x00 }, m_reader, _cardx) != 0)
            {
                Console.WriteLine("Error ");
                return -1;
            }


            if (CallApduCommand(0x80, 0xE6, 0x02, 0x00, new byte[27] { 0x0B, 0xD2, 0x76, 0x00, 0x00, 0x98, 0x63, 0x6F, 0x72, 0x65, 0x6B, 0x6F, 0x08, 0xA0, 0x00, 0x00, 0x01, 0x51, 0x00, 0x00, 0x00, 0x00, 0x03, 0x35, 0x01, 0x12, 0x00 }, m_reader, _cardx) != 0)
            {
                Console.WriteLine("Error ");
                return -1;
            }


            if (CallApduCommand(0x80, 0xE6, 0x02, 0x00, new byte[24] { 0x08, 0xD2, 0x76, 0x00, 0x00, 0x98, 0x4C, 0x44, 0x53, 0x08, 0xA0, 0x00, 0x00, 0x01, 0x51, 0x00, 0x00, 0x00, 0x00, 0x03, 0x35, 0x01, 0x1C, 0x00 }, m_reader, _cardx) != 0)
            {
                Console.WriteLine("Error ");
                return -1;
            }

            if (CallApduCommand(0x80, 0xE6, 0x02, 0x00, new byte[28] { 0x0C, 0xD2, 0x76, 0x00, 0x00, 0x98, 0x54, 0x52, 0x30, 0x33, 0x31, 0x31, 0x30, 0x08, 0xA0, 0x00, 0x00, 0x01, 0x51, 0x00, 0x00, 0x00, 0x00, 0x03, 0x35, 0x01, 0x1E, 0x00 }, m_reader, _cardx) != 0)
            {
                Console.WriteLine("Error ");
                return -1;
            }

            if (CallApduCommand(0x80, 0xE6, 0x02, 0x00, new byte[27] { 0x0B, 0xD2, 0x76, 0x00, 0x00, 0x98, 0x63, 0x6F, 0x72, 0x65, 0x61, 0x6F, 0x08, 0xA0, 0x00, 0x00, 0x01, 0x51, 0x00, 0x00, 0x00, 0x00, 0x03, 0x35, 0x01, 0x14, 0x00 }, m_reader, _cardx) != 0)
            {
                Console.WriteLine("Error ");
                return -1;
            }

            if (CallApduCommand(0x80, 0xE6, 0x02, 0x00, new byte[28] { 0x0C, 0xD2, 0x76, 0x00, 0x00, 0x98, 0x63, 0x6F, 0x72, 0x65, 0x62, 0x69, 0x6F, 0x08, 0xA0, 0x00, 0x00, 0x01, 0x51, 0x00, 0x00, 0x00, 0x00, 0x03, 0x35, 0x01, 0x16, 0x00 }, m_reader, _cardx) != 0)
            {
                Console.WriteLine("Error ");
                return -1;
            }

            if (CallApduCommand(0x80, 0xE6, 0x02, 0x00, new byte[29] { 0x0D, 0xD2, 0x76, 0x00, 0x00, 0x98, 0x63, 0x6F, 0x72, 0x65, 0x72, 0x75, 0x6C, 0x65, 0x08, 0xA0, 0x00, 0x00, 0x01, 0x51, 0x00, 0x00, 0x00, 0x00, 0x03, 0x35, 0x01, 0x18, 0x00 }, m_reader, _cardx) != 0)
            {
                Console.WriteLine("Error ");
                return -1;
            }

            if (CallApduCommand(0x80, 0xE6, 0x02, 0x00, new byte[25] { 0x09, 0xD2, 0x76, 0x00, 0x00, 0x98, 0x61, 0x75, 0x74, 0x68, 0x08, 0xA0, 0x00, 0x00, 0x01, 0x51, 0x00, 0x00, 0x00, 0x00, 0x03, 0x35, 0x01, 0x1A, 0x00 }, m_reader, _cardx) != 0)
            {
                Console.WriteLine("Error ");
                return -1;
            }

            if (CallApduCommand(0x80, 0xE6, 0x02, 0x00, new byte[21] { 0x05, 0xD2, 0x76, 0x00, 0x00, 0x98, 0x08, 0xA0, 0x00, 0x00, 0x01, 0x51, 0x00, 0x00, 0x00, 0x00, 0x03, 0x35, 0x01, 0x24, 0x00 }, m_reader, _cardx) != 0)
            {
                Console.WriteLine("Error ");
                return -1;
            }


            if (CallApduCommand(0x80, 0xE6, 0x02, 0x00, new byte[23] { 0x07, 0xD2, 0x76, 0x00, 0x00, 0x98, 0x4D, 0x44, 0x08, 0xA0, 0x00, 0x00, 0x01, 0x51, 0x00, 0x00, 0x00, 0x00, 0x03, 0x35, 0x01, 0x26, 0x00 }, m_reader, _cardx) != 0)
            {
                Console.WriteLine("Error ");
                return -1;
            }

            if (CallApduCommand(0x80, 0xE6, 0x02, 0x00, new byte[24] { 0x08, 0xD2, 0x76, 0x00, 0x00, 0x98, 0x45, 0x41, 0x43, 0x08, 0xA0, 0x00, 0x00, 0x01, 0x51, 0x00, 0x00, 0x00, 0x00, 0x03, 0x35, 0x01, 0x28, 0x00 }, m_reader, _cardx) != 0)
            {
                Console.WriteLine("Error ");
                return -1;
            }


            if (CallApduCommand(0x80, 0xE6, 0x02, 0x00, new byte[24] { 0x08, 0xD2, 0x76, 0x00, 0x00, 0x98, 0x65, 0x49, 0x44, 0x08, 0xA0, 0x00, 0x00, 0x01, 0x51, 0x00, 0x00, 0x00, 0x00, 0x03, 0x35, 0x01, 0x22, 0x00 }, m_reader, _cardx) != 0)
            {
                Console.WriteLine("Error ");
                return -1;
            }


            if (CallApduCommand(0x80, 0xE6, 0x02, 0x00, new byte[24] { 0x08, 0xD2, 0x76, 0x00, 0x00, 0x98, 0x50, 0x4B, 0x49, 0x08, 0xA0, 0x00, 0x00, 0x01, 0x51, 0x00, 0x00, 0x00, 0x00, 0x03, 0x35, 0x01, 0x2A, 0x00 }, m_reader, _cardx) != 0)
            {
                Console.WriteLine("Error ");
                return -1;
            }


            if (CallApduCommand(0x80, 0xE6, 0x02, 0x00, new byte[24] { 0x08, 0xD2, 0x76, 0x00, 0x00, 0x98, 0x49, 0x41, 0x53, 0x08, 0xA0, 0x00, 0x00, 0x01, 0x51, 0x00, 0x00, 0x00, 0x00, 0x03, 0x35, 0x01, 0x20, 0x00 }, m_reader, _cardx) != 0)
            {
                Console.WriteLine("Error ");
                return -1;
            }

            _cardx.Disconnect(DISCONNECT.Reset);

            _cardx.Connect(m_reader, SHARE.Shared, PROTOCOL.T0orT1);


            if (ext.ExternalAuth() != 0)
            {
                return -1;
            }

            if (CallApduCommand(0x80, 0xF0, 0x80, 0x07, null, m_reader, _cardx) != 0)
            {
                Console.WriteLine("Error ");
                return -1;
            }

            if (CallApduCommand(0x80, 0xE6, 0x0C, 0x00, new byte[36] { 0x08, 0xD2, 0x76, 0x00, 0x00, 0x98, 0x45, 0x41, 0x43, 0x07, 0xA0, 0x00, 0x00, 0x02, 0x47, 0x10, 0x01, 0x07, 0xA0, 0x00, 0x00, 0x02, 0x48, 0x02, 0x00, 0x01, 0x0C, 0x07, 0xC9, 0x05, 0x4C, 0x00, 0x00, 0x02, 0x0A, 0x00 }, m_reader, _cardx) != 0)
            {
                Console.WriteLine("Error ");
                return -1;
            }


            return 0;
        }


        private byte[] CallApduCommandLeData(byte Cla, byte Ins, byte P1, byte P2, byte[] data, uint _recieveLength, CardNative _cardx, string readerInfo)
        {

            APDUResponse apduResp;

            const ushort SC_OK = 0x9000;
            const byte SC_PENDING = 0x9F;
            const ushort SC_FileEnd = 0x6282;

            APDUCommand apduSize5 = new APDUCommand(Cla, Ins, P1, P2, null, 0);

            APDUParam apduParam5 = new APDUParam();

            apduParam5.Data = data;

            apduSize5.Update(apduParam5);

            _cardx.Disconnect(DISCONNECT.Reset);

            _cardx.Connect(readerInfo, SHARE.Shared, PROTOCOL.T0orT1);

            apduResp = _card.TransmitLe(apduSize5, _recieveLength);
            if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING && apduResp.Status != SC_FileEnd)
            {
                return null;
            }

            return apduResp.Data;
        }
    }
}
