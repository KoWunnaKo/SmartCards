using System;
using System.Windows;

namespace GlobalSolutions
{
    /// <summary>
    /// Base attached property to replace the vanilla WPF attached property                        
    /// </summary>
    /// <typeparam name="Parent">Parent class to attached property</typeparam>
    /// <typeparam name="Property">Type of attached property</typeparam>
    public abstract class BaseAttachedProperty<Parent,Property>
        where Parent: BaseAttachedProperty<Parent,Property>, new()
    {

        #region 

        private static Parent mInstance;

        private static bool IsFirst = true;

        #endregion 

        #region Parent
        /// <summary>
        /// Singleton instance of parent class
        /// </summary>
        public static Parent Instance { get { return IsFirst ? new Parent() : mInstance; } private set { mInstance = value; IsFirst = !IsFirst; } } 
        #endregion

        #region Public Events

        /// <summary>
        /// Fired when value changed
        /// </summary>
        public event Action<DependencyObject, DependencyPropertyChangedEventArgs> ValueChanged = (sender, e) => { };            

        #endregion

        #region Default constructor

        //public BaseAttachedProperty()
        //{
        //    Instance = new Parent();
        //}

        #endregion

        #region Attached Property Definitions

        /// <summary>
        /// Attached property for this class
        /// </summary>
        private static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached("Value", typeof(Property), typeof(BaseAttachedProperty<Parent, Property>),new PropertyMetadata(new PropertyChangedCallback(OnValuePropertyChanged)));

        /// <summary>
        /// The callback event when <see cref="ValueProperty"/> is changed
        /// </summary>
        /// <param name="d">The UI element that had its property changed</param>
        /// <param name="e">The argument for the event</param>
        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Call the parent event
            Instance.OnValueChanged(d, e);

            // Call event listeners
            Instance.ValueChanged(d, e);
        }

        /// <summary>
        /// Get the value of attached property
        /// </summary>
        /// <param name="d">Element to get value from</param>
        /// <returns></returns>
        public static Property GetValue(DependencyObject d) { return (Property)d.GetValue(ValueProperty); }

        /// <summary>
        /// Set the value of attached property
        /// </summary>
        /// <param name="d">Element to set value to</param>
        /// <param name="value">Value to set</param>
        public static void SetValue(DependencyObject d, Property value) {d.SetValue(ValueProperty, value);}
        #endregion

        #region Event Methods

        /// <summary>
        /// The method is called when any attached propety of this type changed
        /// </summary>
        /// <param name="sender">UI element property changed for</param>
        /// <param name="e">The argument for this event</param>
        public virtual void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { }                           

        #endregion

    }
}
