using Common_PMG.Container.Game;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Data;

namespace Pack_My_Game.IHM.Converters
{
    class WidthHeightToText : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var link = (string)value;
            if (link ==null)
                return null;

            var width = 0;
            var height = 0;

            using (var fileStream = new FileStream(link, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var image = Image.FromStream(fileStream, false, false))
                {
                    height = image.Height;
                    width = image.Width;
                }
            }

            return $"{width}x{height}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
