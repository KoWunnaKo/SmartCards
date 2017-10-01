using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iso7816Lib
{
    public class ISO7816
    {
        static  byte OFFSET_CLA = (byte)0;
        static  byte OFFSET_INS = (byte)1;
        static  byte OFFSET_P1 = (byte)2;
        static  byte OFFSET_P2 = (byte)3;
        static  byte OFFSET_LC = (byte)4;
        static  byte OFFSET_CDATA = (byte)5;

        static  byte CLA_ISO7816 = (byte)0x00;

        static  byte INVALIDATE_CHV = 0x04;
        static  byte INS_ERASE_BINARY = 0x0E;
        static  byte INS_VERIFY = 0x20;
        static  byte INS_CHANGE_CHV = 0x24;
        static  byte INS_UNBLOCK_CHV = 0x2C;
        static  byte INS_DECREASE = 0x30;
        static  byte INS_INCREASE = 0x32;
        static  byte INS_DECREASE_STAMPED = 0x34;
        static  byte INS_REHABILITATE_CHV = 0x44;
        static  byte INS_MANAGE_CHANNEL = 0x70;
        static  byte INS_EXTERNAL_AUTHENTICATE = (byte)0x82;
        static  byte INS_MUTUAL_AUTHENTICATE = (byte)0x82;
        static  byte INS_GET_CHALLENGE = (byte)0x84;
        static  byte INS_ASK_RANDOM = (byte)0x84;
        static  byte INS_GIVE_RANDOM = (byte)0x86;
        static  byte INS_INTERNAL_AUTHENTICATE = (byte)0x88;
        static  byte INS_SEEK = (byte)0xA2;
        static  byte INS_SELECT = (byte)0xA4;
        static  byte INS_SELECT_FILE = (byte)0xA4;
        static  byte INS_CLOSE_APPLICATION = (byte)0xAC;
        static  byte INS_READ_BINARY = (byte)0xB0;
        static  byte INS_READ_BINARY2 = (byte)0xB1;
        static  byte INS_READ_RECORD = (byte)0xB2;
        static  byte INS_READ_RECORD2 = (byte)0xB3;
        static  byte INS_READ_RECORDS = (byte)0xB2;
        static  byte INS_READ_BINARY_STAMPED = (byte)0xB4;
        static  byte INS_READ_RECORD_STAMPED = (byte)0xB6;
        static  byte INS_GET_RESPONSE = (byte)0xC0;
        static  byte INS_ENVELOPE = (byte)0xC2;
        static  byte INS_GET_DATA = (byte)0xCA;
        static  byte INS_WRITE_BINARY = (byte)0xD0;
        static  byte INS_WRITE_RECORD = (byte)0xD2;
        static  byte INS_UPDATE_BINARY = (byte)0xD6;
        static  byte INS_LOAD_KEY_FILE = (byte)0xD8;
        static  byte INS_PUT_DATA = (byte)0xDA;
        static  byte INS_UPDATE_RECORD = (byte)0xDC;
        static  byte INS_CREATE_FILE = (byte)0xE0;
        static  byte INS_APPEND_RECORD = (byte)0xE2;
        static  byte INS_DELETE_FILE = (byte)0xE4;
        static  byte INS_PSO = (byte)0x2A;
        static  byte INS_MSE = (byte)0x22;

        static  short SW_BYTES_REMAINING_00 = (short)0x6100;
        static  short SW_END_OF_FILE = (short)0x6282;
        static  short SW_LESS_DATA_RESPONDED_THAN_REQUESTED = (short)0x6287;
        static  short SW_WRONG_LENGTH = (short)0x6700;
        static  short SW_SECURITY_STATUS_NOT_SATISFIED = (short)0x6982;
        static  short SW_FILE_INVALID = (short)0x6983;
        static  short SW_DATA_INVALID = (short)0x6984;
        static  short SW_CONDITIONS_NOT_SATISFIED = (short)0x6985;
        static  short SW_COMMAND_NOT_ALLOWED = (short)0x6986;
        static  short SW_EXPECTED_SM_DATA_OBJECTS_MISSING = (short)0x6987;
        static  short SW_SM_DATA_OBJECTS_INCORRECT = (short)0x6988;
        static  short SW_APPLET_SELECT_FAILED = (short)0x6999;
        static  short SW_KEY_USAGE_ERROR = (short)0x69C1;
        static  short SW_WRONG_DATA = (short)0x6A80;
        static  short SW_FILEHEADER_INCONSISTENT = (short)0x6A80;
        static  short SW_FUNC_NOT_SUPPORTED = (short)0x6A81;
        static  short SW_FILE_NOT_FOUND = (short)0x6A82;
        static  short SW_RECORD_NOT_FOUND = (short)0x6A83;
        static  short SW_FILE_FULL = (short)0x6A84;
        static  short SW_OUT_OF_MEMORY = (short)0x6A84;
        static  short SW_INCORRECT_P1P2 = (short)0x6A86;
        static  short SW_KEY_NOT_FOUND = (short)0x6A88;
        static  short SW_WRONG_P1P2 = (short)0x6B00;
        static  short SW_CORRECT_LENGTH_00 = (short)0x6C00;
        static  short SW_INS_NOT_SUPPORTED = (short)0x6D00;
        static  short SW_CLA_NOT_SUPPORTED = (short)0x6E00;
        static  short SW_UNKNOWN = (short)0x6F00;
        static  short SW_CARD_TERMINATED = (short)0x6FFF;
        static  int SW_NO_ERROR = (int)0x9000;
    }
}
