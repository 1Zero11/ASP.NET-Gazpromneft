using System;
using System.Linq;
using Xunit;

namespace XUnitTest1
{
    public class UnitTest1
    {

        [Fact]
        public void StringTest()
        {
            Assert.Equal("ThisIsAnExample", Test.ReverseWordsAndCamelCase("siht si na elpmaxe"));
        }

        [Fact]
        public void FirstTest()
        {
            int[] test1 = new int[] { 0, 0, 0, 0 };
            int[] test2 = new int[] { 1, 1, 1, 1 };
            int[] test3 = new int[] { 0, 1, 1, 0 };
            int[] test4 = new int[] { 0, 1, 0, 1 };

            Assert.Equal(0, Test.BinaryArrayToNumber(test1));
            Assert.Equal(15, Test.BinaryArrayToNumber(test2));
            Assert.Equal(6, Test.BinaryArrayToNumber(test3));
            Assert.Equal(5, Test.BinaryArrayToNumber(test4));
        }

		[Fact]
		public void TestSample1()
		{
			var sample = new[] { 15, 16, 18, 20, 1, 2, 5, 6, 7, 8, 11, 12 };
			var result = new Test.NumericShiftDetector().GetShiftPosition(sample);
			Assert.Equal(expected: 4, actual: result);
		}

		[Fact]
		public void TestSample2()
		{
			var sample = new[] { 5, 6, 7, 8, 11, 12, 15, 16, 18, 20, 1, 2 };
			var result = new Test.NumericShiftDetector().GetShiftPosition(sample);
			Assert.Equal(expected: 10, actual: result);
		}

		[Fact]
		public void TestSampleWithoutShift()
		{
			var sample = new[] { 1, 2, 5, 6, 7, 8, 11, 12, 15, 16, 18, 20 };
			var result = new Test.NumericShiftDetector().GetShiftPosition(sample);
			Assert.Equal(expected: 0, actual: result);
		}

		[Fact]
		public void TestLargeSample()
		{
			var part1 = Enumerable.Range(15_000, 100_000); // 15000, 15001, ... 114998, 114999
			var part2 = Enumerable.Range(0, 14_995); // 0, 1, ... 14993, 14994
			var sample = part1.Concat(part2).ToArray(); // объединение двух последовательностей // 15000, 15001, ... 114998, 114999, 0, 1, ... 14993, 14994

			var result = new Test.NumericShiftDetector().GetShiftPosition(sample);

			Assert.Equal(expected: 100_000, actual: result);
		}



	}
}
