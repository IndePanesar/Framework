using System.Collections.Generic;
using System.Linq;

namespace TT_ListOrder
{
    public class TT_ListOrdering
    {
        // Function to return largest orderd sublist from a list of integers
        public List<int> GetLargestOrderList(int[] p_Type)
        {
            var largest_order = new List<int>();
            if (p_Type.Length == 0)
                return largest_order;

            var groupeditems = p_Type.GetIntegerGroups().ToList();
            var giOrdered = groupeditems.OrderByDescending(gg => gg.Count()).ToList();
            return (List<int>)giOrdered.FirstOrDefault();
        }
    }

    internal static class MyExtensionsMethods
    {
        // An extension method on IEnumerable<int> to return sub list orderd integers 
        public static IEnumerable<IEnumerable<int>> GetIntegerGroups(this IEnumerable<int> IntList)
        {
            var ilist = new List<int>();        // create an empty integers list

            // iterate through the list and yield the list of ordered numbers
            foreach (var number in IntList)
            {
                // We have a currently active list 
                if (ilist.Count == 0 || ilist.Last() < number)
                    ilist.Add(number);
                else
                {
                    // Yield this list and 
                    yield return ilist;
                    ilist = new List<int> { number };
                }
            }
            yield return ilist;
        }
    }
}
