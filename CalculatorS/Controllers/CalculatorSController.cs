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

		[HttpPost] //CLOSED
		public string Add(AddAddends numbersForAdd)
		{
			try {
				if (numbersForAdd.Addends == null || numbersForAdd == null)
				{
					Error objectFinalError = new Error();
					objectFinalError.Error400();
					return JsonConvert.SerializeObject(objectFinalError);
				}
				AddSum objectFinal = new AddSum();
				double result = 0;
				double numD;
				foreach (string num in numbersForAdd.Addends)
				{
					if (!double.TryParse(num, out numD))
					{
						Error objectFinalError = new Error();
						objectFinalError.Error400();
						return JsonConvert.SerializeObject(objectFinalError);
					}
					result += Convert.ToDouble(num);
				}

				if (Request.Headers["X-Evi-Tracking-Id"].Any())
				{
					string calculation = string.Join(" + ", numbersForAdd.Addends);
					Query Operation = new Query("Sum", calculation + " = " + result);
					string jsonOperation = JsonConvert.SerializeObject(Operation);
					Journal journal = new Journal(Request.Headers["X-Evi-Tracking-Id"]);
					journal.saveJournal(jsonOperation);
				}

				objectFinal.Sum = Convert.ToString(result);
				return JsonConvert.SerializeObject(objectFinal);
			}catch(Exception ex) {
				Error objectFinalError = new Error();
				objectFinalError.Error500(ex.ToString());
				return JsonConvert.SerializeObject(objectFinalError);
			}
		}//Add

		[HttpPost] //CLOSED
		public string Sub (SubOperators numbersForSubtract)
		{
			try { 
				double result = 0;
				SubDiference objectFinal = new SubDiference();

				if (numbersForSubtract.Operators == null || numbersForSubtract == null)
				{
					Error objectFinalError = new Error();
					objectFinalError.Error400();
					return JsonConvert.SerializeObject(objectFinalError);
				}

				double[] arr = new double[numbersForSubtract.Operators.Count];
				List<int> list = new List<int>();

				double numD;
				foreach (string num in numbersForSubtract.Operators)
				{
					if (!double.TryParse(num, out numD))
					{
						Error objectFinalError = new Error();
						objectFinalError.Error400();
						return JsonConvert.SerializeObject(objectFinalError);
					}
					result += Convert.ToDouble(num);
				}

				for (int i = 0; i < numbersForSubtract.Operators.Count; i++) {
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
				if (Request.Headers["X-Evi-Tracking-Id"].Any())
				{
					string calculation = string.Join(" - ", numbersForSubtract.Operators);
					Query Operation = new Query("Sub", calculation + " = " + result);
					string jsonOperation = JsonConvert.SerializeObject(Operation);
					Journal journal = new Journal(Request.Headers["X-Evi-Tracking-Id"]);
					journal.saveJournal(jsonOperation);
				}
				objectFinal.Diference = Convert.ToString(result);
				return JsonConvert.SerializeObject(objectFinal);
			}
			catch (Exception ex) 
			{
				Error objectFinalError = new Error();
				objectFinalError.Error500(ex.ToString());
				return JsonConvert.SerializeObject(objectFinalError);
			}
		}//Sub

		[HttpPost] //CLOSED
		public string Mult(MultFactors numbersForMult)
		{ 
			try {
				double result = 1;
				MultProduct objectFinal = new MultProduct();

				if (numbersForMult.Factors == null || numbersForMult == null)
				{
					Error objectFinalError = new Error();
					objectFinalError.Error400();
					return JsonConvert.SerializeObject(objectFinalError);
				}
	
				double numD;
				foreach (string num in numbersForMult.Factors)
				{
					if (!double.TryParse(num, out numD))
					{
						Error objectFinalError = new Error();
						objectFinalError.Error400();
						return JsonConvert.SerializeObject(objectFinalError);
					}
					result *= Convert.ToDouble(num);
				}

				if (Request.Headers["X-Evi-Tracking-Id"].Any())
				{
					string calculation = string.Join(" * ", numbersForMult.Factors);
					Query Operation = new Query("Mult", calculation + " = " + result);
					string jsonOperation = JsonConvert.SerializeObject(Operation);
					Journal journal = new Journal(Request.Headers["X-Evi-Tracking-Id"]);
					journal.saveJournal(jsonOperation);
				}

				objectFinal.Product = Convert.ToString(result);
				return JsonConvert.SerializeObject(objectFinal);
			}
			catch (Exception ex) {
				Error objectFinalError = new Error();
				objectFinalError.Error500(ex.ToString());
				return JsonConvert.SerializeObject(objectFinalError);
			}
		}//Mult

		[HttpPost] //CLOSED
		public string Div(DivDividendDivisor numbersForDiv)
		{
			try { 
				if (numbersForDiv.Dividend == null && numbersForDiv.Divisor == null || numbersForDiv == null)
				{
					Error objectFinalError = new Error();
					objectFinalError.Error400();
					return JsonConvert.SerializeObject(objectFinalError);
				}

				double numD;
				if (!double.TryParse(numbersForDiv.Dividend, out numD) || !double.TryParse(numbersForDiv.Divisor, out numD))
				{
					Error objectFinalError = new Error();
					objectFinalError.Error400();
					return JsonConvert.SerializeObject(objectFinalError);
				}

				if (Convert.ToInt32(numbersForDiv.Dividend) == 0){
					Error objectFinalError = new Error();
					objectFinalError.Error400();
					objectFinalError.ErrorMessage = "Cannot be divided by 0";
					return JsonConvert.SerializeObject(objectFinalError);
				}

				double Quotient = Convert.ToInt32(numbersForDiv.Dividend) / Convert.ToInt32(numbersForDiv.Divisor);
				double Remainder = Convert.ToDouble(numbersForDiv.Dividend) % Convert.ToDouble(numbersForDiv.Divisor);
				DivQuotientRemainder objectFinal = new DivQuotientRemainder(Convert.ToString(Quotient), Convert.ToString(Remainder));
				//objectFinal.Quotient = Convert.ToString(Quotient);
				//objectFinal.Remainder = Convert.ToString(Remainder);

				if (Request.Headers["X-Evi-Tracking-Id"].Any())
				{
					string calculation = numbersForDiv.Dividend + " / " + numbersForDiv.Divisor + " = Quotient(" + objectFinal.Quotient+ ") & Remainder("+ objectFinal.Remainder + ")";
					Query Operation = new Query("Div", calculation);
					string jsonOperation = JsonConvert.SerializeObject(Operation);
					Journal journal = new Journal(Request.Headers["X-Evi-Tracking-Id"]);
					journal.saveJournal(jsonOperation);
				}
				return JsonConvert.SerializeObject(objectFinal);
			}
			catch (Exception ex) {
				Error objectFinalError = new Error();
				objectFinalError.Error500(ex.ToString());
				return JsonConvert.SerializeObject(objectFinalError);
			}
		}//Div

		[HttpPost] //CLOSED
		public string SQRT(SQRTnumber numberForSQRT)
		{
			try
			{
				if (numberForSQRT.Number == null || numberForSQRT == null)
				{
					Error objectFinalError = new Error();
					objectFinalError.Error400();
					return JsonConvert.SerializeObject(objectFinalError);
				}
				double numD;
				if (!double.TryParse(numberForSQRT.Number, out numD))
				{
					Error objectFinalError = new Error();
					objectFinalError.Error400();
					return JsonConvert.SerializeObject(objectFinalError);
				}else if (0 > Convert.ToDouble(numberForSQRT.Number))
				{
					Error objectFinalError = new Error();
					objectFinalError.Error400();
					//objectFinalError.ErrorMessage = "the SQRT of a negative number cannot be made";
					objectFinalError.ErrorMessage = "No se puede hcaer una raiz cuadrada de un numero negativo";
					return JsonConvert.SerializeObject(objectFinalError);
				}

				SQRTsquare objectFinal = new SQRTsquare();
				double result = Math.Sqrt(Convert.ToDouble(numberForSQRT.Number));

				if (Request.Headers["X-Evi-Tracking-Id"].Any())
				{
					string calculation = " √"+ numberForSQRT.Number;
					Query Operation = new Query("Sqrt", calculation + " = " + result);
					string jsonOperation = JsonConvert.SerializeObject(Operation);
					Journal journal = new Journal(Request.Headers["X-Evi-Tracking-Id"]);
					journal.saveJournal(jsonOperation);
				}

				objectFinal.Square = Convert.ToString(result);
				return JsonConvert.SerializeObject(objectFinal);
			}
			catch (Exception ex)
			{
				Error objectFinalError = new Error();
				objectFinalError.Error500(ex.ToString());
				return JsonConvert.SerializeObject(objectFinalError);
			}
		}//SQRT

		[HttpPost] //CLOSED
		public string existJournal(Journal idForJournal)
		{
			try
			{
				if (idForJournal.Id == null || idForJournal == null)
				{
					Error objectFinalError = new Error();
					objectFinalError.Error400();
					return JsonConvert.SerializeObject(objectFinalError);
				}


				if (idForJournal.existJournal()) {
					return idForJournal.Id;				
				 }

				return null;

			}
			catch (Exception ex)
			{
				Error objectFinalError = new Error();
				objectFinalError.Error500(ex.ToString());
				return JsonConvert.SerializeObject(objectFinalError);
			}
		}//SQRT

		[HttpPost] //CLOSED
		public string Journal(Journal idForJournal)
		{
			try
			{
				if (idForJournal.Id == null || idForJournal == null)
				{
					Error objectFinalError = new Error();
					objectFinalError.Error400();
					return JsonConvert.SerializeObject(objectFinalError);
				}

				if (idForJournal.existJournal() )
				{
					return idForJournal.readJournal();
				}
				return null;
			}
			catch (Exception ex)
			{
				Error objectFinalError = new Error();
				objectFinalError.Error500(ex.ToString());
				return JsonConvert.SerializeObject(objectFinalError);
			}
		}//SQRT
	}
}