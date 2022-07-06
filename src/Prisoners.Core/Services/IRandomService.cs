namespace Prisoners.Core.Services
{
    public interface IRandomService
    {
        int[] GenerateRandomList(int randomLimit);
        int Next();
        int NextWithinLimit(int randomLimit);
    }
}