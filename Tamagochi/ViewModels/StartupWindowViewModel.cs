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

namespace Tamagochi.UI.ViewModels
{
    public class StartupWindowViewModel : ViewModelBase
    {
        private ObservableCollection<PetItemViewModel> _petCollection;
        private PetItemViewModel _selected;
        private Command _selectPetCommand;
        private Action<AbstractGame> _startGameWindowCallback;
        private static IList<PetType> _petTypes;
        private const PetEvolutionLevel _evolutionLevel = PetEvolutionLevel.Birth;

        public bool HasSelectedPet => _selected != null;

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

        static StartupWindowViewModel()
        {
            _petTypes = (Enum.GetValues(typeof(PetType)) as PetType[]).Where(x => x != PetType.None).ToList();
        }

        public StartupWindowViewModel()
        {
            _petCollection = new ObservableCollection<PetItemViewModel>();
            foreach (var item in _petTypes)
            {
                _petCollection.Add(new PetItemViewModel()
                {
                    PetName = item.ToString(),
                    Image = ImageSelector.SelectImage(item, _evolutionLevel)
                });
            }

            _selectPetCommand = Command.CreateCommand("Select pet", "SelectPet", GetType(), SelectPetCommandExecute);
        }

        public void SetStartGameWindowCallback(Action<AbstractGame> callback)
        {
            _startGameWindowCallback = callback;
        }

        public override void RegisterCommandsForWindow(Window window)
        {
            Command.RegisterCommandBinding(window, _selectPetCommand);
        }

        public override void UnregisterCommandsForWindow(Window window)
        {
            Command.UnregisterCommandBinding(window, _selectPetCommand);
        }

        private void SelectPetCommandExecute()
        {
            if (HasSelectedPet)
            {
                var petType = (PetType)Enum.Parse(typeof(PetType), _selected.PetName);

                var gameContext = App.Container.Resolve<IGameContext>();
                gameContext.Reset();
                gameContext.PetType = petType;
                var game = App.Container.Resolve<AbstractGameFactory>().MakeGame(gameContext);

                _startGameWindowCallback?.Invoke(game);
            }
        }
    }
}