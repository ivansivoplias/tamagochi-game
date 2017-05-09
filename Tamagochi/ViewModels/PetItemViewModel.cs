using System.Windows.Media;

namespace Tamagochi.UI.ViewModels
{
    public class PetItemViewModel : ViewModelBase
    {
        private string _petName;
        private ImageSource _image;

        public string PetName
        {
            get { return _petName; }
            set
            {
                _petName = value;
                OnPropertyChanged(nameof(PetName));
            }
        }

        public ImageSource Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }
    }
}