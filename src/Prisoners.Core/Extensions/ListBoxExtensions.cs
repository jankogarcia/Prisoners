namespace Prisoners.Core.Extensions
{
    public static class ListBoxExtensions
    {
        // TOOD: Improve this method, could be faster
        public static int[] SwipeBoxesValue(this int[] list, int boxIndex1, int boxIndex2)
        {
            var temp = list[boxIndex1];
            list[boxIndex1] = list[boxIndex2];
            list[boxIndex2] = temp;
            return list;
        }
    }
}
