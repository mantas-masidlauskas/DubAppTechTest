namespace Sorting
{
    /// <summary>
    /// Von Neumann’s merge sort algorithm.
    /// </summary>
    public class MergeSorter : ISorter
    {
        public int[] Sort(int[] values)
        {
            if(values.Length > 5000)
                throw new ArgumentException("Array length less than or equal to 5000", nameof(values));

            MergeSort(values);

            return values;
        }


        private void MergeSort(int[] values)
        {
            var length = values.Length;

            if (length < 2) 
                return; 

            var middle = length / 2;
            var leftValues = new int[middle];
            var rightValues = new int[length - middle];

            // Dividing array into two and copying into two separate arrays
            var rightIndex = 0;

            for (var index = 0; index < length; ++index)
            {
                if (index < middle)
                {
                    leftValues[index] = values[index];
                }
                else
                {
                    rightValues[rightIndex] = values[index];
                    rightIndex++;
                }
            }

            // Recursively calling the function to divide the subarrays further
            MergeSort(leftValues);
            MergeSort(rightValues);

            // Calling the merge method on each subdivision
            Merge(leftValues, rightValues, values);
        }

        private void Merge(int[] leftValues, int[] rightValues, int[] values)
        {
            var index = 0;
            var leftIndex = 0;
            var rightIndex = 0;

            var leftSize = leftValues.Length;
            var rightSize = rightValues.Length;

            // The while loops check the conditions for merging
            while (leftIndex < leftSize && rightIndex < rightSize)
            {
                if (leftValues[leftIndex] < rightValues[rightIndex])
                {
                    values[index++] = leftValues[leftIndex++];
                }
                else
                {
                    values[index++] = rightValues[rightIndex++];
                }
            }

            while (leftIndex < leftSize)
            {
                values[index++] = leftValues[leftIndex++];
            }

            while (rightIndex < rightSize)
            {
                values[index++] = rightValues[rightIndex++];
            }
        }
    }
}