using System;

namespace CalculatorS.Models
{
	public class Query
	{
		public string Operation { get; set; }
		public string Calculation { get; set; }
		public DateTime Date { get; set; }

		public Query(string oper,string calc) {
			Operation = oper;
			Calculation = calc;
			Date = DateTime.UtcNow;
		}
	}
}