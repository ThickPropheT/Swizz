using System;
using System.Data;

namespace Reg.Roup
{
    public class Target
    {
        public string Name { get; }
        public Type Type { get; }

        public Target(string name, Type type)
        {
            Name = name;
            Type = type;
        }
    }

    public partial class Shim
    {
        public Target Target { get; }

        public Func<object?, object?> Invoke { get; }

        public Shim(Target target, Func<object?, object?> invoke)
        {
            Target = target;
            Invoke = invoke;
        }
    }
}
