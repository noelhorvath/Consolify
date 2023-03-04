using Consolify.Base.Extensions;
using Consolify.Base.Resources;
using Consolify.Core;
using Consolify.Core.Plugin;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;

namespace Consolify.Base
{
    public abstract class BasicTerminalApplication<TConsole, TDragAndDropHandler, TApplicationConfiguration, TPlugin> : BasicApplication<TConsole, TDragAndDropHandler, TApplicationConfiguration, TPlugin>, ITerminal
        where TConsole : IConsole
        where TDragAndDropHandler : IDragAndDropHandler
        where TApplicationConfiguration : ITerminalApplicationConfiguration<TConsole, TDragAndDropHandler>
        where TPlugin : IPlugin
    {
        protected BasicTerminalApplication(Parser parser, TApplicationConfiguration configuration) : base(parser, configuration) { }
        protected BasicTerminalApplication(Command rootCommand, TApplicationConfiguration configuration) : this(new CommandLineBuilder(rootCommand).UseDefaults().Build(), configuration) { }
        protected BasicTerminalApplication(string rootCommandDescription, TApplicationConfiguration configuration) : this(new CommandLineBuilder(new RootCommand(rootCommandDescription)).UseDefaults().Build(), configuration) { }
        protected BasicTerminalApplication(TApplicationConfiguration configuration) : this(new CommandLineBuilder().UseDefaults().Build(), configuration) { }

        public virtual bool IsTerminalMode(string[] arguments) => arguments.Length == 0;

        public override int Run(ReadOnlySpan<char> commandLine)
        {
            string[] arguments = this.GetArguments(commandLine);

            try
            {
                if (IsTerminalMode(arguments))
                {
                    return RunTerminalMode();
                }

                if (Configuration.DragAndDropHandler != null && Configuration.DragAndDropHandler.CanHandle(Parser, arguments))
                {
                    return Configuration.DragAndDropHandler.Handle(Parser, arguments[0]);
                }

                return Parser.Invoke(arguments, Configuration.Console);
            }
            catch (Exception)
            {
                Console.WriteLine(Strings.UnexpectedErrorOccured);
                return (int)ExitCode.UnexpectedErrorOccured;
            }
        }

        public override Task<int> RunAsync(ReadOnlySpan<char> commandLine)
        {
            string[] arguments = this.GetArguments(commandLine);

            try
            {
                if (IsTerminalMode(arguments))
                {
                    return RunTerminalModeAsync();
                }

                if (Configuration.DragAndDropHandler != null && Configuration.DragAndDropHandler.CanHandle(Parser, arguments))
                {
                    return Configuration.DragAndDropHandler.HandleAsync(Parser, arguments[0]);
                }

                return Parser.InvokeAsync(arguments, Configuration.Console);
            }
            catch (Exception)
            {
                Console.WriteLine(Strings.UnexpectedErrorOccured);
                return Task.FromResult((int)ExitCode.UnexpectedErrorOccured);
            }
        }

        protected virtual int RunTerminalMode()
        {
            int exitCode = (int)ExitCode.Success;

            if (Configuration.StartMessage.Length != 0)
            {
                Console.WriteLine(Configuration.StartMessage);
            }

            int commandResult = exitCode;

            while (true)
            {
                if (Configuration.InputLineStart.Length != 0)
                {
                    Console.Write(Configuration.InputLineStart);
                }

                string[] arguments = this.GetArguments(Console.ReadLine().AsSpan());

                if (arguments.Length != 0)
                {
                    commandResult = Parser.Invoke(arguments, Configuration.Console);

                    if (commandResult == (int)ExitCode.ExitTerminalMode)
                    {
                        return exitCode;
                    }

                    exitCode = commandResult;
                }
            }
        }

        protected virtual async Task<int> RunTerminalModeAsync()
        {
            int exitCode = (int)ExitCode.Success;

            if (Configuration.StartMessage.Length != 0)
            {
                Console.WriteLine(Configuration.StartMessage);
            }

            int commandResult = exitCode;

            while (true)
            {
                if (Configuration.InputLineStart.Length != 0)
                {
                    Console.Write(Configuration.InputLineStart);
                }

                string[] arguments = this.GetArguments(Console.ReadLine().AsSpan());

                if (arguments.Length != 0)
                {
                    commandResult = await Parser.InvokeAsync(arguments, Configuration.Console).ConfigureAwait(false);

                    if (commandResult == (int)ExitCode.ExitTerminalMode)
                    {
                        return exitCode;
                    }

                    exitCode = commandResult;
                }
            }
        }
    }
}
