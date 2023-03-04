using System.CommandLine.Invocation;

namespace Consolify.Core.CommandLine
{
    public interface IMinimalCommandConfiguration
    {
        IReadOnlyCollection<string> Aliases { get; }
        string Description { get; }
        ICommandHandler? Handler { get; }
        bool IsHidden { get; }
        string Name { get; }
        bool TreatUnmatchedTokensAsErrors { get; }
    }
}
