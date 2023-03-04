using Consolify.Base;
using Consolify.Base.Builder;
using Consolify.Base.Extensions;
using Consolify.Core;
using System.CommandLine;

namespace Consolify.Native.Builder
{
    public sealed class NativeApplicationBuilder : BasicApplicationBuilder<NativeApplication, IApplicationConfiguration<IConsole, IDragAndDropHandler>>
    {
        public NativeApplicationBuilder(Command? rootCommand) : base(rootCommand) { }
        public NativeApplicationBuilder(string rootCommandDescription) : this(new RootCommand(rootCommandDescription)) { }
        public NativeApplicationBuilder() : this(rootCommand: null) { }

        public override NativeApplication Build() =>
            this.BuildApplication(
                RootCommand,
                ParserBuilder,
                (parser, config) => new NativeApplication(parser, config),
                GetConfiguration ?? (() => new TerminalApplicationConfiguration() as ITerminalApplicationConfiguration<IConsole, IDragAndDropHandler>),
                RootCommandConfigurer,
                ParserBuilderConfigurer,
                PluginLoader);
    }
}
