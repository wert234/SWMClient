using SWMClient.Models.JsonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWMClient.Models.Extensions
{
    public static class DwarvesExtentions
    {
        public static IEnumerable<Dwarvecs> OrderByDwarves(this IEnumerable<Dwarvecs> collection)
        {
            var dwarvecs = new List<Dwarvecs>();

            foreach (var item1 in collection)
            {
                var dwarve = item1;
                foreach (var item2 in collection)
                {
                    if (dwarve.name.ToLower()[0] > item2.name.ToLower()[0])
                        dwarve = item2;
                }

                dwarvecs.Add(dwarve);
            }
            return dwarvecs;
        }
    }
}
