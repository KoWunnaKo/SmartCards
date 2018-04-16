using System.ComponentModel;

namespace GlobalSolutions
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The event fired when any UI item changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Call this ti fire <see cref="PropertyChanged"/> event
        /// </summary>
        /// <param name="name"></param>
        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
