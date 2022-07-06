namespace Prisoners.Core.Services
{
    public class RandomBoxService : IBoxService
    {
        private int[] _paperSlipsNumbers;
        private readonly IRandomService _randomService;

        public RandomBoxService(IRandomService randomService)
        {
            _randomService = randomService;
        }

        public int [] GenerateBoxes(int numberOfBoxes)
        {
            if (numberOfBoxes <= 0)
            {
                return new int[0];
            }
            
            GenerateSetOfPaperSlips(numberOfBoxes);
            return _paperSlipsNumbers;
        }

        public int[] RefreshBoxes(int[] boxes)
        {
            var temp = boxes[0];

            for (int i = 0; i < boxes.Length; i++)
            {
                if (i == boxes.Length - 1)
                {
                    boxes[boxes.Length - 1] = temp;
                }
                else
                {
                    boxes[i] = boxes[i + 1];
                }
            }

            return boxes;
        }

        private void GenerateSetOfPaperSlips(int numberOfBoxes)
            => _paperSlipsNumbers = _randomService.GenerateRandomList(numberOfBoxes);
    }
}
