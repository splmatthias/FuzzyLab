using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Threading;
using WpfBase.views;

namespace WpfBase.viewModels
{
    /// <summary>
    /// Defines the base class for a _view model.
    /// </summary>
    [Serializable]
    public abstract class ViewModel<TView> : INotifyPropertyChanged where TView : class, IView
    {
        private readonly TView _view;
        
        /// <summary>
        /// Initializes a new instance of the ViewModel class and attaches itself as <c>DataContext</c> to the _view.
        /// </summary>
        /// <param name="view">The _view.</param>
        protected ViewModel(TView view)
        {
            if (view == null)
            {
                return; //throw new ArgumentNullException("view"); 
            }
            _view = view;

            // Check if the code is running within the WPF application model
            if (SynchronizationContext.Current is DispatcherSynchronizationContext)
            {
                // Set DataContext of the _view has to be delayed so that the ViewModel can initialize the internal data (e.g. Commands)
                // before the _view starts with DataBinding.
                Dispatcher.CurrentDispatcher.BeginInvoke((Action)delegate
                {
                    _view.DataContext = this;
                });
            }
            else
            {
                // When the code runs outside of the WPF application model then we set the DataContext immediately.
                view.DataContext = this;
            }
        }


        /// <summary>
        /// Gets the associated _view.
        /// </summary>
        public TView View { get { return _view; } }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Set the property with the specified value. If the value is not equal with the field then the field is
        /// set, a PropertyChanged event is raised and it returns true.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="field">Reference to the backing field of the property.</param>
        /// <param name="value">The new value for the property.</param>
        /// <param name="propertyName">The property name. </param>
        /// <returns>True if the value has changed, false if the old and new value were equal.</returns>
        protected bool SetProperty<T>(ref T field, T value, string propertyName)
        {
            if (Equals(field, value)) { return false; }

            field = value;
            RaisePropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Raises the <see cref="E:PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The property name of the property that has changed.</param>
        protected void RaisePropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the <see cref="E:PropertyChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }

    /// <summary>
    /// Defines the base class for a _view model.
    /// </summary>
    [Serializable]
    public abstract class ViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Set the property with the specified value. If the value is not equal with the field then the field is
        /// set, a PropertyChanged event is raised and it returns true.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="field">Reference to the backing field of the property.</param>
        /// <param name="value">The new value for the property.</param>
        /// <param name="propertyName">The property name. </param>
        /// <returns>True if the value has changed, false if the old and new value were equal.</returns>
        protected bool SetProperty<T>(ref T field, T value, string propertyName)
        {
            if (Equals(field, value)) { return false; }

            field = value;
            RaisePropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Raises the <see cref="E:PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The property name of the property that has changed.</param>
        protected void RaisePropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the <see cref="E:PropertyChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
