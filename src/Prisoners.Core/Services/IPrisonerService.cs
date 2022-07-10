namespace Prisoners.Core.Services
{
    public interface IPrisonerService
    {
        int[] GetResults();
        IPrisonerService SetNumberOfPrisoners(int numberOfPrisoners);
        IPrisonerService StartIteratingPrisoners();
    }
}