// LULE

namespace Consolify.Base
{
    /// <summary>
    /// Exit codes that a command or the application can return.
    /// </summary>
    public enum ExitCode : int
    {
        /// <summary>
        /// Internal 
        /// </summary>
        ExitTerminalMode = int.MinValue,
        /// <summary>
        /// A command successfully executed.
        /// </summary>
        Success = 0,
        /// <summary>
        /// An unhandled exception 
        /// </summary>
        UnexpectedErrorOccured = int.MaxValue,
    }
}
