using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client.Models
{
	public class SubOperators
	{
		public List<double> Operators { get; set; }

		public SubOperators(List<double> Ope)
		{
			Operators = Ope;
		}
	}
}