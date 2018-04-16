using System;
using System.Windows.Input;

namespace GlobalSolutions
{
    /// <summary>
    /// Basic command that runs an Action
    /// </summary>
    public class RelayCommand : ICommand
    {

        #region Private Members

        /// <summary>
        /// Action to run
        /// </summary>
        private Action mAction;

        #endregion

        #region Public Members
        /// <summary>
        /// This event is called when <see cref="CanExecute(object)"/> value has changed
        /// </summary>
        public event EventHandler CanExecuteChanged =(sender,e)=> { };

        /// <summary>
        /// Relay command executes always so it is always true
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public RelayCommand(Action action)
        {
            mAction = action;
        }

        #endregion

        #region Command Methods
        /// <summary>
        /// Executes a command Action
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            mAction();
        }
        #endregion
    }
}
