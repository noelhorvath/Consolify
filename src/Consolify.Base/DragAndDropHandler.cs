using Consolify.Base.Extensions;
using Consolify.Core;
using System.CommandLine.Parsing;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace Consolify.Base
{
    public class DragAndDropHandler : IDragAndDropHandler
    {
        public required CompositeFormat CommandLineFormat { get; set; }

        public string ExecutablePath => Environment.GetCommandLineArgs()[0];

        [SetsRequiredMembers]
        public DragAndDropHandler(CompositeFormat commandLineFormat)
        {
            CommandLineFormat = commandLineFormat;
        }

        public DragAndDropHandler() { }

        public bool CanHandle(Parser commandLineParser, string[] arguments)
        {
            if (0 < arguments.Length && arguments.Length < 3)
            {
                if (arguments.Length == 1)
                {
                    return !arguments[0].Equals(ExecutablePath, StringComparison.OrdinalIgnoreCase) &&
                        !commandLineParser.Configuration.RootCommand.HasSubcommandAlias(arguments[0]) && Path.Exists(arguments[0]);
                }

                return arguments[0].Equals(ExecutablePath, StringComparison.OrdinalIgnoreCase) &&
                    !commandLineParser.Configuration.RootCommand.HasSubcommandAlias(arguments[1]) && Path.Exists(arguments[1]);
            }

            return false;
        }

        public int Handle(Parser commandLineParser, string argument) => commandLineParser.Invoke(string.Format(CultureInfo.InvariantCulture, CommandLineFormat, argument));

        public Task<int> HandleAsync(Parser commandLineParser, string argument) => commandLineParser.InvokeAsync(string.Format(CultureInfo.InvariantCulture, CommandLineFormat, argument));
    }
}
