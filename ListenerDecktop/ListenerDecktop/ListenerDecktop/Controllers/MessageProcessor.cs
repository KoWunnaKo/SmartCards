using ListenerDecktop.CardAPI;
using ListenerDecktop.ProtocolParser;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListenerDecktop.Controllers
{
    public class MessageProcessor
    {
        private static ILog log = log4net.LogManager.GetLogger(typeof(MessageProcessor));

        public static string ProccIncomingMessage(string message)
        {
            string returnMessage = string.Empty;

            var innerRequest = new InnerRequest();

            var outerRequest = new OuterRequest();

            var cardApi = new CardApiController();

            try
            {
                outerRequest.ErrorCode = innerRequest.ParseInputMessage(message);

                outerRequest.SetInnerReq(innerRequest);

                if (!outerRequest.ErrorCode.Equals("00"))
                {
                    returnMessage = outerRequest.GetReplyMessage();
                    throw new ApplicationException("Invalid Protokol");
                }

                if (innerRequest.requestTp == RequestType.IsCardValid)
                {
                    string certificate = string.Empty;

                    int errorCode = cardApi.LoadCert(out certificate);

                    if (errorCode == 0)
                    {
                        outerRequest.Certificate = certificate;
                    }
                    else
                    {
                        outerRequest.ErrorCode = errorCode.ToString().PadLeft(2,'0');
                    }
                }
                else if (innerRequest.requestTp == RequestType.PutToken)
                {
                    var form = new PinAutorizationForm();

                    if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string pin = form.pinNumber;

                        int loginError = cardApi.userPinCodeLogin(pin);

                        if (loginError == 0)
                        {
                            string signedToken = string.Empty;

                            int signError = cardApi.signToken(innerRequest.Token, out signedToken);

                            if (signError == 0)
                            {
                                outerRequest.SignedToken = signedToken;
                            }
                            else
                            {
                                outerRequest.ErrorCode = signError.ToString().PadLeft(2, '0'); ;
                            }
                        }
                        else
                        {
                            outerRequest.ErrorCode = loginError.ToString().PadLeft(2, '0'); ;
                        }
                    }
                    else
                    {
                        outerRequest.ErrorCode = ""; //User Denied PIN Enter
                    }
                }
                else
                {
                    throw new ApplicationException("Invalid RequestType");
                }

                returnMessage = outerRequest.GetReplyMessage();
            }
            catch(Exception ex)
            {
                log.Error(ex.ToString());
            }

            return returnMessage;
        }
    }
}
