using System.ComponentModel;

namespace Reg.Roup
{
    public partial class Shim
    {
        public static Shim ForDefaultTypeConversion(Target target)
            // TODO
            //=> new(name, targetType, v => TypeDescriptor.GetConverter(targetType).ConvertTo(v, targetType));
            => new Shim(target, v => TypeDescriptor.GetConverter(target.Type).ConvertTo(v, target.Type));
    }
}