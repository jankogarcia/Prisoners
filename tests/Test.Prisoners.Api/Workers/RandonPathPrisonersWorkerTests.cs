using NUnit.Framework;
using Prisoners.Api.Workers;
using Prisoners.Core.Services;
using System.Threading.Tasks;

namespace Test.Prisoners.Api.Workers
{
    [TestFixture]
    public class RandonPathPrisonersWorkerTests
    {
        private RandomPathPrisonersWorker _sut;
        
        [SetUp]
        public void Setup()
        {
            var randomService = new RandomService();
            _sut = new RandomPathPrisonersWorker(
                new RandomBoxPrisonerService(new RandomBoxService(randomService), randomService));
        }

        [TestCase(1, 1000)]
        [TestCase(10, 1000)]
        [TestCase(100, 1000)]
        [TestCase(1000, 1000)]
        //[TestCase(10000, 1000)]
        public async Task TestAsync(int iterations, int prisoners) 
        {
            var res = await _sut.GetPathsForPrisonersAsync(iterations, prisoners);
            Assert.IsNotNull(res);
        }
    }
}
