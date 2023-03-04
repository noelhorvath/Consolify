using Consolify.Core;
using System.CommandLine.IO;

namespace Consolify.Base
{
    public class ApplicationConfiguration : IApplicationConfiguration<IConsole, IDragAndDropHandler>
    {
        public IConsole Console { get; init; }
        public IReadOnlyCollection<char> EnclosureCharacters { get; init; }
        public IDragAndDropHandler? DragAndDropHandler { get; init; }

        public ApplicationConfiguration(IConsole? console, IReadOnlyCollection<char>? enclosureCharacters, IDragAndDropHandler? dragAndDropHandler = null)
        {
            Console = console ?? new SystemConsole();
            EnclosureCharacters = enclosureCharacters ?? new char[] { '\'', '\"' };
            DragAndDropHandler = dragAndDropHandler;
        }

        public ApplicationConfiguration() : this(null, null, null) { }
    }
}
