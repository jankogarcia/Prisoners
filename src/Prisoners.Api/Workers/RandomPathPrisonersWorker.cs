using Prisoners.Core.Services;

namespace Prisoners.Api.Workers
{
    public class RandomPathPrisonersWorker : IPrisonersWorker
    {
        private readonly IPrisonerService _prisonerService;

        public RandomPathPrisonersWorker(IPrisonerService prisonerService)
        {
            _prisonerService = prisonerService;
        }

        public async Task<int[][]> GetPathsForPrisonersAsync(int iterations, int prisoners)
        {
            var result = new int[iterations][];
            Parallel.For(0, iterations, i =>
            {
                result[i] = GetResults(prisoners);
            });

            return result;
        }

        private int[] GetResults(int prisoners)
        {
            _prisonerService.SetNumberOfPrisoners(prisoners);
            _prisonerService.StartIteratingPrisoners();
            return _prisonerService.GetResults();
        }
    }
}
