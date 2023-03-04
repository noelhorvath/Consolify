namespace Consolify.Core
{
    public interface IApplication<TConsole, TDragAndDropHandler, TApplicationConfiguration>
        where TConsole : IConsole
        where TDragAndDropHandler : IDragAndDropHandler
        where TApplicationConfiguration : IApplicationConfiguration<TConsole, TDragAndDropHandler>
    {
        TApplicationConfiguration Configuration { get; }
        int Run(ReadOnlySpan<char> commandLine);
        Task<int> RunAsync(ReadOnlySpan<char> commandLine);
    }
}
