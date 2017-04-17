using System.Windows.Input;
using Tamagochi.Abstract;

namespace Tamagochi.ViewModels
{
    public class PersonViewModel : ViewModelBase
    {
        private readonly IPerson _person;
        private readonly RoutedUICommand _feedPetCommand;
        private readonly RoutedUICommand _playWithPetCommand;
        private readonly RoutedUICommand _cleanAviaryCommand;
        private readonly RoutedUICommand _killPetCommand;

        public PetViewModel Pet { get; }
        public RoutedUICommand FeedPetCommand => _feedPetCommand;
        public RoutedUICommand PlayWithPetCommand => _playWithPetCommand;
        public RoutedUICommand CleanAviaryCommand => _cleanAviaryCommand;
        public RoutedUICommand KillPetCommand => _killPetCommand;

        public PersonViewModel(IPerson person)
        {
            _person = person;
            Pet = new PetViewModel(_person?.Pet);
            _feedPetCommand = new RoutedUICommand("Feed Pet", "FeedPet", GetType());
            _playWithPetCommand = new RoutedUICommand("Play With Pet", "PlayWithPet", GetType());
            _cleanAviaryCommand = new RoutedUICommand("Clean Aviary", "CleanAviary", GetType());
            _killPetCommand = new RoutedUICommand("Euthanize Pet", "KillPet", GetType());
        }

        public void FeedPetCommand_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            _person.FeedPet();
        }

        public void FeedPetCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        public void PlayWithPetCommand_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            _person.PlayWithPet();
        }

        public void PlayWithPetCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        public void CleanAviaryCommand_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            _person.CleanAviary();
        }

        public void CleanAviaryCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        public void KillPetCommand_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            _person.KillPet();
        }

        public void KillPetCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }
    }
}