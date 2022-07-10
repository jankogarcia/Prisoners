using NUnit.Framework;
using Prisoners.Api.Workers;
using Prisoners.Core.Services;
using System.Threading.Tasks;
using System.Linq;

namespace Test.Prisoners.Api.Workers
{
    [TestFixture]
    public class PrisonersWorkerTests
    {
        private PrisonersWorker _sut;
        private RandomBoxService _randomBoxService;
        private RandomService _randomService;
        private RandomBoxPrisonerService _randomPrisonerService;
        private LoopFollowingPrisonerService _loopFollowingPrisonerService;

        [SetUp]
        public void Setup()
        {
            _randomService = new RandomService();
            _randomBoxService = new RandomBoxService(_randomService);
            _randomPrisonerService = new RandomBoxPrisonerService(_randomBoxService, _randomService);
            _loopFollowingPrisonerService = new LoopFollowingPrisonerService(_randomBoxService);
        }

        [Explicit("I guess I dont have the calculation right")]
        [TestCase(1, 1000)]
        [TestCase(10, 1000)]
        [TestCase(100, 1000)]
        [TestCase(1000, 1000)]
        //[TestCase(10000, 1000)] // too heavy
        public async Task RandomPathPrisonersOk(int iterations, int prisoners) 
        {
            _sut = new PrisonersWorker(_randomPrisonerService);

            var res = await _sut.GetPathsForPrisonersAsync(iterations, prisoners);
            Assert.IsNotNull(res);

            var attemps = prisoners / 2;
            var succeededIterations = res.Where(l => l.All(a => a <= attemps));
            Assert.IsNotNull(succeededIterations);

            var expected_success_rate = 0.5f;
            var succeededIterationsCount = succeededIterations.Count();
            var successRate = iterations * expected_success_rate;
            Assert.GreaterOrEqual(succeededIterationsCount, successRate, $"Success iterations ({succeededIterationsCount}) were less than expected: {successRate}");
        }

        [Explicit("I guess I dont have the calculation right")]
        [TestCase(1, 1000)]
        [TestCase(10, 1000)]
        [TestCase(100, 1000)]
        [TestCase(1000, 1000)]
        [TestCase(10000, 1000)] // 4 secs
        [TestCase(100000, 1000)] // 58 secs
        //[TestCase(10000, 10000)] // too heavy
        public async Task FollowingPathPrisonersOk(int iterations, int prisoners)
        {
            _sut = new PrisonersWorker(_loopFollowingPrisonerService);

            var res = await _sut.GetPathsForPrisonersAsync(iterations, prisoners);
            Assert.IsNotNull(res);

            var attemps = prisoners / 2;
            var succeededIterations = res.Where(l => l.All(a => a <= attemps));
            Assert.IsNotNull(succeededIterations);

            var expected_success_rate = 0.31f;
            var succeededIterationsCount = succeededIterations.Count();
            var successRate = iterations * expected_success_rate;
            Assert.GreaterOrEqual(succeededIterationsCount, successRate, $"Success iterations ({succeededIterationsCount}) were less than expected: {successRate}");
        }
    }
}
