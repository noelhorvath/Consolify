using System.CommandLine.Parsing;

namespace Consolify.Core.CommandLine
{
    public interface ICommandConfiguration : IMinimalCommandConfiguration
    {
        IReadOnlyList<Argument> Arguments { get; }
        IReadOnlyList<Option> GlobalOptions { get; }
        IReadOnlyList<Option> Options { get; }
        IReadOnlyList<Command> Subcommands { get; }
        IReadOnlyList<ValidateSymbolResult<SymbolResult>> Validators { get; }
    }
}
