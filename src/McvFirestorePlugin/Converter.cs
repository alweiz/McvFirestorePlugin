using System;
using System.Globalization;
using System.Windows.Data;
namespace McvFirestorePlugin
{
    /// <summary>
    /// boolを反転させる
    /// </summary>
    public class NotConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Not((bool)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Not((bool)value);
        }
        private bool Not(bool b)
        {
            return !b;
        }
    }
}
