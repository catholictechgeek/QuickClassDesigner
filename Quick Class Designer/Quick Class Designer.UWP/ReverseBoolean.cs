using System;
using Windows.UI.Xaml.Data;

namespace Quick_Class_Designer.UWP
{
    public class ReverseBoolean:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool val = (bool)value;
            return !val;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
