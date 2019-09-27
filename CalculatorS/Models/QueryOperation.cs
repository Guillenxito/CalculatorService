using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalculatorS.Models
{
	public class QueryOperation 
	{
		public string Operation { get; set; }
		public string Calculation { get; set; }
		public DateTime Date { get; set; }

		public QueryOperation(string oper,string calc) {
			Operation = oper;
			Calculation = calc;
			Date = new DateTime();
		}
	}
}