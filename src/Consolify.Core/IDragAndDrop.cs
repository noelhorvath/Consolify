namespace Consolify.Core
{
    public interface IDragAndDrop<TCommandConfiguration, TDragAndDropHandler>
        where TDragAndDropHandler : IDragAndDropHandler
    {
        TDragAndDropHandler Handler { get; }
    }
}
