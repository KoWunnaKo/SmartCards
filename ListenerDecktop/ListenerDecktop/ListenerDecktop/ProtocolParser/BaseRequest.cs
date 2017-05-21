using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListenerDecktop.ProtocolParser
{
    public enum RequestType
    {
        IsCardValid,

        PutToken, 

        Error
    }

    public class BaseRequest
    {
        private static ILog log = log4net.LogManager.GetLogger(typeof(BaseRequest));

        public RequestType requestTp { get; set; }

        public string EntryTag { get; set; }

        public string EntryDataLength { get; set; }

        public string EntryData { get; set; }

        public string ContentTag { get; set; }

        public string ContentDataLength { get; set; }

        public string ContentData { get; set; }

        public string lastError { get; set; }

        public string lastErrorNumber { get; set; }

        public void ParseMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                lastError = "Invalid total message length";
                lastErrorNumber = "33";

                log.Error("Invalid total message length");

                throw new ApplicationException("Invalid total message length");
            }

            if (message.Length < 6)
            {
                lastError = "Invalid total message length";
                lastErrorNumber = "33";

                log.Error("Invalid total message length");

                throw new ApplicationException("Invalid total message length");
            }

            EntryTag = message.Substring(0, 4);

            if (!EntryTag.Equals(Constants.FunctionNameTag))
            {
                lastError = "Invalid EntryTag";
                lastErrorNumber = "34";

                log.Error("Invalid EntryTag");

                throw new ApplicationException("Invalid total message length");
            }

            EntryDataLength = message.Substring(4, 1);

            EntryData = message.Substring(5, 1);

            if (EntryData.Equals("0"))
            {
                requestTp = RequestType.IsCardValid;
                return;
            }
            else if (EntryData.Equals("1"))
            {
                requestTp = RequestType.PutToken;

                if (message.Length < 10)
                {
                    lastError = "Invalid total message length";
                    lastErrorNumber = "33";

                    log.Error("Invalid total message length");

                    throw new ApplicationException("Invalid total message length");
                }
            }
            else
            {
                requestTp = RequestType.Error;
                lastError = "Invalid Content Data length";
                lastErrorNumber = "33";

                log.Error("Invalid Content Data length");

                throw new ApplicationException("Invalid Operation ");
            }

            ContentTag = message.Substring(6, 4);

            ContentDataLength = message.Substring(10, 6);

            int contentLength;

            if (!int.TryParse(ContentDataLength, out contentLength))
            {
                lastError = "Invalid Content Data length";
                lastErrorNumber = "33";

                log.Error("Invalid Content Data length");

                throw new ApplicationException("Invalid Content Data length");
            }

            ContentData = message.Substring(16, contentLength);

        }
    }
}
