using Prisoners.Core.Services;

namespace Prisoners.Api.Workers
{
    public class PrisonersWorker : IPrisonersWorker
    {
        private readonly IPrisonerService _prisonerService;

        public PrisonersWorker(IPrisonerService prisonerService)
        {
            _prisonerService = prisonerService;
        }

        public async Task<int[][]> GetPathsForPrisonersAsync(int iterations, int prisoners)
        {
            // add a Task.Run() ???*
            var result = new int[iterations][];
            Parallel.For(0, iterations, i =>
            {
                result[i] = GetResults(prisoners);
            });

            return result;
        }

        private int[] GetResults(int prisoners)
            => _prisonerService
            .SetNumberOfPrisoners(prisoners)
            .StartIteratingPrisoners()
            .GetResults();
    }
}
