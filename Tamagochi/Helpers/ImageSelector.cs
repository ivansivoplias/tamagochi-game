using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;
using Tamagochi.Common;

namespace Tamagochi.Helpers
{
    public class ImageSelector
    {
        private static Dictionary<PetType, List<BitmapImage>> _images;
        private const string _imageFormat = "pack://application:,,,/Images/{0}/{1}-{2}.png";

        static ImageSelector()
        {
            _images = new Dictionary<PetType, List<BitmapImage>>();
            var petTypes = ((PetType[])Enum.GetValues(typeof(PetType))).Where(x => x != PetType.None);
            foreach (var petType in petTypes)
            {
                var petTypeName = petType.ToString();
                var petTypeLowerName = petTypeName.ToLower();
                var list = new List<BitmapImage>();
                for (int i = 0; i < 5; i++)
                {
                    var uri = new Uri(string.Format(_imageFormat, petTypeName, petTypeLowerName, i));
                    var image = new BitmapImage(uri);
                    list.Add(image);
                }
                _images.Add(petType, list);
            }
        }

        public static BitmapImage SelectImage(PetType petType, int currentState)
        {
            if (petType == PetType.None) throw new Exception();
            if (currentState < 0 || currentState >= 5) throw new Exception();
            return _images[petType][currentState];
        }
    }
}