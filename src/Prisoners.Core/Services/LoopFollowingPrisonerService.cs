using Prisoners.Core.Models;

namespace Prisoners.Core.Services
{
    public class LoopFollowingPrisonerService : IPrisonerService
    {
        private IBoxService _boxService;

        private int[] _numberOfPrisoners;

        private int[] _pathResultsByPrisoner;

        private Box[] _boxes;
        public LoopFollowingPrisonerService(IBoxService boxService)
        {
            _boxService = boxService;
        }

        public void SetNumberOfPrisoners(int numberOfPrisoners)
        {
            _numberOfPrisoners = new int[numberOfPrisoners];
            _boxes = _boxService.GenerateBoxes(numberOfPrisoners).ToArray();
        }

        public void StartIteratingPrisoners()
        {
            _pathResultsByPrisoner = new int[_numberOfPrisoners.Length];
            for (int i = 0; i < _numberOfPrisoners.Length; i++)
            {
                _pathResultsByPrisoner[i] = CheckPrisonerPath(i);
            }
        }

        public IEnumerable<int> GetResults()
            => _pathResultsByPrisoner;

        private int CheckPrisonerPath(int prisoner)
        {
            var paperSlipNumber = GetBoxByIndex(prisoner).PaperSlip;
            var counter = 1;
            while (paperSlipNumber != prisoner + 1)
            {
                paperSlipNumber = GetBoxByIndex(paperSlipNumber - 1).PaperSlip;
                counter++;
            }

            return counter;
        }

        private Box GetBoxByIndex(int index)
            => _boxes[index];
    }
}
