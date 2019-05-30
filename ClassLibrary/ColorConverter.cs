using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ClassLibrary
{
    class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case "SCHEDULED": return Brushes.White;
                case "BOARDING": return Brushes.Green;
                case "LASTCALL": return Brushes.Orange;
                case "GATE CLOSED": return Brushes.Red;
                case "AIRBORNE": return Brushes.Purple;
                case "FAR AWAY":
                default: return Brushes.Black;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}


