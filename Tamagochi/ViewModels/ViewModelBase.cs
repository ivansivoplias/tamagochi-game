using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace Tamagochi.UI.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public bool ThrowOnInvalidPropertyName { get; protected set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected ViewModelBase(bool throwOnInvalidProperties = true)
        {
            ThrowOnInvalidPropertyName = throwOnInvalidProperties;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            VerifyPropertyName(propertyName);

            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        public virtual void RegisterCommandsForWindow(Window window)
        {
        }

        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real,
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;

                if (ThrowOnInvalidPropertyName)
                    throw new Exception(msg);
                else
                    Debug.Fail(msg);
            }
        }
    }
}