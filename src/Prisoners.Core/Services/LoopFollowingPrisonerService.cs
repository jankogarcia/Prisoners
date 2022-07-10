namespace Prisoners.Core.Services
{
    public class LoopFollowingPrisonerService : IPrisonerService
    {
        private readonly IBoxService _boxService;

        private int _numberOfPrisoners;

        private int[] _pathResultsByPrisoner;

        private int[] _boxes;
        
        private int _maximumAttemps;

        private const int DIVIDER = 2;

        public LoopFollowingPrisonerService(IBoxService boxService)
        {
            _boxService = boxService;
        }

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
                _pathResultsByPrisoner[i] = CheckPrisonerPath(i);
            }
            return this;
        }

        public int [] GetResults()
            => _pathResultsByPrisoner;

        private int CheckPrisonerPath(int prisoner)
        {
            var paperSlipNumber = GetBoxByIndex(prisoner);
            var count = 1;
            while (paperSlipNumber != prisoner + 1 && count <= _maximumAttemps)
            {
                paperSlipNumber = GetBoxByIndex(paperSlipNumber - 1);
                count ++;
            }

            return count;
        }

        private int GetBoxByIndex(int index) 
            => _boxes[index];
    }
}
