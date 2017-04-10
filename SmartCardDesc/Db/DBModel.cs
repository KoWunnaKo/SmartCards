using SmartCardDesc.EntityModel;
using System;
using System.ComponentModel;

namespace SmartCardDesc.Db
{
    [Serializable]
    public abstract class DbModel:INotifyPropertyChanged
    {
        
        public static DbConnection db;

        public static SmartCardDs dataSetSc;

        //------------------------------------------------------
        //
        //  Constructors
        //
        //------------------------------------------------------
        #region Constructors
        #endregion

        //------------------------------------------------------
        //
        //  Properties
        //
        //------------------------------------------------------
        #region Properties
        #endregion

        //------------------------------------------------------
        //
        //  Overrides
        //
        //------------------------------------------------------
        #region Overrides

        /// <summary>
        /// When overidden in ad derived class, model is added to database.
        /// </summary>
        internal void AddItem() { }

        #endregion

        //------------------------------------------------------
        //
        //  Public Methods
        //
        //------------------------------------------------------
        #region Public Methods
        #endregion

        //------------------------------------------------------
        //
        //  Internal Methods
        //
        //------------------------------------------------------
        #region Internal Methods
        #endregion

        //------------------------------------------------------
        //
        //  Private Methods
        //
        //------------------------------------------------------
        #region Private Methods

        /// <summary>
        /// Raises PropertyChanged event when specified property changes.
        /// </summary>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        //------------------------------------------------------
        //
        //  Event
        //
        //------------------------------------------------------
        #region Event
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        //------------------------------------------------------
        //
        //  Fields
        //
        //-----------------------------------------------------
        #region Fields
        #endregion
    }    
}
