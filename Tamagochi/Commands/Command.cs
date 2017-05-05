using System;
using System.Windows;
using System.Windows.Input;

namespace Tamagochi.UI.Commands
{
    public class Command : RoutedUICommand
    {
        private Func<bool> _canExecute;
        private Action _execute;

        public Command(string text, string name, Type ownerType, Func<bool> canExecute, Action execute) : base(text, name, ownerType)
        {
            _canExecute = canExecute;
            _execute = execute;
        }

        public void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _canExecute == null || _canExecute();
            e.Handled = true;
        }

        public void Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _execute?.Invoke();
        }

        public static void RegisterCommandBinding(Window window, Command command)
        {
            window.CommandBindings.Add(new CommandBinding(command, command.Executed, command.CanExecute));
            //CommandManager.RegisterClassCommandBinding(type, new CommandBinding(command, command.Executed, command.CanExecute));
        }
    }
}