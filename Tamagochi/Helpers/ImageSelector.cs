﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;
using Tamagochi.Common;
using Tamagochi.Common.GameExceptions;

namespace Tamagochi.UI.Helpers
{
    public class ImageSelector
    {
        private static Dictionary<PetType, List<BitmapImage>> _images;
        private const string _imageFormat = "pack://application:,,,/Images/{0}/{1}-{2}.png";

        static ImageSelector()
        {
            _images = new Dictionary<PetType, List<BitmapImage>>();
            var petTypes = ((PetType[])Enum.GetValues(typeof(PetType))).Where(x => x != PetType.None).ToList();
            foreach (var petType in petTypes)
            {
                var petTypeName = petType.ToString();
                var petTypeLowerName = petTypeName.ToLower();
                var list = new List<BitmapImage>();
                for (int i = 0; i < 5; i++)
                {
                    var uri = new Uri(string.Format(_imageFormat, petTypeName, petTypeLowerName, i));
                    var image = new BitmapImage(uri);
                    image.Freeze();
                    list.Add(image);
                }
                _images.Add(petType, list);
            }
        }

        public static BitmapImage SelectImage(PetType petType, PetEvolutionLevel evolutionLevel)
        {
            int currentState = (int)evolutionLevel;

            if (petType == PetType.None)
                throw new SelectImageException($"Invalid pet type. Pet type is {petType}");
            return _images[petType][currentState];
        }
    }
}