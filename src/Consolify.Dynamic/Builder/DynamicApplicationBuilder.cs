using Consolify.Base.Builder;
using Consolify.Base.Extensions;
using Consolify.Base;
using Consolify.Core;
using System.CommandLine;

namespace Consolify.Dynamic.Builder
{
    public sealed class DynamicApplicationBuilder : BasicApplicationBuilder<DynamicApplication, IApplicationConfiguration<IConsole, IDragAndDropHandler>>
    {
        public DynamicApplicationBuilder(Command? rootCommand) : base(rootCommand) { }
        public DynamicApplicationBuilder(string rootCommandDescription) : this(new RootCommand(rootCommandDescription)) { }
        public DynamicApplicationBuilder() : this(rootCommand: null) { }

        public override DynamicApplication Build() =>
            this.BuildApplication(
                RootCommand,
                ParserBuilder,
                (parser, config) => new DynamicApplication(parser, config),
                GetConfiguration ?? (() => new ApplicationConfiguration() as IApplicationConfiguration<IConsole, IDragAndDropHandler>),
                RootCommandConfigurer,
                ParserBuilderConfigurer,
                PluginLoader);
    }
}
