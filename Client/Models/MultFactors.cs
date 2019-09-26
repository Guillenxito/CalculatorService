using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client.Models
{
	public class MultFactors
	{
		public List<double> Factors { get; set; }

		public MultFactors(List<double> data)
		{
			Factors = data;
		}

		public MultFactors()
		{
		}
	}
}