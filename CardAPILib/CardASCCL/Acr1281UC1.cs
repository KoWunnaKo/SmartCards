/*===========================================================================================
 * 
 *  Copyright (C)   : Advanced Card System Ltd
 * 
 *  File            : Acr1281UC1.cs
 * 
 *  Description     : Contains Methods and Properties related to ACR1281U-C1 device's functionality
 * 
 *  Author          : Arturo Salvamante
 *  
 *  Date            : October 19, 2011
 * 
 *  Revision Traile : [Author] / [Date if modification] / [Details of Modifications done] 
 *  
 *                   Buboy    October 12, 2011      Add functions for advanced device programming
 * 
 * =========================================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advanced_Device_Programming
{
    public enum CHIP_TYPE
    {
        UNKNOWN = 0,
        MIFARE_1K = 1,
        MIFARE_4K = 2,
        MIFARE_ULTRALIGHT = 3,
    }

    public enum KEY_STRUCTURE
    {
        VOLATILE = 0x00,
        NON_VOLATILE = 0x20
    }

    public enum KEYTYPES
    {
        ACR122_KEYTYPE_A = 96,
        ACR122_KEYTYPE_B = 97,
    }

    public enum LED_BUZZER_STATUS
    {
        OFF = 0,
        ON = 1
    }

    public enum READER_MODE
    {
        SHARE_MODE = 0x00,
        EXCLUSIVE_MODE = 0x01
    }

    public enum POLLING_INERVAL
    {
        MSEC_250,
        MSEC_500,
        MSEC_1000,
        MSEC_2500
    }

    public enum PPS_SPEED
    {
        KBPS_106 = 0x00,
        KBPS_212 = 0x01,
        KBPS_424 = 0x02,
        KBPS_848 = 0x03
    }

    public enum ANTENNA_FIELD_STATUS
    {
        DISABLE = 0x00,
        ENABLE = 0x01
    }

    public enum AUTO_HANDLE_OPTION_616C
    {
        DISABLE = 0x00,
        ENABLE = 0xFF
    }

    public enum DEVICE_INTERFACE
    {
        ICC = 0x01,
        PICC = 0x02,
        SAM = 0x04
    }

    public class LedStatus
    {

        private LED_BUZZER_STATUS _red = LED_BUZZER_STATUS.OFF;
        private LED_BUZZER_STATUS _green = LED_BUZZER_STATUS.OFF;

        public LedStatus()
        {

        }

        public LedStatus(byte ledStatus)
        {
            if ((ledStatus & 0x01) == 0x01)
                _red = LED_BUZZER_STATUS.ON;

            if ((ledStatus & 0x02) == 0x02)
                _green = LED_BUZZER_STATUS.ON;
        }
        
        public LED_BUZZER_STATUS red
        {
            get { return _red; }
            set { _red = value; }
        }

        public LED_BUZZER_STATUS green
        {
            get { return _green; }
            set { _green = value; }
        }

        public byte getRawLedStatus()
        {
            byte ledStatus = 0x00;

            if (red == LED_BUZZER_STATUS.ON)
                ledStatus |= 0x01;

            if (green == LED_BUZZER_STATUS.ON)
                ledStatus |= 0x02;

            return ledStatus;
        }
    }

    public class DefaultLedBuzzerBehavior
    {
        public DefaultLedBuzzerBehavior()
        {
            
        }

        public DefaultLedBuzzerBehavior(byte rawBehavior)
        {
            setRawDefaultBehavior(rawBehavior);
        }

        private LED_BUZZER_STATUS _iccActivationStatusLed = LED_BUZZER_STATUS.OFF;
        private LED_BUZZER_STATUS _piccPollingStatusLed = LED_BUZZER_STATUS.OFF;
        private LED_BUZZER_STATUS _piccActivationStatusBuzzer = LED_BUZZER_STATUS.OFF;
        private LED_BUZZER_STATUS _cardInsertionAndRemovalEventsBuzzer = LED_BUZZER_STATUS.OFF;
        private LED_BUZZER_STATUS _contactlessChipResetIndicationBuzzer = LED_BUZZER_STATUS.OFF;
        private LED_BUZZER_STATUS _exclusiveModeStatusBuzzer = LED_BUZZER_STATUS.OFF;
        private LED_BUZZER_STATUS _cardOperationBlinkingLed = LED_BUZZER_STATUS.OFF;

        public LED_BUZZER_STATUS iccActivationStatusLed
        {
            get { return _iccActivationStatusLed; }
            set { _iccActivationStatusLed = value; }
        }

        public LED_BUZZER_STATUS piccPollingStatusLed
        {
            get { return _piccPollingStatusLed; }
            set { _piccPollingStatusLed = value; }
        }

        public LED_BUZZER_STATUS piccActivationStatusBuzzer
        {
            get { return _piccActivationStatusBuzzer; }
            set { _piccActivationStatusBuzzer = value; }
        }

        public LED_BUZZER_STATUS cardInsertionAndRemovalEventsBuzzer
        {
            get { return _cardInsertionAndRemovalEventsBuzzer; }
            set { _cardInsertionAndRemovalEventsBuzzer = value; }
        }

        public LED_BUZZER_STATUS contactlessChipResetIndicationBuzzer
        {
            get { return _contactlessChipResetIndicationBuzzer; }
            set { _contactlessChipResetIndicationBuzzer = value; }
        }

        public LED_BUZZER_STATUS exclusiveModeStatusBuzzer
        {
            get { return _exclusiveModeStatusBuzzer; }
            set { _exclusiveModeStatusBuzzer = value; }
        }

        public LED_BUZZER_STATUS cardOperationBlinkingLed
        {
            get { return _cardOperationBlinkingLed; }
            set { _cardOperationBlinkingLed = value; }
        }

        public void setRawDefaultBehavior(byte rawBehavior)
        {
            if ((rawBehavior & 0x01) == 0x01)
                iccActivationStatusLed = LED_BUZZER_STATUS.ON;

            if ((rawBehavior & 0x02) == 0x02)
                piccPollingStatusLed = LED_BUZZER_STATUS.ON;

            if ((rawBehavior & 0x04) == 0x04)
                piccActivationStatusBuzzer = LED_BUZZER_STATUS.ON;

            if ((rawBehavior & 0x10) == 0x10)
                cardInsertionAndRemovalEventsBuzzer = LED_BUZZER_STATUS.ON;

            if ((rawBehavior & 0x20) == 0x20)
                contactlessChipResetIndicationBuzzer = LED_BUZZER_STATUS.ON;

            if ((rawBehavior & 0x40) == 0x40)
                exclusiveModeStatusBuzzer = LED_BUZZER_STATUS.ON;

            if ((rawBehavior & 0x80) == 0x80)
                cardOperationBlinkingLed = LED_BUZZER_STATUS.ON;
        }

        public byte getRawDefaulBehavior()
        {
            byte rawBehavior = 0x00;

            if (iccActivationStatusLed == LED_BUZZER_STATUS.ON)
                rawBehavior |= 0x01;

            if (piccPollingStatusLed == LED_BUZZER_STATUS.ON)
                rawBehavior |= 0x02;

            if (piccActivationStatusBuzzer == LED_BUZZER_STATUS.ON)
                rawBehavior |= 0x04;

            if (cardInsertionAndRemovalEventsBuzzer == LED_BUZZER_STATUS.ON)
                rawBehavior |= 0x10;

            if (contactlessChipResetIndicationBuzzer == LED_BUZZER_STATUS.ON)
                rawBehavior |= 0x20;

            if (exclusiveModeStatusBuzzer == LED_BUZZER_STATUS.ON)
                rawBehavior |= 0x40;

            if (cardOperationBlinkingLed == LED_BUZZER_STATUS.ON)
                rawBehavior |= 0x80;

            return rawBehavior;
        }
    }

    public class ModeConfiguration
    {
        public ModeConfiguration(byte rawNewMode, byte rawPreviousMode)
        {
            if (rawNewMode == 0x00)
                newMode = READER_MODE.SHARE_MODE;
            else
                newMode = READER_MODE.EXCLUSIVE_MODE;

            if(rawPreviousMode == 0x00)
                previousMode = READER_MODE.SHARE_MODE;
            else
                previousMode = READER_MODE.EXCLUSIVE_MODE;
        }

        private READER_MODE _newMode;
        private READER_MODE _previousMode;

        public READER_MODE newMode
        {
            get { return _newMode; }
            set { _newMode = value; }
        }

        public READER_MODE previousMode
        {
            get { return _previousMode; }
            set { _previousMode = value; }
        }
    }

    public class AutoPiccPollingSettings
    {
        public AutoPiccPollingSettings()
        {

        }

        public AutoPiccPollingSettings(byte rawPiccPollingSetting)
        {
            setRawPollingSetting(rawPiccPollingSetting);
        }

        private bool _autoPiccPolling = false;
        private bool _turnOffAntennaIfNoPiccCard = false;
        private bool _turnOffAntennaIfPiccInActive = false;
        private bool _activatePiccWhenDetected = false;
        private bool _enforceIso14443APart4 = false;
        private POLLING_INERVAL _pollingInterval = POLLING_INERVAL.MSEC_250;

        public bool autoPiccPolling
        {
            get { return _autoPiccPolling; }
            set { _autoPiccPolling = value; }
        }

        public bool turnOffAntennaIfNoPiccCard
        {
            get { return _turnOffAntennaIfNoPiccCard; }
            set { _turnOffAntennaIfNoPiccCard = value; }
        }

        public bool turnOffAntennaIfPiccInActive
        {
            get { return _turnOffAntennaIfPiccInActive; }
            set { _turnOffAntennaIfPiccInActive = value; }
        }

        public bool activatePiccWhenDetected
        {
            get { return _activatePiccWhenDetected; }
            set { _activatePiccWhenDetected = value; }
        }

        public bool enforceIso14443APart4
        {
            get { return _enforceIso14443APart4; }
            set { _enforceIso14443APart4 = value; }
        }

        public POLLING_INERVAL pollingInterval
        {
            get { return _pollingInterval; }
            set { _pollingInterval = value; }
        }

        public void setRawPollingSetting(byte rawPiccPollingSetting)
        {
            if ((rawPiccPollingSetting & 0x01) == 0x01)
                autoPiccPolling = true;

            if ((rawPiccPollingSetting & 0x02) == 0x02)
                turnOffAntennaIfNoPiccCard = true;

            if ((rawPiccPollingSetting & 0x04) == 0x04)
                turnOffAntennaIfPiccInActive = true;

            if ((rawPiccPollingSetting & 0x08) == 0x08)
                activatePiccWhenDetected = true;

            if ((rawPiccPollingSetting & 0x80) == 0x80)
                enforceIso14443APart4 = true;

            switch ((rawPiccPollingSetting >> 4) & 0x03)
            {
                case 0: pollingInterval = POLLING_INERVAL.MSEC_250; break;
                case 1: pollingInterval = POLLING_INERVAL.MSEC_500; break;
                case 2: pollingInterval = POLLING_INERVAL.MSEC_1000; break;
                case 3: pollingInterval = POLLING_INERVAL.MSEC_2500; break;
                default: pollingInterval = POLLING_INERVAL.MSEC_250; break;
            }
        }

        public byte getRawPollingSetting()
        {
            byte rawPiccPollingSettings = 0x00;

            if (autoPiccPolling)
                rawPiccPollingSettings |= 0x01;

            if (turnOffAntennaIfNoPiccCard)
                rawPiccPollingSettings |= 0x02;

            if (turnOffAntennaIfPiccInActive)
                rawPiccPollingSettings |= 0x04;

            if (activatePiccWhenDetected)
                rawPiccPollingSettings |= 0x08;

            if (enforceIso14443APart4)
                rawPiccPollingSettings |= 0x80;

            switch (pollingInterval)
            {
                case POLLING_INERVAL.MSEC_2500: rawPiccPollingSettings |= 0x30; break;
                case POLLING_INERVAL.MSEC_1000: rawPiccPollingSettings |= 0x20; break;
                case POLLING_INERVAL.MSEC_500: rawPiccPollingSettings |= 0x10; break;
                default: rawPiccPollingSettings |= 0x00; break;
            }

            return rawPiccPollingSettings;
        }
    }

    public class PiccOperatingParameter
    {
        public PiccOperatingParameter()
        {

        }

        public PiccOperatingParameter(byte rawOperatingParameter)
        {
            if ((rawOperatingParameter & 0x01) == 0x01)
                iso14443TypeA = PARAMETER_OPTION.DETECT;

            if ((rawOperatingParameter & 0x02) == 0x02)
                iso14443TypeB = PARAMETER_OPTION.DETECT;
            
        }

        public enum PARAMETER_OPTION
        {
            SKIP = 0x00,
            DETECT = 0x01          
        }

        private PARAMETER_OPTION _Iso14443TypeA = PARAMETER_OPTION.SKIP;
        private PARAMETER_OPTION _Iso14443TypeB = PARAMETER_OPTION.SKIP;

        public PARAMETER_OPTION iso14443TypeA
        {
            get { return _Iso14443TypeA; }
            set { _Iso14443TypeA = value; }
        }

        public PARAMETER_OPTION iso14443TypeB
        {
            get { return _Iso14443TypeB; }
            set { _Iso14443TypeB = value; }
        }

        public byte getRawOperatingParamter()
        {
            byte operatingParameter = 0x00;

            if (iso14443TypeA == PARAMETER_OPTION.DETECT)
                operatingParameter |= 0x01;

            if (iso14443TypeB == PARAMETER_OPTION.DETECT)
                operatingParameter |= 0x02;

            return operatingParameter;
        }

    }

    public class Acr1281UC1 : PcscReader
    {
        //Note: When using 3500 you must use the full command format (E0 00 00 xx xx)
        const uint IOCTL_SMARTCARD_ACR128_ESCAPE_COMMAND = (uint)PcscProvider.FILE_DEVICE_SMARTCARD + 2079 * 4;

        public Acr1281UC1()
            : base()
        {
            base.establishContext();
            operationControlCode = IOCTL_SMARTCARD_ACR128_ESCAPE_COMMAND;
        }

        public Acr1281UC1(string readerName)
            : base(readerName)
        {
            base.establishContext();
            operationControlCode = IOCTL_SMARTCARD_ACR128_ESCAPE_COMMAND;
        }

        public byte[] getCardSerialNumber()
        {
            byte[] cardSerial;

            apduCommand = new Apdu();
            apduCommand.setCommand(new byte[] {  0xFF,      //Intruction Class
                                                 0xCA,      //Intruction Code
                                                 0x00,      //Parameter 1
                                                 0x00,      //Parameter 2
                                                 0x00 });   //Parameter 3
            apduCommand.lengthExpected = 20;
            sendCommand();

            if (apduCommand.statusWord[0] != 0x90)
                return null;

            cardSerial = new byte[apduCommand.response.Length];
            Array.Copy(apduCommand.response, cardSerial, cardSerial.Length);

            return cardSerial;

        }

        public byte[] getAnswerToSelect()
        {
            apduCommand = new Apdu();
            apduCommand.setCommand(new byte[] {  0xFF, 
                                                 0xCA, 
                                                 0x01, 
                                                 0x00, 
                                                 0x00 });

            apduCommand.lengthExpected = 50;

            sendCommand();

            if (!apduCommand.statusWordEqualTo(new byte[] { 0x90, 0x00 }))
                throw new CardException("Unable to get Answer to Select (ATS)", apduCommand.statusWord);

            return apduCommand.response;
        }

        public void loadAuthKey(KEY_STRUCTURE keyStructure, byte keyNumber, byte[] key)
        {            
            if (keyStructure == KEY_STRUCTURE.NON_VOLATILE)
            {
                if (keyNumber > 0x1F)
                    throw new Exception("Key number is invalid");
            }
            else
            {
                keyNumber = 0x20;
            }

            if (key.Length != 6)
                throw new Exception("Invalid key length");


            apduCommand = new Apdu();
            apduCommand.setCommand(new byte[] {  0xFF,                  //Instruction Class
                                                 0x82,                  //Instruction code
                                                 (byte)keyStructure,    //Key Structure
                                                 keyNumber,             //Key Number
                                                 0x06 });               //Length of key

            //Set key to load
            apduCommand.data = key;

            sendCommand();

            if (!apduCommand.statusWordEqualTo(new byte[] { 0x90, 0x00 }))
                throw new CardException("Load key failed", apduCommand.statusWord);
                
        }

        public void authenticate(byte blockNumber, KEYTYPES keyType, byte KeyNumber)
        {
            if (KeyNumber < 0x00 && KeyNumber > 0x20)
                throw new Exception("Key number is invalid");

            apduCommand = new Apdu();
            apduCommand.setCommand(new byte[]{ 0xFF,            //Instruction Class
                                               0x86,            //Instruction Code
                                               0x00,            //RFU
                                               0x00,            //RFU
                                               0x05});          //Length of authentication data bytes

            //Authentication Data Bytes
            apduCommand.data = new byte[] {  0x01,              //Version
                                             0x00,              //RFU
                                             (byte)blockNumber, //Block Number
                                             (byte)keyType,     //Key Type
                                             KeyNumber};        //Key Number

            sendCommand();

            if (!apduCommand.statusWordEqualTo(new byte[] { 0x90, 0x00 }))
                throw new CardException("Authenticate failed", apduCommand.statusWord);
        }

        public CHIP_TYPE getChipType()
        {
            int rdrLen = 0, retCode, protocol = pcscConnection.activeProtocol;
            int pdwSate = 0, atrLen = 33;
            byte[] atr = new byte[100];
            CHIP_TYPE cardType = CHIP_TYPE.UNKNOWN;


            retCode = PcscProvider.SCardStatus(pcscConnection.cardHandle, pcscConnection.readerName, ref rdrLen, ref pdwSate,
                                                ref protocol, atr, ref atrLen);

            if (retCode != PcscProvider.SCARD_S_SUCCESS)
                throw new PcscException(retCode);

            pcscConnection.activeProtocol = protocol;

            if (atr.Length < 33)
                return CHIP_TYPE.UNKNOWN;

            Array.Resize(ref atr, atrLen);

            if (atr[13] == 0x00 && atr[14] == 0x01)
                cardType = CHIP_TYPE.MIFARE_1K;
            else if (atr[13] == 0x00 && atr[14] == 0x02)
                cardType = CHIP_TYPE.MIFARE_4K;
            else if (atr[13] == 0x00 && atr[14] == 0x03)
                cardType = CHIP_TYPE.MIFARE_ULTRALIGHT;
            else
                cardType = CHIP_TYPE.UNKNOWN;

            return cardType;
        }

        public override byte[] getFirmwareVersion()
        {
            int firmwareLength;

            apduCommand = new Apdu();
            apduCommand.data = new byte[] { 0x18, 0x00 };

            //Assume maximum length
            apduCommand.lengthExpected = 255;

            sendCardControl();

            if (apduCommand.response == null || apduCommand.response.Length < 6)
                throw new ReaderException("Unable to get firmware version", apduCommand.response);

            firmwareLength = apduCommand.response[4];

            return apduCommand.response.Skip(5).Take(firmwareLength).ToArray();
        }

        public LedStatus getLedStatus()
        {
            apduCommand = new Apdu();
            apduCommand.data = new byte[] {0x29, 0x00 };

            //Assume maximum length
            apduCommand.lengthExpected = 10;

            sendCardControl();

            if (apduCommand.response == null || apduCommand.response.Length < 6)
                throw new ReaderException("Unable to get led status version", apduCommand.response);

            return new LedStatus(apduCommand.response[5]);
        }

        public LedStatus setLedStatus(LedStatus ledStatus)
        {
            apduCommand = new Apdu();
            apduCommand.data = new byte[] { 0x29, 0x01, ledStatus.getRawLedStatus() };

            //Assume maximum length
            apduCommand.lengthExpected = 10;

            sendCardControl();

            if (apduCommand.response == null || apduCommand.response.Length < 6)
                throw new ReaderException("Unable to set led status version", apduCommand.response);

            return new LedStatus(apduCommand.response[5]);
        }

        public byte setBuzzer(byte duration)
        {
            apduCommand = new Apdu();
            apduCommand.data = new byte[] { 0x28, 0x01, duration };

            apduCommand.lengthExpected = 10;

            sendCardControl();

            if (apduCommand.response == null || apduCommand.response.Length < 6)
                throw new ReaderException("Unable to set buzzer", apduCommand.response);

            return apduCommand.response[5];
        }

        public DefaultLedBuzzerBehavior setDefaultLedBuzzerBehavior(DefaultLedBuzzerBehavior defaultBehavior)
        {
            apduCommand = new Apdu();
            apduCommand.data = new byte[] { 0x21, 0x01, defaultBehavior.getRawDefaulBehavior() };

            apduCommand.lengthExpected = 10;

            sendCardControl();

            if (apduCommand.response == null || apduCommand.response.Length < 6)
                throw new ReaderException("Unable to set default led and buzzer behavior", apduCommand.response);

            return new DefaultLedBuzzerBehavior(apduCommand.response[5]);
        }

        public DefaultLedBuzzerBehavior getDefaultLedBuzzerBehavior()
        {
            apduCommand = new Apdu();
            apduCommand.data = new byte[] { 0x21, 0x00 };

            apduCommand.lengthExpected = 10;

            sendCardControl();

            if (apduCommand.response == null || apduCommand.response.Length < 6)
                throw new ReaderException("Unable to get the default led and buzzer behavior", apduCommand.response);

            return new DefaultLedBuzzerBehavior(apduCommand.response[5]);
        }

        public ModeConfiguration setReaderMode(bool enableExclusiveMode)
        {
            byte exclusiveMode = 0x00;

            if (enableExclusiveMode)
                exclusiveMode = 0x01;
            
            apduCommand = new Apdu();
            apduCommand.data = new byte[] { 0x2B, 0x01, exclusiveMode};

            apduCommand.lengthExpected = 10;

            sendCardControl();

            if (apduCommand.response == null || apduCommand.response.Length < 7)
                throw new ReaderException("Unable to set reader mode", apduCommand.response);

            return new ModeConfiguration(apduCommand.response[5], apduCommand.response[6]);
        }

        public ModeConfiguration getReaderMode()
        {
            apduCommand = new Apdu();
            apduCommand.data = new byte[] { 0x2B, 0x00 };

            apduCommand.lengthExpected = 10;

            sendCardControl();

            if (apduCommand.response == null || apduCommand.response.Length < 7)
                throw new ReaderException("Unable to set reader mode", apduCommand.response);

            return new ModeConfiguration(apduCommand.response[5], apduCommand.response[6]);
        }

        public AutoPiccPollingSettings setAutomaticPiccPolling(AutoPiccPollingSettings pollingSettings)
        {
            apduCommand = new Apdu();
            apduCommand.data = new byte[] { 0x23, 0x01, pollingSettings.getRawPollingSetting() };

            apduCommand.lengthExpected = 10;

            sendCardControl();

            if (apduCommand.response == null || apduCommand.response.Length < 6)
                throw new ReaderException("Unable to set auto PICC polling settings", apduCommand.response);

            return new AutoPiccPollingSettings(apduCommand.response[5]);
        }

        public AutoPiccPollingSettings getAutomaticPiccPolling()
        {
            apduCommand = new Apdu();
            apduCommand.data = new byte[] { 0x23, 0x00 };

            apduCommand.lengthExpected = 10;

            sendCardControl();

            if (apduCommand.response == null || apduCommand.response.Length < 6)
                throw new ReaderException("Unable to get auto PICC polling settings", apduCommand.response);

            return new AutoPiccPollingSettings(apduCommand.response[5]);
        }

        public PiccOperatingParameter setPiccOperatingParameter(PiccOperatingParameter operatingParameter)
        {
            apduCommand = new Apdu();
            apduCommand.data = new byte[] { 0x20, 0x01, operatingParameter.getRawOperatingParamter() };

            apduCommand.lengthExpected = 10;

            sendCardControl();

            if (apduCommand.response == null || apduCommand.response.Length < 6)
                throw new ReaderException("Unable to get PICC operating parameter", apduCommand.response);

            return new PiccOperatingParameter(apduCommand.response[5]);
        }

        public PiccOperatingParameter getPiccOperatingParameter()
        {
            apduCommand = new Apdu();
            apduCommand.data = new byte[] { 0x20, 0x00 };

            apduCommand.lengthExpected = 10;

            sendCardControl();

            if (apduCommand.response == null || apduCommand.response.Length < 6)
                throw new ReaderException("Unable to get PICC operating parameter", apduCommand.response);

            return new PiccOperatingParameter(apduCommand.response[5]);
        }

        public byte updateRegister(byte registerNumber, byte value)
        {

            if (registerNumber < 0x10 || registerNumber > 0x7F)
                throw new Exception("Invalid register address/number. Valid value is from 0x10 to 0x7F");

            apduCommand = new Apdu();
            apduCommand.data = new byte[] { 0x1A, 0x02, registerNumber, value };

            apduCommand.lengthExpected = 10;

            sendCardControl();

            if (apduCommand.response == null || apduCommand.response.Length < 6)
                throw new ReaderException("Unable to set register value", apduCommand.response);

            return apduCommand.response[5];
        }

        public byte readRegister(byte registerNumber)
        {
            //byte value;

            if (registerNumber > 0x7F)
                throw new Exception("Invalid register address/number. Valid value is from 00 to 0x7F");

            apduCommand = new Apdu();
            apduCommand.data = new byte[] { 0x19, 0x01, registerNumber };

            apduCommand.lengthExpected = 10;

            sendCardControl();

            if (apduCommand.response == null || apduCommand.response.Length < 6)
                throw new ReaderException("Unable to set register value", apduCommand.response);

            return apduCommand.response[5];
        }

        public void setUserExtraGuardTime(byte iccGuardTime, byte samGuardTime)
        {
            apduCommand = new Apdu();
            apduCommand.data = new byte[] { 0x2E, 0x02, iccGuardTime, samGuardTime };

            apduCommand.lengthExpected = 10;

            sendCardControl();

            if (apduCommand.response == null || apduCommand.response.Length < 7)
                throw new ReaderException("Unable to set user extra guard time", apduCommand.response);
        }
        
        public void getUserExtraGuardTime(out byte iccGuardTime, out byte samGuardTime)
        {
            apduCommand = new Apdu();
            apduCommand.data = new byte[] { 0x2E, 0x00 };

            apduCommand.lengthExpected = 10;

            sendCardControl();

            if (apduCommand.response == null || apduCommand.response.Length < 7)
                throw new ReaderException("Unable to get user extra guard time", apduCommand.response);

            iccGuardTime = apduCommand.response[5];
            samGuardTime = apduCommand.response[6];
        }
        
        public bool manualCardDetection()
        {
            apduCommand = new Apdu();
            apduCommand.data = new byte[] { 0x22, 0x01, 0x0A};

            apduCommand.lengthExpected = 6;

            sendCardControl();

            if (apduCommand.response == null || apduCommand.response.Length < 6)
                throw new ReaderException("Unable to detect card", apduCommand.response);

            if (apduCommand.response[5] == 0xFF)
                return false;
            else
                return true;
        }

        public void setCardInsertionDetectionCounter(Int16 iccInitialCountervalue, Int16 piccInitialCountervalue)
        {
            byte[] iccValue, piccValue;

            //Get the 2 bytes from the lsb
            iccValue = Helper.intToByte(iccInitialCountervalue).Skip(2).Take(2).ToArray();

            //reverse the array to comform to the acr1281 format
            Array.Reverse(iccValue);

            //Get the 2 bytes from the lsb
            piccValue = Helper.intToByte(piccInitialCountervalue).Skip(2).Take(2).ToArray();

            //reverse the array to comform to the acr1281 format
            Array.Reverse(piccValue);

            apduCommand = new Apdu();
            apduCommand.data = new byte[6];

            apduCommand.data[0] = 0x09;
            apduCommand.data[1] = 0x04;

            //Copy icc initial counter value
            Array.Copy(iccValue, 0, apduCommand.data, 2, 2);

            //Copy icc initial counter value
            Array.Copy(piccValue, 0, apduCommand.data, 4, 2);

            apduCommand.lengthExpected = 10;

            sendCardControl();

            if (apduCommand.response == null || apduCommand.response.Length < 5 ||
                Helper.byteArrayIsEqual(apduCommand.response, new byte[] { 0xE1, 0x00, 0x00, 0x00, 0x00 }, 5))
                throw new ReaderException("Unable to initialize card insertion/detection counter", apduCommand.response);
            
        }

        public void getCardInsertionDetectionCounter(out Int16 iccInitialCountervalue, out Int16 piccInitialCountervalue)
        {
            byte[] iccValue, piccValue;

            apduCommand = new Apdu();
            apduCommand.data = new byte[6];

            apduCommand.data[0] = 0x09;
            apduCommand.data[1] = 0x00;

            apduCommand.lengthExpected = 10;

            sendCardControl();

            if (apduCommand.response == null || apduCommand.response.Length < 9)
                throw new ReaderException("Unable to read card insertion/detection counter", apduCommand.response);

            //Get the 2 bytes from the lsb
            iccValue = apduCommand.response.Skip(5).Take(2).ToArray();

            //reverse the array
            Array.Reverse(iccValue);

            //Get the 2 bytes from the lsb
            piccValue = apduCommand.response.Skip(7).Take(2).ToArray();

            //reverse the array to comform to the acr1281 format
            Array.Reverse(piccValue);

            iccInitialCountervalue = (Int16)Helper.byteToInt(iccValue);
            piccInitialCountervalue = (Int16)Helper.byteToInt(piccValue);
        }

        public void updateCardInsertionDetectionCounter(out Int16 iccInitialCountervalue, out Int16 piccInitialCountervalue)
        {
            byte[] iccValue, piccValue;

            apduCommand = new Apdu();
            apduCommand.data = new byte[6];

            apduCommand.data[0] = 0x0A;
            apduCommand.data[1] = 0x00;

            apduCommand.lengthExpected = 10;

            sendCardControl();

            if (apduCommand.response == null || apduCommand.response.Length < 9)
                throw new ReaderException("Unable to read card insertion/detection counter", apduCommand.response);

            //Get the 2 bytes from the lsb
            iccValue = apduCommand.response.Skip(5).Take(2).ToArray();

            //reverse the array
            Array.Reverse(iccValue);

            //Get the 2 bytes from the lsb
            piccValue = apduCommand.response.Skip(7).Take(2).ToArray();

            //reverse the array to comform to the acr1281 format
            Array.Reverse(piccValue);

            iccInitialCountervalue = (Int16)Helper.byteToInt(iccValue);
            piccInitialCountervalue = (Int16)Helper.byteToInt(piccValue);
        }

        public void setAutoParameterProtocolSelection(PPS_SPEED maxSpeed)
        {
            byte ppsMaxSpeed = 0x00;


            switch (maxSpeed)
            {
                case PPS_SPEED.KBPS_212: ppsMaxSpeed = 0x01; break;
                case PPS_SPEED.KBPS_424: ppsMaxSpeed = 0x02; break;
                case PPS_SPEED.KBPS_848: ppsMaxSpeed = 0x03; break;
                default: ppsMaxSpeed = 0x00; break;
            }

            apduCommand = new Apdu();
            apduCommand.data = new byte[] { 0x24, 0x01, ppsMaxSpeed };

            apduCommand.lengthExpected = 10;

            sendCardControl();

            if (apduCommand.response == null || apduCommand.response.Length < 7)
                throw new ReaderException("Unable to set auto pps parameter", apduCommand.response);

        }
        
        public void getAutoParameterProtocolSelection(out PPS_SPEED maxSpeed, out PPS_SPEED currentSpeed)
        {                        
            apduCommand = new Apdu();
            apduCommand.data = new byte[] { 0x24, 0x00 };

            apduCommand.lengthExpected = 10;

            sendCardControl();

            if (apduCommand.response == null || apduCommand.response.Length < 7)
                throw new ReaderException("Unable to get auto pps parameter", apduCommand.response);

            //Max Speed
            switch (apduCommand.response[5])
            {
                case 0x01: maxSpeed = PPS_SPEED.KBPS_212; break;
                case 0x02: maxSpeed = PPS_SPEED.KBPS_424; break;
                case 0x03: maxSpeed = PPS_SPEED.KBPS_848; break;
                default: maxSpeed = PPS_SPEED.KBPS_106; break;
            }

            //Current Speed
            switch (apduCommand.response[6])
            {
                case 0x01: currentSpeed = PPS_SPEED.KBPS_212; break;
                case 0x02: currentSpeed = PPS_SPEED.KBPS_424; break;
                case 0x03: currentSpeed = PPS_SPEED.KBPS_848; break;
                default: currentSpeed = PPS_SPEED.KBPS_106; break;
            }


        }

        public void set616CAutoHandleOption(AUTO_HANDLE_OPTION_616C iccOption, AUTO_HANDLE_OPTION_616C samOption)
        {
            byte icc = 0x00, sam = 0x00;

            if(iccOption == AUTO_HANDLE_OPTION_616C.ENABLE)
                icc = 0xFF;

            if(samOption == AUTO_HANDLE_OPTION_616C.ENABLE)
                sam = 0xFF;

            set616CAutoHandleOption(icc, sam);
        }

        public void set616CAutoHandleOption(byte iccOption, byte samOption)
        {
            apduCommand = new Apdu();
            apduCommand.data = new byte[] { 0x32, 0x02, iccOption, samOption };

            apduCommand.lengthExpected = 10;

            sendCardControl();

            if (apduCommand.response == null || apduCommand.response.Length < 7)
                throw new ReaderException("Unable to set 616C Auto handle option", apduCommand.response);
        }

        public void get616CAutoHandleOption(out AUTO_HANDLE_OPTION_616C iccOption, out AUTO_HANDLE_OPTION_616C samOption)
        {
            byte icc = 0x00, sam = 0x00;

            get616CAutoHandleOption(out icc, out sam);

            if (icc == 0xFF)
                iccOption = AUTO_HANDLE_OPTION_616C.ENABLE;
            else
                iccOption = AUTO_HANDLE_OPTION_616C.DISABLE;

            if (sam == 0xFF)
                samOption = AUTO_HANDLE_OPTION_616C.ENABLE;
            else
                samOption = AUTO_HANDLE_OPTION_616C.DISABLE;
        }

        public void get616CAutoHandleOption(out byte iccOption, out byte samOption)
        {

            apduCommand = new Apdu();
            apduCommand.data = new byte[] { 0x32, 0x00 };

            apduCommand.lengthExpected = 10;

            sendCardControl();

            if (apduCommand.response == null || apduCommand.response.Length < 7)
                throw new ReaderException("Unable to get 616C auto handle option", apduCommand.response);

            iccOption = apduCommand.response[5];
            samOption = apduCommand.response[6];
        }

        public ANTENNA_FIELD_STATUS setAntennaField(ANTENNA_FIELD_STATUS status)
        {
            byte antennaFieldStatus;

            if (status == ANTENNA_FIELD_STATUS.ENABLE)
                antennaFieldStatus = 0x01;
            else
                antennaFieldStatus = 0x00;

            apduCommand = new Apdu();
            apduCommand.data = new byte[] { 0x25, 0x01, antennaFieldStatus };

            apduCommand.lengthExpected = 6;

            sendCardControl();

            if (apduCommand.response == null || apduCommand.response.Length < 6 ||
               (apduCommand.response[5] != antennaFieldStatus))
                throw new ReaderException("Unable to set antenna field", apduCommand.response);

            if (apduCommand.response[5] == 0x00)
                return ANTENNA_FIELD_STATUS.DISABLE;
            else
                return ANTENNA_FIELD_STATUS.ENABLE;
        }

        public ANTENNA_FIELD_STATUS getAntennaFieldStatus()
        {
            apduCommand = new Apdu();
            apduCommand.data = new byte[] { 0x25, 0x00 };

            apduCommand.lengthExpected = 6;

            sendCardControl();

            if (apduCommand.response == null || apduCommand.response.Length < 6)
                throw new ReaderException("Unable to get antenna field status", apduCommand.response);

            if (apduCommand.response[5] == 0x00)
                return ANTENNA_FIELD_STATUS.DISABLE;
            else
                return ANTENNA_FIELD_STATUS.ENABLE;
        }

        public List<DEVICE_INTERFACE> refreshInterface(List<DEVICE_INTERFACE> interfacesToRefresh)
        {
            byte status = 0x00;
            List<DEVICE_INTERFACE> interfaceList;

            foreach (DEVICE_INTERFACE i in interfacesToRefresh)
            {
                switch (i)
                {
                    case DEVICE_INTERFACE.ICC: status |= 0x01; break;
                    case DEVICE_INTERFACE.PICC: status |= 0x02; break;
                    case DEVICE_INTERFACE.SAM: status |= 0x04; break;
                }
            }

            apduCommand = new Apdu();
            apduCommand.data = new byte[] { 0x2D, 0x01, status };

            apduCommand.lengthExpected = 6;

            sendCardControl();

            if (apduCommand.response == null || apduCommand.response.Length < 6)
                throw new ReaderException("Unable to refresh interface", apduCommand.response);

            status = apduCommand.response[5];


            interfaceList = new List<DEVICE_INTERFACE>();

            if ((status & 0x01) == 0x01)
                interfaceList.Add(DEVICE_INTERFACE.ICC);

            if ((status & 0x02) == 0x02)
                interfaceList.Add(DEVICE_INTERFACE.PICC);

            if ((status & 0x04) == 0x04)
                interfaceList.Add(DEVICE_INTERFACE.SAM);

            return interfaceList;
        }

        public List<DEVICE_INTERFACE> getInterfaceStatus()
        {
            byte status = 0x00;
            List<DEVICE_INTERFACE> interfaceList;

            apduCommand = new Apdu();
            apduCommand.data = new byte[] { 0x2D, 0x00 };

            apduCommand.lengthExpected = 6;

            sendCardControl();

            if (apduCommand.response == null || apduCommand.response.Length < 6)
                throw new ReaderException("Unable to get interface status", apduCommand.response);

            status = apduCommand.response[5];


            interfaceList = new List<DEVICE_INTERFACE>();

            if ((status & 0x01) == 0x01)
                interfaceList.Add(DEVICE_INTERFACE.ICC);

            if ((status & 0x02) == 0x02)
                interfaceList.Add(DEVICE_INTERFACE.PICC);

            if ((status & 0x04) == 0x04)
                interfaceList.Add(DEVICE_INTERFACE.SAM);

            return interfaceList;
        }

    }
}
