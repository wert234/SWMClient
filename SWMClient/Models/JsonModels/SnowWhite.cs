using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWMClient.Models.JsonModels
{
    public class SnowWhite
    {
        public int id { get; set; }
        public string fullName { get; set; }
        public IEnumerable<Dwarvecs> dwarves { get; set; }
    }
}
