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
		public string Sub (SubOperators numbersForSubtract)
		{
			double result = 0;
			SubDiference objectFinal = new SubDiference();

			if (numbersForSubtract.Operators == null || numbersForSubtract == null)
			{
				objectFinal.Diference = null;
			}
			else
			{
				double[] arr = new double[numbersForSubtract.Operators.Count];
				List<int> list = new List<int>();

				for(int i = 0; i < numbersForSubtract.Operators.Count; i++) {
					list.Add(Convert.ToInt32(numbersForSubtract.Operators[i]));
				}

				list.Sort();
				list.Reverse();
				result = list[0];
				list.RemoveRange(0,1);
				

				foreach (double num in list)
				{
					result = result - num;
				}
				objectFinal.Diference = Convert.ToString(result);
			}

			return JsonConvert.SerializeObject(objectFinal);
		}

		[HttpPost]
		public string Mult(MultFactors numbersForMult)
		{
			double result = 0;
			MultProduct objectFinal = new MultProduct();

			if (numbersForMult.Factors == null || numbersForMult == null)
			{
				objectFinal.Product = null;
			}
			else
			{
				result = 1;
				foreach (double num in numbersForMult.Factors)
				{
					result *= num;
				}
				objectFinal.Product = Convert.ToString(result);
			}

			return JsonConvert.SerializeObject(objectFinal);
		}//Mult

		[HttpPost]
		public string Div(DivDividendDivisor numbersForDiv)
		{
			DivQuotientRemainder objectFinal = new DivQuotientRemainder();
			if (numbersForDiv.Dividend == null && numbersForDiv.Divisor == null || numbersForDiv == null)
			{
				objectFinal.Quotient = null;
				objectFinal.Remainder = null;
			}
			else
			{
				double Quotient = Convert.ToInt32(numbersForDiv.Dividend) / Convert.ToInt32(numbersForDiv.Divisor);
				double Remainder = Convert.ToDouble(numbersForDiv.Dividend) % Convert.ToDouble(numbersForDiv.Divisor);

				objectFinal.Quotient = Convert.ToString(Quotient);
				objectFinal.Remainder = Convert.ToString(Remainder);
			}

			return JsonConvert.SerializeObject(objectFinal);
		}//Mult
	}
}