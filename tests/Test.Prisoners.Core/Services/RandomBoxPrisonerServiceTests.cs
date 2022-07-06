using NUnit.Framework;
using Prisoners.Core.Services;
using System.Linq;

namespace Test.Prisoners.Core.Services
{
    [TestFixture]
    public class RandomBoxPrisonerServiceTests
    {
        private RandomBoxPrisonerService _sut;
        private RandomService _randomService;

        private const float EXPECTECTED_SUCCESS_RATE = 0.5f;

        [SetUp]
        public void Setup() 
        {
            _randomService = new RandomService();
            _sut = new RandomBoxPrisonerService(new RandomBoxService(_randomService), _randomService);
        }

        [TestCase(10)]
        [TestCase(100)]
        [TestCase(1000)]
        [TestCase(10000)]
        [TestCase(100000)]
        [TestCase(1000000)]
        public void GeneratePrisonersOk(int prisonersCount)
        {
            Assert.DoesNotThrow(() => _sut.SetNumberOfPrisoners(prisonersCount));
        }

        [Ignore("This test are for checking if the success rate is the expected")]
        [TestCase(10)]
        [TestCase(100)]
        [TestCase(1000)]
        [TestCase(10000)]
        // too heavy
        //[TestCase(100000)]
        //[TestCase(1000000)]
        public void IteratePrisonersOk(int prisonersCount)
        {
            _sut.SetNumberOfPrisoners(prisonersCount);
            _sut.StartIteratingPrisoners();
            var results = _sut.GetResults();
            Assert.IsNotNull(results);

            var attempts = prisonersCount / 2;
            var succedeed = results.Count(value => value <= attempts);
            var successRate = prisonersCount * EXPECTECTED_SUCCESS_RATE;

            Assert.GreaterOrEqual(succedeed, successRate, $"Number of succedeed prisoners didnt satisficed the rule at: {succedeed} successed prisoners out of {prisonersCount}");
        }
    }
}
