namespace Consolify.Core
{
    public interface IApplicationConfiguration<TConsole, TDragAndDropHandler>
        where TConsole : IConsole
        where TDragAndDropHandler : IDragAndDropHandler
    {
        TConsole Console { get; }
        TDragAndDropHandler? DragAndDropHandler { get; }
        IReadOnlyCollection<char> EnclosureCharacters { get; }
    }
}
