using System;

namespace Client.Models
{
	public class Query
	{
		public string Operation { get; set; }
		public string Calculation { get; set; }
		public DateTime Date { get; set; }


		public Query() { }

		public Query(string oper,string calc) {
			Operation = oper;
			Calculation = calc;
			Date = DateTime.UtcNow;
		}
	}
}