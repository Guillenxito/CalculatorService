using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalculatorS
{
	public static class Program
	{
		public static string GetIdTracking(string id) {
			if (id.Any())
			{
				return id;
			}
			else
			{
				return "NULL";
			}
		}
	}
}