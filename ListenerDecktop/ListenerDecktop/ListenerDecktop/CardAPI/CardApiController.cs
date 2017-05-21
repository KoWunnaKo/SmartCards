namespace ListenerDecktop.CardAPI
{
    public class CardApiController : CardApiInterface
    {
        CardAPILib.CardAPI.CardApiController cbt;

        public CardApiController()
        {
            
        }

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

            if ((CardInternals.hcard == 0) && (CardInternals.hcontect == 0))
            {
                cbt = new CardAPILib.CardAPI.CardApiController(true);

                CardInternals.hcard = cbt.hCard;
                CardInternals.hcontect = cbt.hContext;
            }
            else
            {
                cbt = new CardAPILib.CardAPI.CardApiController();

                cbt.hCard = CardInternals.hcard;
                cbt.hContext = CardInternals.hcontect;

            }

            var result = cbt.userPinCodeLogin(pinCode);

            if (result != 0)
            {
                return result;
            }

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
            signedToken = "";

            var result = cbt.signToken(token, out signedToken);

            if (result != 0)
            {
                return result;
            }

            return 0;
        }

        public int LoadCert(out string certificate)
        {
            certificate = "";

            if ((CardInternals.hcard == 0) && (CardInternals.hcontect == 0))
            {
                cbt = new CardAPILib.CardAPI.CardApiController(true);

                CardInternals.hcard = cbt.hCard;
                CardInternals.hcontect = cbt.hContext;
            }
            else
            {
                cbt = new CardAPILib.CardAPI.CardApiController();

                cbt.hCard = CardInternals.hcard;
                cbt.hContext = CardInternals.hcontect;

            }

            var result = cbt.LoadCert(out certificate);

            if (result != 0)
            {
                return result;
            }

            return 0;
        }

        public int WriteCert(string certificate)
        {
            return 0;
        }
    }
}
