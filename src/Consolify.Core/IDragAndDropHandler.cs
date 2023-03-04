using System.CommandLine.Parsing;

namespace Consolify.Core
{
    public interface IDragAndDropHandler
    {
        int Handle(Parser commandLineParser, string arguments);
        Task<int> HandleAsync(Parser commandLineParser, string argument);
        bool CanHandle(Parser commandLineParser, string[] argument);
    }
}
