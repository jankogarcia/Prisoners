namespace Prisoners.Core.Services
{
    public class RandomService : IRandomService
    {
        private Random _rnd;

        public RandomService()
        {
            _rnd = new Random();
        }

        public int NextWithinLimit(int randomLimit)
            => _rnd.Next(1, randomLimit);

        public int Next(int maxValue)
            => _rnd.Next(maxValue);

        public int[] GenerateRandomList(int randomLimit)
            => Enumerable.Range(1, randomLimit).OrderBy(x => _rnd.Next()).ToArray();
    }
}
