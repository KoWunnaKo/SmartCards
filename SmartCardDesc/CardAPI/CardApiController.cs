using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartCardDesc.CardAPI
{
    public class CardApiController : CardApiInterface
    {
        public int getUUId(out string uuid)
        {
            uuid = "1111";

            return 0;
        }

        public int getPubKeyModule(out string module)
        {
            module = "1111";

            return 0;
        }

        public int userPinCodeLogin(string pinCode)
        {
            return 0;
        }

        public int adminPukCodeLogin(string pinCode)
        {
            return 0;
        }

        public int userChangePin(string newPinCode)
        {
            return 0;
        }

        public int adminChangePuk(string newPukCode)
        {
            return 0;
        }

        public int adminRestoreUserPin(string newPinCode)
        {
            return 0;
        }

        public int signToken(string token , out string signedToken)
        {
            signedToken = "Signed Token";

            return 0;
        }

        public int LoadCert(out string certificate)
        {
            certificate = "Certificate";

            return 0;
        }

        public int WriteCert(string certificate)
        {
            return 0;
        }
    }
}
