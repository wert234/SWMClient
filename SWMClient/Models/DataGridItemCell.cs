using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWMClient.Models
{
    public class DataGridItemCell
    {
        public double ColumnSpacing { get; set; }
        public string Title { get; set; }
        public Color TitleColor { get; set; } = new Color(109, 96, 248);

        public bool isReadOnly { get; set; } = false;
    }
}
