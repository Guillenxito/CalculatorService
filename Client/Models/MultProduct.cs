using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client.Models
{
	public class MultProduct
	{
		public string Product { get; set; }

		public MultProduct(string prod)
		{
			Product = prod;
		}
		public MultProduct()
		{
		}
	}
}