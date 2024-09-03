using System;

namespace Reg.Roup
{
    public partial class Shim
    {
        public static Shim ForNoTypeConversion(Target target)
            // TODO
            //=> new(name, targetType, v => v);
            => new Shim(target, v => v);
    }
}