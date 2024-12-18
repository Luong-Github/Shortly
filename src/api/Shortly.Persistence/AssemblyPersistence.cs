using System.Reflection;
namespace Shortly.Persistence
{
    public static class AssemblyPersistence
    {
        public static readonly Assembly Assembly = typeof(AssemblyPersistence).Assembly;
    }
}
