using System;

namespace Reg.Roup
{
    internal static class TypeExtensions
    {
        public static bool CanBeAssignedNull(this Type type)
            => !type.IsValueType || Nullable.GetUnderlyingType(type) != null;
    }
}