
namespace Prisoners.Core.Services
{
    public interface IPrisonerService
    {
        IEnumerable<int> GetResults();
        void SetNumberOfPrisoners(int numberOfPrisoners);
        void StartIteratingPrisoners();
    }
}