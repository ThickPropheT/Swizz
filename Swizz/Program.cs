namespace Swizz
{
    internal class Program
    {
        static async Task<int> Main(string[] args)
        {
            return await SwizzConsoleBootstrapper
                .Bootstrap(args)
                .Evaluate();
        }
    }
}