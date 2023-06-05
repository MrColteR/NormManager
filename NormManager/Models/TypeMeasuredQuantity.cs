using System.ComponentModel;

namespace NormManager.Models
{
    public enum MeasuredQuantityType
    {
        [Description("formula")]
        Formula,
        [Description("fixedgradations")]
        Fixedgradations,
        [Description("variablegradations")]
        Variablegradations, // не работает 
        [Description("normalinterval")]
        Normalinterval,
        [Description("fixedgradationsnorm")]
        Fixedgradationsnorm, // не работает 
        [Description("string")]
        String
    }
}
