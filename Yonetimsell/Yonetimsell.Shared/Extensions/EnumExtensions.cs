using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Yonetimsell.Shared.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum value)
        {
            string displayName = value.GetType()
                .GetMember(value.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()?
                .GetName();
            return displayName;
        }
    }
}
