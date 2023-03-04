using Consolify.Base.Extensions;
using Consolify.Core.CommandLine;
using System.Diagnostics.CodeAnalysis;

namespace Consolify.Base.CommandLine
{
    public class PluginCommand<TCommandConfig> : Command
        where TCommandConfig : IMinimalCommandConfiguration
    {
        private readonly TCommandConfig _commandConfig;

        public TCommandConfig Configuration
        {
            get => _commandConfig;

            [MemberNotNull(nameof(_commandConfig))]
            init
            {
                _commandConfig = value;
                this.InitializeCommandFromConfiguration(_commandConfig);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <exception cref="ArgumentException"><see cref="TCommandConfig.Name"/> is empty or contains whitespace.</exception>
        public PluginCommand(TCommandConfig configuration) : base(configuration.Name, configuration.Description)
        {
            Configuration = configuration;
        }
    }
}
