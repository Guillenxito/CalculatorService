using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client.Models
{
	public class SubOperators
	{
		public List<string> Operators { get; set; }

		public SubOperators(List<string> Ope)
		{
			Operators = Ope;
		}
	}
}