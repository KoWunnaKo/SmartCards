using CardAPILib.InterfaceCL;
using System;
using System.Text;

namespace CardAPILib.CardAPI
{
    /// <summary>
    /// Class to Realize Card Operations for SmartCard UzInfocom Library
    /// </summary>
    public class CardApiController : CardApiMessages, CardApiInterface 
    {
        /// <summary>
        /// Constructor of CardApiController Class
        /// </summary>
        /// <param name="isConnct">Allow Create Class with opened or not opened connection</param>
        public CardApiController(bool isConnct = false)
        {
            if (isConnct)
            {
                if (Connect2Card() != 0)
                {

                }
            }
        }


        /// <summary>
        /// This command queries the smart card for the 20 byte UUID which is derived by using SHA256 to derive a 32 byte hash from the RSA Public
        /// Modulus of the card and then extracting the first 20 bytes of the hashed result as the UUID for the smart card. 
        /// </summary>
        /// <param name="uuid">Out parametr Returns UUID</param>
        /// <returns>Status of Operation 0 - Success</returns>
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
        ///  This command allows the reading of the RSA Public Key Modulus from the card. 
        ///  No login is required for issuing this command. The RSA Public Key Exponent will always be fixed to 0x010001. 
        /// </summary>
        /// <param name="module">Out Parametr Returns Module of Public Key</param>
        /// <returns>Status of Operation 0 - Success</returns>
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

                throw new ApplicationException(LastOperationStatus);
            }
            
            return 0;
        }

        /// <summary>
        /// This command is to be issued by the card’s user to login to the smart card before being capable of performing the RSA signature operation and the changing of user’s PIN operation. 
        /// Userlogin PIN have a maximum retry of 3 retries before the User PIN becomes blocked. 
        /// </summary>
        /// <param name="pinCode">Input parametr PIN Code</param>
        /// <returns>Status of Operation 0 - Success</returns>
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
            catch (Exception)
            {
                throw new ApplicationException(LastOperationStatus);
            }

            return 0;
        }

        /// <summary>
        ///  This command is to be issued by the card’s administrator to login to the smart card before being capable of performing card administrative operations for resetting and unblocking the User PIN, uploading of card certificate and changing the administrator’s own Admin PIN.
        ///  Administrative login PIN have a maximum retry of 3 retries before the Admin PIN becomes blocked. 
        /// </summary>
        /// <param name="pukCode">Input parametr PUK code of Admin</param>
        /// <returns>Status of Operation 0 - Success</returns>
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
            catch (Exception)
            {
                throw new ApplicationException(LastOperationStatus);
            }

            return 0;
        }

        /// <summary>
        /// This command is for a user to issue for the changing of the User PIN. 
        /// The user must already be logged in to proceed to issue this command to change User PIN. 
        /// </summary>
        /// <param name="newPinCode">Input Parametr New PIN code</param>
        /// <returns>Status of Operation 0 - Success</returns>
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
        /// This command is for am administrator to issue for the changing of the PUK. 
        /// The administrator must already be logged in to proceed to issue this command to change the PUK 
        /// </summary>
        /// <param name="newPukCode">Input Parametr New PUK code</param>
        /// <returns>Status of Operation 0 - Success</returns>
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
        /// This command is for the administrator to reset and unblock the User PIN to the default User PIN (123456). 
        /// The administrator must already be logged in to proceed to issue this command. 
        /// </summary>
        /// <param name="newPinCode">Input Parametr New PIN code</param>
        /// <returns>Status of Operation 0 - Success</returns>
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
        /// This command allows the card user to create an RSA digital signature by feeding a 64 byte SHA-512 hash. 
        /// The card user must already be logged in to proceed the creation of RSA signature. 
        /// </summary>
        /// <param name="token">Input parametr Token to sign</param>
        /// <param name="signedToken">Out parametr Signed Token</param>
        /// <returns>Status of Operation 0 - Success</returns>
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

        /// <summary>
        /// This command allows the traversing and reading of the storage area containing the CA Signed Certificate without the need to login. 
        /// The minimum and maximum range of bytes (in 2 byte ‘short’ representation) are fed into the command to allow traversing and read of data stored under the condition that the length of data to be read out should not exceed 256 bytes.
        /// </summary>
        /// <param name="certificate">Out Parametr return certificate</param>
        /// <returns>Status of Operation 0 - Success</returns>
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
        ///  This command allows the upload of a CA Signed Certificate. The new CA Signed Certificate will overwrite any old CA Signed Certificate existing in the storage space. 
        ///  This command requires an administrator to be logged in to proceed to issue this command. 
        /// </summary>
        /// <param name="certificate">Input parametr certificate to write</param>
        /// <returns>Status of Operation 0 - Success</returns>
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
