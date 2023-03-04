namespace Consolify.Base.Dynamic.Extensions
{
    public static class MemoryExtensions
    {
        public static (T?, TItem2?) FirstOrDefault<T, TItem2>(this Span<T> span, Func<T, (bool, TItem2)> tupledPredicate, (T?, TItem2?) defaultValue)
        {
            int i = 0;

            while (i < span.Length)
            {
                var result = tupledPredicate(span[i]);
                if (result.Item1)
                {
                    return (span[i], result.Item2);
                }

                i++;
            }

            return defaultValue;
        }
    }
}
