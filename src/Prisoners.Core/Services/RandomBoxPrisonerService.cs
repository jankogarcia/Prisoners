namespace Prisoners.Core.Services
{
    public class RandomBoxPrisonerService : IPrisonerService
    {
        private readonly IBoxService _boxService;
        private readonly IRandomService _randomService;

        private int _numberOfPrisoners;

        private int[] _pathResultsByPrisoner;

        private int[] _boxes;

        private int _maximumAttemps;

        private const int DIVIDER = 2;

        public RandomBoxPrisonerService(IBoxService boxService, IRandomService randomService)
        {
            _boxService = boxService;
            _randomService = randomService;
        }

        public int[] GetResults()
            => _pathResultsByPrisoner;

        public void SetNumberOfPrisoners(int numberOfPrisoners)
        {
            _numberOfPrisoners = numberOfPrisoners;
            _maximumAttemps = _numberOfPrisoners / DIVIDER;
            _boxes = _boxService.GenerateBoxes(numberOfPrisoners);
        }

        public void StartIteratingPrisoners()
        {
            _pathResultsByPrisoner = new int[_numberOfPrisoners];
            for (int i = 0; i < _numberOfPrisoners; i++)
            {
                _pathResultsByPrisoner[i] = CheckPrisonerPath(i);
            }
        }

        private int CheckPrisonerPath(int prisoner)
        {
            var paperSlip = GetBoxByIndex(GetRandomIndex());
            var count = 1;
            while (paperSlip != prisoner + 1 && count <= _maximumAttemps)
            {
                paperSlip = GetBoxByIndex(GetRandomIndex());
                count++;
            }
            return count;
        }

        private int GetBoxByIndex(int index)
            => _boxes[index];

        // TOOD: to prevent this method to get repeated numbers within same iteration
        private int GetRandomIndex()
            => _randomService.NextWithinLimit(_numberOfPrisoners) - 1;
    }
}
