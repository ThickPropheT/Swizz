using System;

namespace Reg.Roup
{
    public interface Parse
    {
        public T With<T>(Func<string, T> parse) => default!;
    }
}
