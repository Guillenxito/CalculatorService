using System.Collections.Generic;

namespace CalculatorS.Models
{
	public class Operations
	{
		public IList<Query> ListOperations { get; set; }

		public Operations (List<Query>oper) {
			ListOperations = oper;
		}
		public Operations()
		{
		}
	}
}