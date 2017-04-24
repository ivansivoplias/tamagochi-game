using System;
using System.Windows.Input;
using Tamagochi.Commands;

namespace Tamagochi.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly Command _closeCommand;
        private Action _closeWindow;

        public PetViewModel Pet { get; set; }

        public Command CloseCommand => _closeCommand;

        public MainWindowViewModel(Action closeWindow)
        {
            _closeWindow = closeWindow;
            _closeCommand = new Command("Close", "Close", GetType(), null, null);
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