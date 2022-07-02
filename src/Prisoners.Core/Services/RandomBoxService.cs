using Prisoners.Core.Models;

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

        public IEnumerable<Box> GenerateBoxes(int numberOfBoxes)
        {
            if (numberOfBoxes <= 0)
            {
                return Enumerable.Empty<Box>();
            }
            
            GenerateSetOfPaperSlips(numberOfBoxes);
            var boxes = new List<Box>();
            for (int i = 1; i <= numberOfBoxes; i++)
            {
                boxes.Add(GetBox(i, _paperSlipsNumbers[i - 1]));
            }

            return boxes;

            // TOOD: implement a Iterator pattern to use yield, I will see

            //if (numberOfBoxes > 0)
            //{
            //    GenerateSetOfPaperSlips(numberOfBoxes);
            //    for (int i = 1; i <= numberOfBoxes; i++)
            //    {
            //        yield return GetBox(i, _numbers[i - 1]);
            //    }
            //}
            //else
            //{
            //    yield break;
            //}
        }

        public IEnumerable<Box> RefreshBoxes(IEnumerable<Box> boxes)
        {
            // TOOD: Implement this method
            throw new NotImplementedException();
        }

        private void GenerateSetOfPaperSlips(int numberOfBoxes)
            => _paperSlipsNumbers = Enumerable.Range(1, numberOfBoxes).OrderBy(i => _rnd.Next()).ToArray();

        private Box GetBox(int boxnumber, int paperSlipNumber)
            => new Box 
            {
                Number = boxnumber, 
                PaperSlip = GetPaperSlip(paperSlipNumber)
            };

        private PaperSlip GetPaperSlip(int paperSlipNumber)
            => new PaperSlip 
            {
                Number = paperSlipNumber
            };
    }
}
