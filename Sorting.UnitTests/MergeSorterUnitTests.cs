using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace Sorting.UnitTests
{
    public class MergeSorterUnitTests
    {
        private readonly MergeSorter sorter = new ();

        [Fact]
        public void GivenArrayWithTooManyElementsWhenSortedThenArgumentExceptionThrownWithTooManyElementsMessage()
        {
            // Arrange
            var data = Enumerable.Repeat(0, 5001).ToArray();
            var act = () => this.sorter.Sort(data);


            // Act && Assert
            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("Array length less than or equal to 5000 (Parameter 'values')");
        }

        [Fact]
        public void GivenEmptyArrayWhenSortedThenEmptyArrayReturned()
        {
            // Arrange
            var data = Array.Empty<int>();
            var expected = Array.Empty<int>();

            // Act
            var actual = this.sorter.Sort(data);

            // Assert
            actual.Should().ContainInOrder(expected);
        }

        [Theory]
        [InlineData(new int[] { 1 }, new int[] { 1 })]
        [InlineData(new int[] { 1, 2 }, new int[] { 1, 2 })]
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 })]
        [InlineData(new int[] { 1, 2, 3, 4 }, new int[] { 1, 2, 3, 4 })]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, new int[] { 1, 2, 3, 4, 5 })]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6 }, new int[] { 1, 2, 3, 4, 5, 6 })]
        public void GivenOrderedElementsArrayWhenSortedWithDifferentRecursionLevelsThenSortedArrayReturned(int[] data, int[] expected)
        {
            // Arrange
            // Act
            var actual = this.sorter.Sort(data);

            // Assert
            actual.Should().ContainInOrder(expected);
        }

        [Theory]
        [InlineData(new int[] { 1, 1 }, new int[] { 1, 1 })]
        [InlineData(new int[] { 2, 2, 1 }, new int[] { 1, 2, 2 })]
        [InlineData(new int[] { 2, 1, 1 }, new int[] { 1, 1, 2 })]
        [InlineData(new int[] { 2, 1, 1, 2, 2, 1 }, new int[] { 1, 1, 1, 2, 2, 2 })]
        public void GivenElementsArrayWithSameValuesWhenSortedWithDifferentRecursionLevelsThenSortedArrayReturned(int[] data, int[] expected)
        {
            // Arrange
            // Act
            var actual = this.sorter.Sort(data);

            // Assert
            actual.Should().ContainInOrder(expected);
        }

        [Theory]
        [InlineData(new int[] { 3, 1, 2 }, new int[] { 1, 2, 3 })]
        [InlineData(new int[] { 4, 1, 2, 5, 3 }, new int[] { 1, 2, 3, 4, 5 })]
        public void GivenArrayWithOddNumberElementsWhenSortedWitMultipleLevelThenSortedArrayReturned(int[] data, int[] expected)
        {
            // Arrange
            // Act
            var actual = this.sorter.Sort(data);

            // Assert
            actual.Should().ContainInOrder(expected);
        }

        [Theory]
        [InlineData(new int[] { -1, -2 }, new int[] { -2, -1 })]
        [InlineData(new int[] { 2, 1 }, new int[] { 1, 2})]
        [InlineData(new int[] { -4, -1, -3, -2 }, new int[] { -4, -3, -2, -1 })]
        [InlineData(new int[] { 4, 1, 3, 2 }, new int[] { 1, 2, 3, 4 })]
        [InlineData(new int[] { -6, -5, -4, -1, -3, -2 }, new int[] { -6, -5, -4, -3, -2, -1 })]
        [InlineData(new int[] { 6, 5, 4, 1, 3, 2 }, new int[] { 1, 2, 3, 4, 5, 6 })]
        public void GivenArrayWithEvenNumberElementsWhenSortedWitMultipleLevelThenSortedArrayReturned(int[] data, int[] expected)
        {
            // Arrange
            // Act
            var actual = this.sorter.Sort(data);

            // Assert
            actual.Should().ContainInOrder(expected);
        }

        [Theory]
        [InlineData(new int[] { 3, 1, 2 }, new int[] { 1, 2, 3 })]
        [InlineData(new int[] { -3, -1, -2 }, new int[] { -3, -2, -1})]
        [InlineData(new int[] { 4, 1, 2 }, new int[] { 1, 2, 4 })]
        [InlineData(new int[] { 6, 5, 3, 1, 8, 7, 2, 4 }, new int[] { 1, 2, 3, 4, 5, 6, 7, 8 })]
        public void GivenArrayWithElementsWhenSortedThenSortedArrayReturned(int[] data, int[] expected)
        {
            // Arrange
            // Act
            var actual = this.sorter.Sort(data);

            // Assert
            actual.Should().ContainInOrder(expected);
        }

        [Theory]
        [InlineData(new int[] { 3, 1 }, new int[] { 1, 3 })]
        public void GivenArrayWithElementsWithHigherValuesOnLeftSideWhenSortedThenSortedArrayReturned(int[] data, int[] expected)
        {
            // Arrange
            // Act
            var actual = this.sorter.Sort(data);

            // Assert
            actual.Should().ContainInOrder(expected);
        }

        [Theory]
        [InlineData(new int[] { 1, 5, 7 }, new int[] { 1, 5, 7})]
        public void GivenArrayWithElementsWithHigherValuesOnRightSideWhenSortedThenSortedArrayReturned(int[] data, int[] expected)
        {
            // Arrange
            // Act
            var actual = this.sorter.Sort(data);

            // Assert
            actual.Should().ContainInOrder(expected);
        }

        [Theory]
        [InlineData(new int[] { int.MaxValue, int.MaxValue - 1, int.MaxValue - 2 }, new int[] { int.MaxValue -2, int.MaxValue - 1, int.MaxValue })]
        [InlineData(new int[] { int.MinValue + 2, int.MinValue + 1, int.MinValue }, new int[] { int.MinValue, int.MinValue + 1, int.MinValue + 2 })]
        [InlineData(new int[] { int.MaxValue, 0, int.MinValue }, new int[] { int.MinValue, 0, int.MaxValue })]
        public void GivenArrayWithHighElementValuesWhenSortedThenSortedArrayReturned(int[] data, int[] expected)
        {
            // Arrange
            // Act
            var actual = this.sorter.Sort(data);

            // Assert
            actual.Should().ContainInOrder(expected);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(5000)]
        public void GivenArrayWithManyElementsWhenSortedThenSortedArrayReturned(int count)
        {
            var random = new Random();

            var data = Enumerable.Range(0, count).Select(s => random.Next()).ToArray();
            var expected = data.OrderBy(s => s).ToArray();

            // Arrange
            // Act
            var actual = this.sorter.Sort(data);

            // Assert
            actual.Should().ContainInOrder(expected);
        }
    }
}