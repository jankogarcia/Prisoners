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

        public IPrisonerService SetNumberOfPrisoners(int numberOfPrisoners)
        {
            _numberOfPrisoners = numberOfPrisoners;
            _maximumAttemps = _numberOfPrisoners / DIVIDER;
            _boxes = _boxService.GenerateBoxes(numberOfPrisoners);
            return this;
        }

        public IPrisonerService StartIteratingPrisoners()
        {
            _pathResultsByPrisoner = new int[_numberOfPrisoners];
            for (int i = 0; i < _numberOfPrisoners; i++)
            {
                _pathResultsByPrisoner[i] = CheckPrisonerPath(i, GetPrisonerRandomPath());
            }
            return this;
        }

        private int CheckPrisonerPath(int prisoner, int[] loop)
        {
            var count = 1;
            for (int i = 0; i < loop.Length; i++)
            {
                var paperSlip = GetBoxByIndex(loop[i] - 1);
                if (paperSlip != prisoner + 1 && count <= _maximumAttemps)
                {
                    count++;
                    continue;
                }
                else 
                {
                    // prisoner made it in range
                    return count;
                }
            }
            // prisoner didn't find paperslip.
            return count;
        }

        private int GetBoxByIndex(int index)
            => _boxes[index];

        private int[] GetPrisonerRandomPath()
        {
            var res = new List<int>();
            while(res.Count() < _maximumAttemps)
            {
                int val = _randomService.Next(_numberOfPrisoners) + 1;
                if (!res.Contains(val))
                {
                    res.Add(val);
                }
            }
            return res.ToArray();
        }
    }
}
