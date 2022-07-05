namespace Prisoners.Core.Services
{
    public class RandomBoxService : IBoxService
    {
        private Random _rnd;
        private int[] _paperSlipsNumbers;
        public RandomBoxService()
        {
            _rnd = new Random();
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
            => _paperSlipsNumbers = Enumerable.Range(1, numberOfBoxes).OrderBy(i => _rnd.Next()).ToArray();
    }
}
