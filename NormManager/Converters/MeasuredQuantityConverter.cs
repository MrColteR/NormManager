using NormManager.Models;
using System.Globalization;
using System.Windows.Data;

namespace NormManager.Converters
{
    public class MeasuredQuantityConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            if (value is MeasuredQuantityType type)
            {
                switch (type)
                {
                    case MeasuredQuantityType.Formula:
                        return "Формула";
                    case MeasuredQuantityType.Fixedgradations:
                        return "Фиксированные градации";
                    case MeasuredQuantityType.Variablegradations:
                        return "Вычисляемые градации";
                    case MeasuredQuantityType.Normalinterval:
                        return "Интервал / Норма";
                    case MeasuredQuantityType.Fixedgradationsnorm:
                        return "Фикс. градации / Норма";
                    case MeasuredQuantityType.String:
                        return "Строка";
                }
            }

            return value;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string type)
            {
                switch (type)
                {
                    case "Формула":
                        return MeasuredQuantityType.Formula;
                    case "Фиксированные градации":
                        return MeasuredQuantityType.Fixedgradations;
                    case "Вычисляемые градации":
                        return MeasuredQuantityType.Variablegradations;
                    case "Интервал / Норма":
                        return MeasuredQuantityType.Normalinterval;
                    case "Фикс. градации / Норма":
                        return MeasuredQuantityType.Fixedgradationsnorm;
                    case "Строка":
                        return MeasuredQuantityType.String;
                }
            }

            return value;
        }
    }
}
