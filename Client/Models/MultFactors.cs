using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client.Models
{
	public class MultFactors
	{
		public List<string> Factors { get; set; }

		public MultFactors(List<string> data)
		{
			Factors = data;
		}

		public MultFactors()
		{
		}
	}
}