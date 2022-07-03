using Prisoners.Core.Models;

namespace Prisoners.Core.Extensions
{
    public static class ListBoxExtensions
    {
        // TOOD: Improve this method, could be faster
        public static IEnumerable<Box> SwipeBoxPaperSlips(this IEnumerable<Box> boxes, int boxNumber1, int boxNumber2)
        {
            var box1 = boxes.Single(b => b.Number == boxNumber1);
            var box2 = boxes.Single(b => b.Number == boxNumber2);
            var temp = box1.PaperSlip.Number;
            box1.PaperSlip.Number = box2.PaperSlip.Number;
            box2.PaperSlip.Number = temp;

            return boxes;
        }
    }
}
