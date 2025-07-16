using System.Globalization;

namespace News.Converters;

public class StringIsNotNullOrEmptyConverter : IValueConverter
{
    public static readonly StringIsNotNullOrEmptyConverter Default = new();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return !string.IsNullOrEmpty(value as string);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}