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
		protected string idTraking { get; set; }
		protected Query listTraking;
		protected bool trakingReady = false;

		public void haveTraking() {
			//var c = Request.Headers["X-Evi-Tracking-Id"];
			if(!string.IsNullOrEmpty(Request.Headers["X-Evi-Tracking-Id"])) {
				idTraking = Request.Headers["X-Evi-Tracking-Id"];
				listTraking = new Query(id);
				trakingReady = true;
			}
			else{
				idTraking = null;
				listTraking = null;
				trakingReady = false;
			}
		}

		[HttpPost]
		public string Add(AddAddends numbersForAdd)
		{
			try {
				haveTraking();
				if (numbersForAdd.Addends == null || numbersForAdd == null)
				{
					Error objectFinalError = new Error();
					objectFinalError.Error400();
					return JsonConvert.SerializeObject(objectFinalError);
				}
				AddSum objectFinal = new AddSum();
				double result = 0;
				string calculation = "";
				foreach (double num in numbersForAdd.Addends)
				{
					result += num;
					calculation += num+"+";
				}
				if(trakingReady) {
					calculation.Substring(0, -1);
					QueryOperation qp = new QueryOperation("Sum",calculation);
					listTraking.setOperation(qp);
				}
				objectFinal.Sum = Convert.ToString(result);
				return JsonConvert.SerializeObject(objectFinal);
			}catch(Exception ex) {
				Error objectFinalError = new Error();
				objectFinalError.Error500(ex.ToString());
				return JsonConvert.SerializeObject(objectFinalError);
			}
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