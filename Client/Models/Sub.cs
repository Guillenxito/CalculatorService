using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client.Models
{
	public class Sub
	{
		public List<double> Operators { get; set; }

		public Sub(List<double> Ope)
		{
			Operators = Ope;
		}
	}
}