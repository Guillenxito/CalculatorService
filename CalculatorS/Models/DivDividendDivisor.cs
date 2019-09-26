using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalculatorS.Models
{
	public class DivDividendDivisor
	{
		public string Dividend { set; get; }
		public string Divisor { set; get; }

		public DivDividendDivisor() {

		}
		public DivDividendDivisor(string dividend,string divisor) 
		{
			Dividend = dividend;
			Divisor = divisor;

		}
	}
}