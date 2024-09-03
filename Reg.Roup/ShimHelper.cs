using System;
using System.Text.RegularExpressions;

namespace Reg.Roup
{
    public static class ShimHelper
    {
        public static object? InvokeShimForMappedGroup(Shim shim, GroupCollection groups)
        {
            var group = groups[shim.Target.Name];

            if (group.Success)
            {
                return shim.Invoke(group.Value);
            }

            return null;
        }
    }
}
