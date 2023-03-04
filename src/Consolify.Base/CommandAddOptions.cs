namespace Consolify.Base
{
    [Flags]
    public enum CommandAddOptions
    {
        None = 0,
        Arguments = 1,
        GlobalOptions = 1 << 1,
        Options = 1 << 2,
        Subcommands = 1 << 3,
        Validators = 1 << 4,
        All = ~(-1 << 5),
    }
}
