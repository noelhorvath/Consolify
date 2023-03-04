namespace Consolify.Core.Builder
{
    public interface IBuilder<T>
    {
        public T Build();
    }
}