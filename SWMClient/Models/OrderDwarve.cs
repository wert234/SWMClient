using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWMClient.Models
{
    public class OrderDwarve
    {
		private string fullName;

		public string FullName
		{
			get { return fullName; }
			set { fullName = value; }
		}

		private int classDwarve;

		public int ClassDwarve
        {
			get { return classDwarve; }
			set { classDwarve = value; }
		}

		public OrderDwarve(string fullName, int classDwarve)
		{
			FullName = fullName;
			ClassDwarve = classDwarve;
		}
    }
}
