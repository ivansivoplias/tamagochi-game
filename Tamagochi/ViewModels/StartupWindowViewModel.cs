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
        private static IList<PetType> _petTypes;
        private const PetEvolutionLevel _evolutionLevel = PetEvolutionLevel.Birth;

        public event EventHandler PetSelectedMessage = delegate { };

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
            PetSelectedMessage.Invoke(null, EventArgs.Empty);
        }
    }
}