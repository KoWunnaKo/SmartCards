using GemCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GID_Client.ViewModel
{
    public class CardActionTracker
    {
        private CardNative _card;

        public CardActionTracker()
        {
            _card = new CardNative();

            _card.OnCardInserted += _card_OnCardInserted;
            _card.OnCardRemoved += _card_OnCardRemoved;
        }

        private void _card_OnCardRemoved(string reader)
        {
            throw new NotImplementedException();
        }

        private void _card_OnCardInserted(string reader)
        {
            throw new NotImplementedException();
        }
    }
}
