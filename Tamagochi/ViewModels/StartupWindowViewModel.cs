using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamagochi.Common;
using Tamagochi.UI.Helpers;

namespace Tamagochi.UI.ViewModels
{
    public class StartupWindowViewModel : ViewModelBase
    {
        private ObservableCollection<PetItemViewModel> _petCollection;

        public ObservableCollection<PetItemViewModel> Pets
        {
            get { return _petCollection; }
            set
            {
                _petCollection = value;
                OnPropertyChanged(nameof(Pets));
            }
        }

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
        }
    }
}