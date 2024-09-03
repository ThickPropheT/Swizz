using System;

namespace Reg.Roup
{
    public partial class Shim
    {
        public static Shim ForDefaultConstructor(Type type)
            // TODO
            //=> new(type.Name, type, _ => Activator.CreateInstance(type));
            => new Shim(new Target(type.Name, type), _ => Activator.CreateInstance(type));
    }
}