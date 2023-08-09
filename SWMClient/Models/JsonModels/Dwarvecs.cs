using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SWMClient.Models.JsonModels
{
    public class Dwarvecs
    {
        public int id { get; set; }
        public string name { get; set; }

        [JsonPropertyName("class")]
        public int Class { get; set; }
    }
}
