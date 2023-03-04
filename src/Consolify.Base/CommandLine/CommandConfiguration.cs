using Consolify.Core.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.Diagnostics.CodeAnalysis;

namespace Consolify.Base.CommandLine
{
    public class CommandConfiguration : IMinimalCommandConfiguration
    {
        public required string Name { get; init; }
        public IReadOnlyCollection<string> Aliases { get; init; } = Array.Empty<string>();
        public IReadOnlyList<Argument> Arguments { get; init; } = Array.Empty<Argument>();
        public string Description { get; init; } = string.Empty;
        public IReadOnlyList<Option> GlobalOptions { get; init; } = Array.Empty<Option>();
        public ICommandHandler? Handler { get; init; } = null;
        public bool IsHidden { get; init; }
        public IReadOnlyList<Option> Options { get; init; } = Array.Empty<Option>();
        public IReadOnlyList<Command> Subcommands { get; init; } = Array.Empty<Command>();
        public bool TreatUnmatchedTokensAsErrors { get; init; }
        public IReadOnlyList<ValidateSymbolResult<SymbolResult>> Validators { get; init; } = Array.Empty<ValidateSymbolResult<SymbolResult>>();

        [SetsRequiredMembers]
        public CommandConfiguration(string name)
        {
            Name = name;
        }

        public CommandConfiguration() { }
    }
}
