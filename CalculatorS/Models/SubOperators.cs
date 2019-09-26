using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalculatorS.Models
{
	public class SubOperators
	{
		public List<double> Operators { get; set; }

		public SubOperators (List<double> data){
			Operators = data;
		}

		public SubOperators() {
		}

		public double Subtract(){
			double result = 0;

			foreach (double element in Operators){
				result += element;
			}
			return result;
		}// Subtract

	}
}