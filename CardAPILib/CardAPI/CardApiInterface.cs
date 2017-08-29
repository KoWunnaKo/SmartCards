using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardAPILib.CardAPI
{
    public interface CardApiInterface
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        int getUUId(out string uuid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        int getPubKeyModule(out string module);

        /// <summary>
        /// 6 charakters , default 123456
        /// </summary>
        /// <param name="pinCode"></param>
        /// <returns></returns>
        int userPinCodeLogin(string pinCode);

        /// <summary>
        /// default 12345678
        /// </summary>
        /// <param name="pinCode"></param>
        /// <returns></returns>
        int adminPukCodeLogin(string pinCode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newPinCode"></param>
        /// <returns></returns>
        int userChangePin(string newPinCode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newPukCode"></param>
        /// <returns></returns>
        int adminChangePuk(string newPukCode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newPinCode"></param>
        /// <returns></returns>
        int adminRestoreUserPin();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="signedToken"></param>
        /// <returns></returns>
        int signToken(string token, out string signedToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="certificate"></param>
        /// <returns></returns>
        int LoadCert(out string certificate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="certificate"></param>
        /// <returns></returns>
        int WriteCert(string certificate);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int OpenCardDR();


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int OpenCardVR();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int SaveCertificateCardPKI();

    }
}
