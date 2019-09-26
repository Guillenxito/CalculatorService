using CalculatorS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CalculatorS.Controllers
{
    public class CalculatorSController : Controller
    {
		[HttpPost]
		public string Add(AddAddends numbersForAdd)
		{
			double result = 0;
			AddSum objectFinal = new AddSum();

			if (numbersForAdd.Addends == null || numbersForAdd == null ) {
				objectFinal.Sum = null;
			} else {
				result = 0;
				foreach (double num in numbersForAdd.Addends)
				{
					result += num;
				}
				objectFinal.Sum = Convert.ToString(result);
			}

			return JsonConvert.SerializeObject(objectFinal);
			
		}//Add

		[HttpPost]
		public double Sub (SubOperators numbersForSubtract)
		{
			var result = numbersForSubtract.Subtract();
			return result;
		}

	}
}