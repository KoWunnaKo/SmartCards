using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListenerDecktop.ProtocolParser
{
    public class OuterRequest : BaseRequest
    {

        private string _certificate;

        public string Certificate
        {
            get
            {
                return _certificate;
            }

            set
            {
                _certificate = value;
            }
        }

        private string _errorCode;

        public string ErrorCode
        {
            get
            {
                return _errorCode;
            }

            set
            {
                _errorCode = value;
            }
        }

        private string _signedToken;

        public string SignedToken
        {
            get { return _signedToken; }
            set { _signedToken = value; }
        }

        private InnerRequest _request;

        private string _replyMessage;
        
        public OuterRequest()
        {
            ErrorCode = "00";
            _replyMessage = string.Empty;
        }

        public void SetInnerReq(InnerRequest request)
        {
            _request = request;
        }

        public string GetReplyMessage()
        {
            if (_request.requestTp == RequestType.IsCardValid)
            {
                _replyMessage = string.Format("{0}{1}{2}",
                    Constants.ReturnDataTag , 1 , (int)RequestType.IsCardValid);

                if (!string.IsNullOrEmpty(Certificate))
                {
                    _replyMessage = string.Format("{0}{1}{2}{3}", _replyMessage, Constants.OutputDataTag,
                        Certificate.Length.ToString().PadLeft(6, '0'), Certificate);
                }
                else if ((!string.IsNullOrEmpty(ErrorCode)) && (!ErrorCode.Equals("00")))
                {
                    _replyMessage = string.Format("{0}{1}{2}{3}", _replyMessage, Constants.ErrorDataTag,
                        ErrorCode.Length.ToString().PadLeft(6, '0'), ErrorCode);
                }
                else
                {
                    throw new ApplicationException("Invalid IsCardValid construction!!!");
                }
            }
            else if (_request.requestTp == RequestType.PutToken)
            {
                _replyMessage = string.Format("{0}{1}{2}",
                    Constants.ReturnDataTag, 1, (int)RequestType.PutToken);

                if (!string.IsNullOrEmpty(SignedToken))
                {
                    _replyMessage = string.Format("{0}{1}{2}{3}", _replyMessage, Constants.OutputDataTag,
                        SignedToken.Length.ToString().PadLeft(6, '0'), SignedToken);
                }
                else if (!string.IsNullOrEmpty(ErrorCode) && (!ErrorCode.Equals("00")))
                {
                    _replyMessage = string.Format("{0}{1}{2}{3}", _replyMessage, Constants.ErrorDataTag,
                        ErrorCode.Length.ToString().PadLeft(6, '0'), ErrorCode);
                }
                else
                {
                    throw new ApplicationException("Invalid PutToken construction!!!");
                }
            }
            else if (_request.requestTp == RequestType.Error)
            {
                _replyMessage = string.Format("{0}{1}{2}",
                    Constants.ReturnDataTag, 1, (int)RequestType.Error);

                _replyMessage = string.Format("{0}{1}{2}{3}", _replyMessage, Constants.ErrorDataTag,
                    ErrorCode.Length.ToString().PadLeft(6, '0'), ErrorCode);
            }
            else
            {
                throw new ApplicationException("Invalid GetReplyMessage construction!!!");
            }

            return _replyMessage;
        }
    }
}
