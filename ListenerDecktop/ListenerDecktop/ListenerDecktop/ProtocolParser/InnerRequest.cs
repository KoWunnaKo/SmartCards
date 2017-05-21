using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListenerDecktop.ProtocolParser
{
    public class InnerRequest : BaseRequest
    {
        public InnerRequest()
        {
            //ParseMessage(message);
        }

        public string  ParseInputMessage(string message)
        {
            lastErrorNumber = "00";

            try
            {
                ParseMessage(message);
            }
            catch(ApplicationException)
            {
                //errorCode = lastErrorNumber;
            }

            return lastErrorNumber;
        }

        private bool _isFirstReq;

        public bool IsFirstReq
        {
            get
            {

                _isFirstReq = false;

                if (EntryData.Equals("0"))
                {
                    _isFirstReq = true;

                    return _isFirstReq;
                }

                return _isFirstReq;
            }

            private set
            {
                _isFirstReq = value;
            }
        }


        private bool _isSecondReq;

        public bool IsSecondReq
        {
            get
            {

                _isSecondReq = false;

                if (EntryData.Equals("1"))
                {
                    _isSecondReq = true;

                    return _isSecondReq;
                }

                return _isSecondReq;
            }

            private set
            {
                _isSecondReq = value;
            }
        }

        private string _token;

        public string Token
        {
            get
            {
                if (IsSecondReq)
                {
                    _token = ContentData;
                }
                else
                {
                    _token = string.Empty;
                }
                
                return _token;
            }
            private set
            {
                _token = value;
            }
        }
    }
}
