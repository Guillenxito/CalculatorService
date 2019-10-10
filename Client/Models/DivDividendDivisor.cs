﻿namespace Client.Models
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