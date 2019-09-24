using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalculatorS.Models
{
	public class Add
	{
		public List<double> Addends { get; set; }

		public Add (List<double> data) {
			Addends = data;
		}

		public Add() {
		}
		
		public double Sum() {
			double result = 0;

			foreach(double element in Addends) {
				result += element;
			}
			return result;
		}//Sum

	}
}