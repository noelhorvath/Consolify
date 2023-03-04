using Consolify.Base.CommandLine;
using Consolify.Core.CommandLine;
using Consolify.Core.Plugin;
using System.CommandLine.Parsing;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Consolify.Base.Extensions
{
    public static class CommandExtensions
    {
        public static void AddAliases(this Command command, IReadOnlyCollection<string> aliases)
        {
            foreach (string alias in aliases)
            {
                if (!command.HasAlias(alias))
                {
                    command.AddAlias(alias);
                }
            }
        }

        public static void AddArguments(this Command command, IReadOnlyList<Argument> arguments)
        {
            for (int i = 0; i < arguments.Count; i++)
            {
                command.AddArgument(arguments[i]);
            }
        }

        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
        public static void AddCustomPropertiesFromConfiguration(this Command command, IMinimalCommandConfiguration configuration, CommandAddOptions addOptions = CommandAddOptions.All)
        {
            if (addOptions == CommandAddOptions.None)
            {
                return;
            }

            PropertyInfo[] properties = configuration.GetType().GetProperties();

            for (int i = 0; i < properties.Length; i++)
            {
                if (addOptions.HasFlag(CommandAddOptions.Arguments) &&
                    properties[i].PropertyType.IsAssignableTo(typeof(Argument)) &&
                    properties[i].CanRead &&
                    properties[i].GetValue(configuration) is Argument argument)
                {
                    command.AddArgument(argument);
                    continue;
                }

                if ((addOptions.HasFlag(CommandAddOptions.GlobalOptions) || addOptions.HasFlag(CommandAddOptions.Options)) &&
                    properties[i].PropertyType.IsAssignableTo(typeof(Option)) &&
                    properties[i].CanRead &&
                    properties[i].GetValue(configuration) is Option option)
                {
                    if (properties[i].GetCustomAttribute<GlobalAttribute>() != null)
                    {
                        command.AddGlobalOption(option);
                        continue;
                    }

                    command.AddOption(option);
                    continue;
                }

                if (addOptions.HasFlag(CommandAddOptions.Subcommands) &&
                    properties[i].PropertyType.IsAssignableTo(typeof(Command)) &&
                    properties[i].CanRead &&
                    properties[i].GetValue(configuration) is Command subcommand)
                {
                    command.AddCommand(subcommand);
                    continue;
                }

                if (addOptions.HasFlag(CommandAddOptions.Arguments) &&
                    properties[i].PropertyType.IsAssignableTo(typeof(Argument)) &&
                    properties[i].CanRead &&
                    properties[i].GetValue(configuration) is ValidateSymbolResult<SymbolResult> validator)
                {
                    command.AddValidator(validator);
                    continue;
                }
            }
        }

        public static void AddGlobalOptions(this Command command, IReadOnlyList<Option> options)
        {
            for (int i = 0; i < options.Count; i++)
            {
                command.AddGlobalOption(options[i]);
            }
        }

        public static void AddOptions(this Command command, IReadOnlyList<Option> options)
        {
            for (int i = 0; i < options.Count; i++)
            {
                command.AddOption(options[i]);
            }
        }

        public static void AddValidators(this Command command, IReadOnlyList<ValidateSymbolResult<SymbolResult>> validators)
        {
            for (int i = 0; i < validators.Count; i++)
            {
                command.AddValidator(validators[i]);
            }
        }

        public static bool HasSubcommandAlias(this Command command, string alias, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            foreach (Command subcommand in command.Subcommands)
            {
                foreach (string subcommandAlias in subcommand.Aliases)
                {
                    if (alias.Equals(subcommandAlias, comparison))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static void InitializeCommandFromConfiguration(this Command command, IMinimalCommandConfiguration configuration)
        {
            command.Description = configuration.Description;
            command.Handler = configuration.Handler;
            command.IsHidden = configuration.IsHidden;
            command.TreatUnmatchedTokensAsErrors = configuration.TreatUnmatchedTokensAsErrors;
            command.AddAliases(configuration.Aliases);
            command.AddCustomPropertiesFromConfiguration(configuration);
        }

        public static bool RemoveCommand(this Command command, string subcommandName, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            IList<Command>? commands = command.Subcommands as IList<Command>;

            if (commands == null)
            {
                return false;
            }

            for (int i = 0; i < commands.Count; i++)
            {
                if (commands[i].Name.Equals(subcommandName, comparison))
                {
                    return commands.Remove(commands[i]);
                }
            }

            return false;
        }

        public static bool TryAddCommand(this Command command, Command subcommand)
        {
            if (command.HasSubcommandAlias(subcommand.Name))
            {
                return false;
            }

            command.AddCommand(subcommand);
            return true;
        }

        public static bool TryAddCommand(this Command command, IPlugin plugin) => command.TryAddCommand(plugin.Command);
        public static bool[] TryAddCommands(this Command command, IReadOnlyList<Command> commands)
        {
            bool[] results = commands.Count == 0 ? Array.Empty<bool>() : new bool[commands.Count];

            for (int i = 0; i < commands.Count; i++)
            {
                results[i] = command.TryAddCommand(commands[i]);
            }

            return results;
        }

        public static bool[] TryAddCommands(this Command command, IReadOnlyList<IPlugin> plugins)
        {
            bool[] results = plugins.Count == 0 ? Array.Empty<bool>() : new bool[plugins.Count];

            for (int i = 0; i < plugins.Count; i++)
            {
                results[i] = command.TryAddCommand(plugins[i].Command);
            }

            return results;
        }

    }
}
