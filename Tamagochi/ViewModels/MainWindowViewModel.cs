using System;
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
            Command.RegisterCommandBinding(GetType(), _closeCommand);
        }
    }
}