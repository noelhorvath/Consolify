namespace Consolify.Core.CommandLine.Pagination
{
    public interface IPaginated
    {
        int DefaultChunkSize { get; }
        Option<int> ChunkSizeOption { get; }
    }
}
