namespace Consolify.Base.Extensions
{
    public static class ConsoleExtensions
    {
        public static void Clear(this IConsole _)
        {
            Console.Clear();
        }

        public static void ClearLine(this IConsole _)
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, Console.CursorTop);
        }

        public static ConsoleColor GetBackgroundColor(this IConsole _) => Console.BackgroundColor;

        public static ConsoleColor GetForegroundColor(this IConsole _) => Console.ForegroundColor;

        public static string? ReadLine(this IConsole _)
        {
            return Console.ReadLine();
        }

        public static ConsoleKeyInfo ReadKey(this IConsole _, bool intercept) => Console.ReadKey(intercept);

        public static ConsoleKeyInfo ReadKey(this IConsole console) => console.ReadKey(intercept: false);

        public static void ResetColor(this IConsole _)
        {
            Console.ResetColor();
        }
        public static ConsoleColor SetBackgroundColor(this IConsole _, ConsoleColor color) => Console.BackgroundColor = color;

        public static ConsoleColor SetForegroundColor(this IConsole _, ConsoleColor color) => Console.ForegroundColor = color;

        public static void Write<T>(this IConsole _, T value)
        {
            Console.Write(value?.ToString());
        }

        public static void Write(this IConsole _, string format, params object?[] formatArguments)
        {
            Console.Write(format, formatArguments);
        }

        public static void WriteLine<T>(this IConsole _, T value)
        {
            Console.WriteLine(value?.ToString());
        }

        public static void WriteLine(this IConsole _, string format, params object?[] formatArguments)
        {
            Console.WriteLine(format, formatArguments);
        }
    }
}
