namespace Consolify.Core.CommandLine.Pagination
{
    public interface IKeyControllablePage
    {
        string KeyControlMessage { get; }
        string KeyControlSingularMessage { get; }
        IReadOnlySet<ConsoleKey> ExitKeys { get; }
        IReadOnlySet<ConsoleKey> ContinueKeys { get; }
    }
}
