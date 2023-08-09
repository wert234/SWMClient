using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWMClient.Models.JsonModels
{
    internal class Authorization
    {
        public string access_token { get; set; }
        public int snowWhiteId { get; set; }
    }
}
