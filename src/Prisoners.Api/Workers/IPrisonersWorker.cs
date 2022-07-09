
namespace Prisoners.Api.Workers
{
    public interface IPrisonersWorker
    {
        Task<int[][]> GetPathsForPrisonersAsync(int iterations, int prisoners);
    }
}