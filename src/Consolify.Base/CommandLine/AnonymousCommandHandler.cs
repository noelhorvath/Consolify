using System.CommandLine.Invocation;

namespace Consolify.Base.CommandLine
{
    public class AnonymousCommandHandler : ICommandHandler
    {
        private readonly Func<InvocationContext, int> _invoke;
        private readonly Func<InvocationContext, Task<int>> _invokeAsync;

        public AnonymousCommandHandler(Func<InvocationContext, int> invoke, Func<InvocationContext, Task<int>> invokeAsync)
        {
            _invoke = invoke;
            _invokeAsync = invokeAsync;
        }

        public AnonymousCommandHandler(Func<InvocationContext, int> invoke)
        {
            _invoke = invoke;
            _invokeAsync = (ctx) => Task.FromResult(invoke(ctx));
        }

        public AnonymousCommandHandler(Func<InvocationContext, Task<int>> invokeAsync)
        {
            _invoke = (ctx) => invokeAsync(ctx).GetAwaiter().GetResult();
            _invokeAsync = invokeAsync;
        }

        public int Invoke(InvocationContext context) => _invoke(context);

        public Task<int> InvokeAsync(InvocationContext context) => _invokeAsync(context);
    }
}
