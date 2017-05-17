using Autofac;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Tamagochi.Common;
using Tamagochi.Infrastructure.Abstract;
using Tamagochi.UI.Commands;
using Tamagochi.UI.Helpers;
using Tamagochi.UI.Views;

namespace Tamagochi.UI.ViewModels
{
    public class StartupWindowViewModel : ViewModelBase
    {
        private ObservableCollection<PetItemViewModel> _petCollection;
        private PetItemViewModel _selected;
        private Command _selectPetCommand;
        private Action _selectPetCallback;

        public ObservableCollection<PetItemViewModel> Pets
        {
            get { return _petCollection; }
            set
            {
                _petCollection = value;
                OnPropertyChanged(nameof(Pets));
            }
        }

        public PetItemViewModel SelectedPet
        {
            get { return _selected; }
            set
            {
                _selected = value;
                OnPropertyChanged(nameof(SelectedPet));
            }
        }

        public ICommand SelectPetCommand => _selectPetCommand;

        public StartupWindowViewModel()
        {
            var petTypes = (Enum.GetValues(typeof(PetType)) as PetType[]).Where(x => x != PetType.None).ToList();
            var evolutionLevel = PetEvolutionLevel.Birth;
            var list = new List<PetItemViewModel>();
            foreach (var item in petTypes)
            {
                list.Add(new PetItemViewModel()
                {
                    PetName = item.ToString(),
                    Image = ImageSelector.SelectImage(item, evolutionLevel)
                });
                _petCollection = new ObservableCollection<PetItemViewModel>(list);
            }

            _selectPetCommand = Command.CreateCommand("Select pet", "SelectPet", GetType(), SelectPetCommandExecute);
        }

        public void SetSelectPetCallback(Action callback)
        {
            _selectPetCallback = callback;
        }

        public override void RegisterCommandsForWindow(Window window)
        {
            Command.RegisterCommandBinding(window, _selectPetCommand);
        }

        private void SelectPetCommandExecute()
        {
            if (_selected != null)
            {
                var petType = (PetType)Enum.Parse(typeof(PetType), _selected.PetName);

                var gameContext = App.Container.Resolve<IGameContext>();
                gameContext.Reset();
                gameContext.PetType = petType;
                var game = App.Container.Resolve<AbstractGameFactory>().MakeGame(gameContext);

                var viewModel = new GameViewModel(game, (s, arg) => MessageBox.Show("Pet is dead"));
                var mainWindow = new MainWindow(viewModel);
                mainWindow.Show();

                _selectPetCallback?.Invoke();
            }
        }
    }
}