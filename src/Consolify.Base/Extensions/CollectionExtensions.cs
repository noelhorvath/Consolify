namespace Consolify.Base.Extensions
{
    public static class CollectionExtensions
    {
        public static int MaxLength(this IReadOnlyList<string> list)
        {
            int max = 0;
            int i = 0;

            while (i < list.Count)
            {
                if (max < list[i].Length)
                {
                    max = list[i].Length;
                }

                i++;
            }

            return max;
        }

        public static void AddSplitEntry(this IList<string> list, ReadOnlySpan<char> newEntry, StringSplitOptions splitOption)
        {
            switch (splitOption)
            {
                case StringSplitOptions.RemoveEmptyEntries:
                    if (newEntry.Length > 0)
                    {
                        list.Add(newEntry.ToString());
                    }
                    return;
                case StringSplitOptions.TrimEntries:
                    list.Add(newEntry.Trim().ToString());
                    return;
                case StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries:
                    ReadOnlySpan<char> trimmed = newEntry.Trim();
                    if (trimmed.Length > 0)
                    {
                        list.Add(trimmed.ToString());
                    }
                    return;
                default:
                    list.Add(newEntry.ToString());
                    return;
            }
        }
    }
}
