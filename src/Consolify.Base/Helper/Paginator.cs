using Consolify.Base.Extensions;

namespace Consolify.Base.Helper
{
    public static class Paginator
    {
        public static void WriteChunks<T>(int chunkSize, int totalLength, IEnumerable<T[]> chunks, IConsole console, string continuationMessage, Func<T, string> formatter, bool isInteractive = false, bool dispose = true)
        {
            int i = 0;

            foreach (T[] chunk in chunks)
            {
                for (int j = 0; j < chunk.Length; j++)
                {
                    console.WriteLine(formatter(chunk[j]));

                    if (dispose && chunk[j] is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                }

                if (isInteractive)
                {
                    bool canQuit = chunkSize > chunk.Length || CanQuitePagination(console, continuationMessage);

                    if (totalLength - 1 != i && canQuit)
                    {
                        break;
                    }
                }

                i++;
            }
        }

        public static bool CanQuitePagination(IConsole console, string continuationMessage)
        {
            console.WriteLine(continuationMessage);
            bool waitingForContinuation = true;

            while (waitingForContinuation)
            {
                ConsoleKeyInfo keyInfo = console.ReadKey(intercept: true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.Q:
                        return true;
                    case ConsoleKey.S or ConsoleKey.DownArrow:
                        waitingForContinuation = false;
                        break;
                    default:
                        break;
                }
            }

            console.ClearLine(); // remove message
            return false;
        }
    }
}
