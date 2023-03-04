using LinkDotNet.StringBuilder;

namespace Consolify.Base.Extensions
{
    public static class MemoryExtensions
    {
        public static int Count<T>(this ReadOnlySpan<T> span, Func<T, bool> predicate)
        {
            int count = 0;
            int i = 0;

            while (i < span.Length)
            {
                if (predicate(span[i]))
                {
                    count++;
                }

                i++;
            }

            return count;
        }

        public static string[] SplitEnclosed(this ReadOnlySpan<char> span, char separator, ReadOnlySpan<char> enclosureCharacters, StringSplitOptions splitOptions = StringSplitOptions.None)
        {

            if (span.Length == 0 || (separator != ' ' && span.IsWhiteSpace()) || enclosureCharacters.Contains(separator))
            {
                return Array.Empty<string>();
            }

            int initialSize = span.Count(c => c == separator) + 1;
            List<string> arguments = new(initialSize);
            ValueStringBuilder stringBuilder = new();
            char? enclosureCharacter = null;

            for (int i = 0; i < span.Length; i++)
            {
                bool isEnclosureCharacter = enclosureCharacters.Contains(span[i]);

                if (isEnclosureCharacter)
                {
                    if (enclosureCharacter == span[i])
                    {
                        enclosureCharacter = null;
                        arguments.AddSplitEntry(stringBuilder.AsSpan(), splitOptions);
                        stringBuilder.Clear();
                        continue;
                    }

                    enclosureCharacter ??= span[i];
                    continue;
                }

                if (span[i] != separator || span[i] == separator && enclosureCharacter != null)
                {
                    stringBuilder.Append(span[i]);
                }

                if (span[i] == separator && enclosureCharacter == null)
                {
                    arguments.AddSplitEntry(stringBuilder.AsSpan(), splitOptions);
                    stringBuilder.Clear();
                }

                if (i == span.Length - 1)
                {
                    arguments.AddSplitEntry(stringBuilder.AsSpan(), splitOptions);
                    stringBuilder.Clear();
                }
            }

            return arguments.ToArray();
        }
    }
}
