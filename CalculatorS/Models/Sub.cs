using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalculatorS.Models
{
	public class Sub
	{
		public List<double> Operators { get; set; }

		public Sub (List<double> data){
			Operators = data;
		}

		public Sub() {
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