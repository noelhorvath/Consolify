using Consolify.Base.Dynamic.Extensions;
using Consolify.Core;
using Consolify.Core.Plugin;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;

namespace Consolify.Base.Dynamic
{
    public abstract class BasicDynamicApplication<TConsole, TDragAndDropHandler, TApplicationConfiguration, TPlugin> : BasicApplication<TConsole, TDragAndDropHandler, TApplicationConfiguration, TPlugin>, IDynamicModular<TPlugin>
        where TConsole : IConsole
        where TDragAndDropHandler : IDragAndDropHandler
        where TApplicationConfiguration : IApplicationConfiguration<TConsole, TDragAndDropHandler>
        where TPlugin : IPlugin
    {
        protected BasicDynamicApplication(Parser parser, TApplicationConfiguration configuration) : base(parser, configuration) { }
        protected BasicDynamicApplication(Command rootCommand, TApplicationConfiguration configuration) : this(new CommandLineBuilder(rootCommand).UseDefaults().Build(), configuration) { }
        protected BasicDynamicApplication(string rootCommandDescription, TApplicationConfiguration configuration) : this(new CommandLineBuilder(new RootCommand(rootCommandDescription)).UseDefaults().Build(), configuration) { }
        protected BasicDynamicApplication(TApplicationConfiguration configuration) : this(new CommandLineBuilder().UseDefaults().Build(), configuration) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblyFilePath"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="FileLoadException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="BadImageFormatException"></exception>
        /// <exception cref="PluginNotFoundException"></exception>
        public virtual bool LoadFromFile(string assemblyFilePath) => this.LoadPluginFromAssemblyFile(assemblyFilePath);
    }
}
