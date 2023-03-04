using Consolify.Core;
using System.CommandLine.IO;

namespace Consolify.Base
{
    public class TerminalApplicationConfiguration : ITerminalApplicationConfiguration<IConsole, IDragAndDropHandler>
    {
        public string InputLineStart { get; init; } = string.Empty;
        public string StartMessage { get; init; } = string.Empty;
        public IConsole Console { get; init; } = new SystemConsole();
        public IDragAndDropHandler? DragAndDropHandler { get; init; }
        public IReadOnlyCollection<char> EnclosureCharacters { get; init; } = new char[] { '\'', '\"' };
    }
}
