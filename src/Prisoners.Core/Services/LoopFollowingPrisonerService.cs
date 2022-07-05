namespace Prisoners.Core.Services
{
    public class LoopFollowingPrisonerService : IPrisonerService
    {
        private IBoxService _boxService;

        private int _numberOfPrisoners;

        private int[] _pathResultsByPrisoner;

        private int[] _boxes;
        public LoopFollowingPrisonerService(IBoxService boxService)
        {
            _boxService = boxService;
        }

        public void SetNumberOfPrisoners(int numberOfPrisoners)
        {
            _numberOfPrisoners = numberOfPrisoners;
            _boxes = _boxService.GenerateBoxes(numberOfPrisoners).ToArray();
        }

        public void StartIteratingPrisoners()
        {
            _pathResultsByPrisoner = new int[_numberOfPrisoners];
            for (int i = 0; i < _numberOfPrisoners; i++)
            {
                _pathResultsByPrisoner[i] = CheckPrisonerPath(i);
            }
        }

        public int [] GetResults()
            => _pathResultsByPrisoner;

        private int CheckPrisonerPath(int prisoner)
        {
            var paperSlipNumber = GetBoxByIndex(prisoner);
            var count = 1;
            while (paperSlipNumber != prisoner + 1)
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
