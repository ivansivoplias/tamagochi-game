using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using Tamagochi.Common;

namespace Tamagochi.Helpers
{
    public class IconsSelector
    {
        private static IList<BitmapImage> _icons;
        private const string _uriFormat = "pack://application:,,,/Images/Icons/{0}.png";

        static IconsSelector()
        {
            _icons = new List<BitmapImage>();
            var actionTypes = (ActionType[])Enum.GetValues(typeof(ActionType));
            foreach (var item in actionTypes)
            {
                var lowerActionName = item.ToString().ToLower();
                var uri = new Uri(string.Format(_uriFormat, lowerActionName));
                var image = new BitmapImage(uri);
                _icons.Add(image);
            }
        }

        public static BitmapImage SelectIconForAction(ActionType action)
        {
            return _icons[(int)action];
        }
    }
}