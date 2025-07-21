using System.Globalization;

namespace News.Converters;

public class InverseBoolConverter : IValueConverter
{
    public static readonly InverseBoolConverter Default = new();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
            return !boolValue;
        return true;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
            return !boolValue;
        return false;
    }
}