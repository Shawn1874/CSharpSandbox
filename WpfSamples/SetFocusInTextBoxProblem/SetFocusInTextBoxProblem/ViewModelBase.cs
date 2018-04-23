using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SetFocusInTextBoxProblem
{
    class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Provides a convenient way of setting properties in a more compact form within derived
        /// view models.
        /// </summary>
        /// <typeparam name="T">The typename of the property</typeparam>
        /// <param name="member">reference to the backing field</param>
        /// <param name="val">the value to assign to the backing field</param>
        /// <param name="propertyName"The name of the property set by the attribute automatically></param>
        protected virtual void SetProperty<T>(ref T member, T val,
            [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(member, val)) return;

            member = val;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raise a property changed event without setting the property.  Prefer using the 
        /// SetProperty function but this can be used occasionally when a field is modified
        /// in a function but the event has to be raised for the associated property.
        /// </summary>
        /// <param name="prop">The name of the property</param>
        internal void RaisePropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
