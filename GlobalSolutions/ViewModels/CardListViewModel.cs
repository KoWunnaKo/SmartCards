using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace GlobalSolutions
{
    public class CardListViewModel : BaseViewModel
    {


        /// <summary>
        /// List of all <see cref="CardControlViewModel"/>s
        /// </summary>
        public static ObservableCollection<CardControlViewModel> Cards { set; get; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public CardListViewModel()
        {
            Cards = new ObservableCollection<CardControlViewModel>
            {
                //new CardControlViewModel {IsBusy=false,CardName= "Card "+(Cards==null?1:Cards.Count+1).ToString(),Name="First one", Key="Something",Status="Working" },
                //new CardControlViewModel {IsBusy=false,CardName= "Card "+(Cards==null?2:Cards.Count+1).ToString(),Name="Second one", Key="Anything",Status="Working" },
                //new CardControlViewModel {IsBusy=false,CardName= "Card "+(Cards==null?4:Cards.Count+1).ToString(),Name="Third one", Key="Nothing",Status="Working" },
                //new CardControlViewModel {IsBusy=false,CardName= "Card "+(Cards==null?4:Cards.Count+1).ToString(),Name="Fourth one", Key="Morething",Status="Working" },
                //new CardControlViewModel {IsBusy=true,CardName= "Card "+(Cards==null?5:Cards.Count+1).ToString(),Name="Fourth one", Key="Morething",Status="Working" },
            };

        }
    }
}
