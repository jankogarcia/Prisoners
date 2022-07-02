using Prisoners.Core.Models;

namespace Prisoners.Core.Services
{
    public interface IBoxService
    {
        IEnumerable<Box> GenerateBoxes(int numberOfBoxes);
        IEnumerable<Box> RefreshBoxes(IEnumerable<Box> boxes);
    }
}