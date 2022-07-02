using NUnit.Framework;
using Prisoners.Core.Services;
using Prisoners.Core.Extensions;
using System;
using System.Linq;

namespace Test.Prisoners.Core.Services
{
    [TestFixture]
    public class BoxServiceTests
    {
        private RandomBoxService _sut;
        private Random _rnd;

        [SetUp]
        public void Setup()
        {
            _sut = new RandomBoxService();
            _rnd = new Random();
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10)]
        [TestCase(100)]
        [TestCase(1000)]
        [TestCase(10000)]
        [TestCase(100000)]
        [TestCase(1000000)]
        public void GenerateBoxes(int numberOfBoxes)
        {
            var boxes = _sut.GenerateBoxes(numberOfBoxes);
            var count = boxes.Count();
            Assert.AreEqual(numberOfBoxes, count, "list of boxes is not same length as expected.");

            if (count > 0)
            {
                var randomNumber = _rnd.Next(1, numberOfBoxes);
                Assert.IsNotNull(boxes.First(box => box.PaperSlip.Number == randomNumber), $"list of boxes does not contain random number: { randomNumber }.");

                var distinctPaperSlips = boxes.Select(box => box.PaperSlip.Number).ToList().Distinct().Count();
                Assert.AreEqual(numberOfBoxes, distinctPaperSlips, $"Box is not correctly created, its missing a paper slip");
            }
        }

        [TestCase(10, 2, 8)]
        [TestCase(100, 28, 99)]
        [TestCase(1000, 248, 987)]
        [TestCase(10000, 1248, 9187)]
        public void SwipePaperSlipsByBoxNumber(int numberOfBoxes, int boxNumber1, int boxNumber2)
        {
            var boxes = _sut.GenerateBoxes(numberOfBoxes);

            var box1 = boxes.First(box => box.Number == boxNumber1);
            var paperSlip1 = box1.PaperSlip.Number;

            var box2 = boxes.First(box => box.Number == boxNumber2);
            var paperSlip2 = box2.PaperSlip.Number;

            var swipedBoxes = boxes.SwipeBoxPaperSlips(boxNumber1, boxNumber2);

            box1 = boxes.First(box => box.Number == boxNumber1);
            var paperSlip3 = box1.PaperSlip.Number;
            Assert.AreNotEqual(paperSlip1, paperSlip3, "Swaping did not work.");

            box2 = boxes.First(box => box.Number == boxNumber2);
            var paperSlip4 = box2.PaperSlip.Number;

            Assert.AreNotEqual(paperSlip2, paperSlip4, "Swaping did not work.");
        }

        //TOOD: I need a method to refresh the list of boxes and add some test

        //[TestCase(2)]
        //[TestCase(10)]
        //[TestCase(100)]
        //[TestCase(1000)]
        //[TestCase(10000)]
        //[TestCase(100000)]
        //[TestCase(1000000)]
        //public void RefreshingBoxOk(int numberOfBoxes)
        //{
        //    //var boxes = _sut.GenerateBoxes(numberOfBoxes);
        //    //Assert.IsNotNull(boxes, "boxes are not null");

        //    //var randomBoxNumber = _rnd.Next(1, numberOfBoxes);
        //    //var randomBoxPaperSlipNumber = boxes.First(box => box.Number == randomBoxNumber).PaperSlip.Number;

        //    //// refresh the boxes
        //    //var refresedBoxes = _sut.RefreshBoxes(boxes);
        //    //var randomBoxPaperSlipNumberRefreshed = refresedBoxes.First(box => box.Number == randomBoxNumber).PaperSlip.Number;

        //    //Assert.AreNotEqual(randomBoxPaperSlipNumber, randomBoxPaperSlipNumberRefreshed, $"The box list wasnt refreshed, box with number: {randomBoxNumber} has the same paperslip number: {randomBoxPaperSlipNumber}");
        //}
    }
}
