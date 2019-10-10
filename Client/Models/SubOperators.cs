using System.Collections.Generic;

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