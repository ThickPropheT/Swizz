using System;

namespace Reg.Roup
{
    public interface IParse
    {
        public T With<T>(Func<string, T> parse) => default!;
    }
}
