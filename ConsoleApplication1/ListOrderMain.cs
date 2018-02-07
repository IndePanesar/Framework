using System;
using TT_ListOrder;

namespace ConsoleAppForListOrder
{
    internal class ListOrderMain
    {
        private static void Main()
        {
            // Have a 2D array of integers to test with
            var unorderInts = new[]
            {
                new[] {1, 3, 5, 6, 3, 5, 6, 7, 8, 9, 0, 9},
                new[] {1, 2, 3, 4, 5, 6},
                new[] {0, 0, 0, 0},
                new[] {-1, -2, -3, -2, -1, 0, 1, 5}
            };

            var ttListOrdering = new TT_ListOrdering();
            foreach (var intList in unorderInts)
            {
                var largestOrderList = ttListOrdering.GetLargestOrderList(intList);

                Console.WriteLine($"The largest ordered list from ({string.Join(",",intList)}) is of length {largestOrderList.Count}, and the values are:-");
                Console.WriteLine($"({string.Join(",", largestOrderList)})");
                Console.WriteLine("\n\n");
            }

            Environment.Exit(0);
        }
    }
}

