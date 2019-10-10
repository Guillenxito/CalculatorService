using System;
using System.Collections.Generic;

namespace CalculatorS.Models
{
	public class SubOperators
	{
		public List<string> Operators { get; set; }

		public SubOperators (List<string> data){
			Operators = data;
		}

		public SubOperators() {
		}

		public double Subtract(){
			double result = 0;

			foreach (string element in Operators){
				result += Convert.ToDouble(element);
			}
			return result;
		}// Subtract

	}
}