using Consolify.Core;

namespace Consolify.Base.Extensions
{
    public static class ApplicationExtensions
    {
        public static string[] GetArguments<TConsole, TDragAndDropHandler, TApplicationConfiguration>
            (this IApplication<TConsole, TDragAndDropHandler, TApplicationConfiguration> application, ReadOnlySpan<char> commandLine)
                where TConsole : IConsole
                where TDragAndDropHandler : IDragAndDropHandler
                where TApplicationConfiguration : IApplicationConfiguration<TConsole, TDragAndDropHandler>
        {
            char[] chars = application.Configuration.EnclosureCharacters is char[] charArray ? charArray : application.Configuration.EnclosureCharacters.ToArray();
            return commandLine.SplitEnclosed(' ', chars, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        }
    }
}
