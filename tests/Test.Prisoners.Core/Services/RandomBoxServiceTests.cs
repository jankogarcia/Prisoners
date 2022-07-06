using NUnit.Framework;
using Prisoners.Core.Services;
using Prisoners.Core.Extensions;
using System;
using System.Linq;

namespace Test.Prisoners.Core.Services
{
    [TestFixture]
    public class RandomBoxServiceTests
    {
        private RandomBoxService _sut;
        private RandomService _randomService;

        [SetUp]
        public void Setup()
        {
            _randomService = new RandomService();
            _sut = new RandomBoxService(_randomService);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10)]
        [TestCase(100)]
        [TestCase(1000)]
        [TestCase(10000)]
        [TestCase(100000)]
        [TestCase(1000000)]
        public void GenerateBoxesOk(int numberOfBoxes)
        {
            var boxes = _sut.GenerateBoxes(numberOfBoxes);
            
            var count = boxes.Count();
            Assert.AreEqual(numberOfBoxes, count, "list of boxes is not same length as expected.");

            if (count > 0)
            {
                var randomNumber = _randomService.NextWithinLimit(numberOfBoxes);
                Assert.IsNotNull(Array.FindIndex(boxes, b => b == randomNumber), $"list of boxes does not contain random number: { randomNumber }.");

                var distinctPaperSlips = boxes.ToList().Distinct().Count();
                Assert.AreEqual(numberOfBoxes, distinctPaperSlips, $"Box is not correctly created, its missing a paper slip");
            }
        }

        [TestCase(10, 2, 8)]
        [TestCase(100, 28, 99)]
        [TestCase(1000, 248, 987)]
        [TestCase(10000, 1248, 9187)]
        [TestCase(100000, 12448, 91487)]
        [TestCase(1000000, 121748, 919887)]
        public void SwipePaperSlipsByBoxNumberOk(int numberOfBoxes, int boxNumber1, int boxNumber2)
        {
            var boxes = _sut.GenerateBoxes(numberOfBoxes);

            var box1paperSlip = boxes[boxNumber1];
            var box2paperSlip = boxes[boxNumber2];

            var swipedBoxes = boxes.SwipeBoxesValue(boxNumber1, boxNumber2);
            var paperSlip3 = swipedBoxes[boxNumber1];

            Assert.AreNotEqual(box1paperSlip, paperSlip3, "Swaping did not work.");
            var paperSlip4 = swipedBoxes[boxNumber2];

            Assert.AreNotEqual(box2paperSlip, paperSlip4, "Swaping did not work.");
        }

        [TestCase(2)]
        [TestCase(10)]
        [TestCase(100)]
        [TestCase(1000)]
        [TestCase(10000)]
        [TestCase(100000)]
        [TestCase(1000000)]
        [TestCase(10000000)]
        public void RefreshingBoxOk(int numberOfBoxes)
        {
            var boxes = _sut.GenerateBoxes(numberOfBoxes);
            Assert.IsNotNull(boxes, "boxes are not null");

            var randomBoxNumber = _randomService.NextWithinLimit(numberOfBoxes);
            var randomBoxPaperSlipNumber = Array.FindIndex(boxes, b => b == randomBoxNumber);

            //// refresh the boxes
            var refresedBoxes = _sut.RefreshBoxes(boxes);
            var randomBoxPaperSlipNumberRefreshed = Array.FindIndex(refresedBoxes, b => b == randomBoxNumber);

            Assert.AreNotEqual(randomBoxPaperSlipNumber, randomBoxPaperSlipNumberRefreshed, $"The box list wasnt refreshed, box with number: {randomBoxNumber} has the same paperslip number: {randomBoxPaperSlipNumber}");
        }
    }
}
