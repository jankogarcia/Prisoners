namespace Prisoners.Core.Services
{
    public interface IBoxService
    {
        int[] GenerateBoxes(int numberOfBoxes);
        int[] RefreshBoxes(int[] boxes);
    }
}