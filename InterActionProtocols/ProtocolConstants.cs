using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterActionProtocols
{
    public enum FunctionType
    {
        IsValid,
        PutToken
    }

    public static class ProtocolConstants
    {
        public const string FunctionName = "FNNM";

        public const string InputData = "INPT";

        public const string OutputData = "OUTP";

        public const string ReturnData = "RETR";

        public const string ErrorValue = "EROR";

        public const int IsValid = 0;

        public const int PutToken = 1;
    }
}
