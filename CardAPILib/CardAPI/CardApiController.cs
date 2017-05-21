using CardAPILib.InterfaceCL;
using System;
using System.Text;

namespace CardAPILib.CardAPI
{
    public class CardApiController : CardApiAPDUMessages ,CardApiInterface 
    {
        /// <summary>
        /// 
        /// </summary>
        public CardApiController(bool isConnct = false)
        {
            if (isConnct)
                Connect2Card();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public int getUUId(out string uuid)
        {
            try
            {
                if (SelectFile() != 0)
                {
                    throw new ApplicationException(LastOperationStatus);
                }


                if (getUUID() != 0)
                {
                    throw new ApplicationException(LastOperationStatus);
                }

                uuid = CardUUID;
            }
            catch(Exception ex)
            {
                uuid = "Invalid UUID";

                throw ex;
            }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public int getPubKeyModule(out string module)
        {
            try
            {
                if (SelectFile() != 0)
                {
                    throw new ApplicationException(LastOperationStatus);
                }

                if (readPublicKeyModulus() != 0)
                {
                    throw new ApplicationException(LastOperationStatus);
                }

                module = PublicKey;
            }
            catch(Exception ex)
            {
                module = "Invalid Public Key";

                throw ex;
            }
            
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pinCode"></param>
        /// <returns></returns>
        public int userPinCodeLogin(string pinCode)
        {
            try
            {
                if (SelectFile() != 0)
                {
                    throw new ApplicationException(LastOperationStatus);
                }

                byte[] pinCodeBt = Encoding.UTF8.GetBytes(pinCode);

                if (userLogin(pinCodeBt) != 0)
                {
                    throw new ApplicationException(LastOperationStatus);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pinCode"></param>
        /// <returns></returns>
        public int adminPukCodeLogin(string pukCode)
        {
            try
            {
                if (SelectFile() != 0)
                {
                    throw new ApplicationException(LastOperationStatus);
                }

                byte[] pinCodeBt = Encoding.UTF8.GetBytes(pukCode);

                if (adminLogin(pinCodeBt) != 0)
                {
                    throw new ApplicationException(LastOperationStatus);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newPinCode"></param>
        /// <returns></returns>
        public int userChangePin(string newPinCode)
        {
            try
            {
                if (SelectFile() != 0)
                {
                    throw new ApplicationException(LastOperationStatus);
                }

                byte[] pinCodeBt = Encoding.UTF8.GetBytes(newPinCode);

                if (userChangePin(pinCodeBt) != 0)
                {
                    throw new ApplicationException(LastOperationStatus);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newPukCode"></param>
        /// <returns></returns>
        public int adminChangePuk(string newPukCode)
        {
            try
            {
                if (SelectFile() != 0)
                {
                    throw new ApplicationException(LastOperationStatus);
                }

                byte[] pinCodeBt = Encoding.UTF8.GetBytes(newPukCode);

                if (adminChangePuk(pinCodeBt) != 0)
                {
                    throw new ApplicationException(LastOperationStatus);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newPinCode"></param>
        /// <returns></returns>
        public int adminRestoreUserPin()
        {
            try
            {
                if (SelectFile() != 0)
                {
                    throw new ApplicationException(LastOperationStatus);
                }

                //123456
                if (adminResetUserPin() != 0)
                {
                    throw new ApplicationException(LastOperationStatus);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="signedToken"></param>
        /// <returns></returns>
        public int signToken(string token , out string signedToken)
        {
            signedToken = "";

            try
            {
                var result = SelectFile();

                if (result != 0)
                {
                    return result;
                }

                byte[] Token = Encoding.UTF8.GetBytes(token);

                result = signToken(Token);

                if (result != 0)
                {
                    return result;
                }
            }
            catch (Exception)
            {
                signedToken = "Invalid Signed Token";

                return 36;
            }

            signedToken = SignedToken;

            return 0;
        }
        //9175512 said 2 hona 2 kona 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="certificate"></param>
        /// <returns></returns>
        public int LoadCert(out string certificate)
        {

            certificate = "";
            try
            {
                var result = SelectFile();

                if (result != 0)
                {
                    return result;
                }

                byte[] Certificate = new byte[2048];

                result = readCA(ref Certificate);

                if (result != 0)
                {
                    return result;
                }

                byte[] destination = new byte[CertificateSize];

                Array.Copy(Certificate, destination, CertificateSize);

                //certificate = Convert.ToBase64String(destination);

                certificate = Encoding.UTF8.GetString(destination);
            }
            catch (Exception)
            {
                return 36;
            }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="certificate"></param>
        /// <returns></returns>
        public int WriteCert(string certificate)
        {
            try
            {
                if (SelectFile() != 0)
                {
                    throw new ApplicationException(LastOperationStatus);
                }

                byte[] Certificate = Encoding.UTF8.GetBytes(certificate);

                if (uploadCA(Certificate) != 0)
                {
                    throw new ApplicationException(LastOperationStatus);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return 0;
        }
    }
}
