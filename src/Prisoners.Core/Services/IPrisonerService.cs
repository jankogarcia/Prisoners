
namespace Prisoners.Core.Services
{
    public interface IPrisonerService
    {
        int[] GetResults();
        void SetNumberOfPrisoners(int numberOfPrisoners);
        void StartIteratingPrisoners();
    }
}