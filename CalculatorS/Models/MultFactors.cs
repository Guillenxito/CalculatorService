using System.Collections.Generic;

namespace CalculatorS.Models
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