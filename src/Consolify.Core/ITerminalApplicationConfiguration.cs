namespace Consolify.Core
{
    public interface ITerminalApplicationConfiguration<TConsole, TDragAndDropHandler> : IApplicationConfiguration<TConsole, TDragAndDropHandler>
        where TConsole : IConsole
        where TDragAndDropHandler : IDragAndDropHandler
    {
        string StartMessage { get; }
        string InputLineStart { get; }
    }
}
