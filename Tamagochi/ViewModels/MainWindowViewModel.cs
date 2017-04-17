using System;
using System.Windows.Input;

namespace Tamagochi.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly RoutedUICommand _closeCommand;
        private Action _closeWindow;

        public PersonViewModel Person { get; set; }

        public RoutedUICommand CloseCommand => _closeCommand;

        public MainWindowViewModel(Action closeWindow)
        {
            _closeWindow = closeWindow;
            _closeCommand = new RoutedUICommand("Close", "Close", GetType());
            Person = new PersonViewModel(null);
        }

        public void CloseCommand_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            _closeWindow?.Invoke();
        }

        public void CloseCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _closeWindow != null;
            e.Handled = true;
        }
    }
}