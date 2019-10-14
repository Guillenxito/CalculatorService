using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Newtonsoft.Json;

using NLog;

using CalculatorS.Models;

namespace CalculatorS.Controllers
{
    public class CalculatorSController : Controller
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();

		[HttpPost] //CLOSED
		public string Add(AddAddends NumbersForAdd)
		{
			string stringOperationLog = "";
			try {

				if (NumbersForAdd == null || NumbersForAdd.Addends == null)
				{
					Error objectFinalError = new Error();
					objectFinalError.Error400();
					logger.Trace("ERROR ADD -> ID : " + Request.Headers["X-Evi-Tracking-Id"] + " MENSAJE : OPERADORES NULOS.");
					return JsonConvert.SerializeObject(objectFinalError);
				}
				AddSum responseSum = new AddSum();
				double result = 0;
				double numD;
				
				foreach (string num in NumbersForAdd.Addends)
				{
					if (!double.TryParse(num, out numD)){
						Error objectFinalError = new Error();
						objectFinalError.Error400();
						logger.Trace("ERROR ADD -> ID : " + Request.Headers["X-Evi-Tracking-Id"] + " MENSAJE : OPERADORES INVALIDOS.");
						return JsonConvert.SerializeObject(objectFinalError);
					}
					if(num.Contains(",")) {
						string[] arr = num.Split(',');
						string numAux = string.Join(".",arr);
						result += Convert.ToDouble(numAux);
					}else {
						result += Convert.ToDouble(num);
					}	
				}
				stringOperationLog = string.Join(" + ",NumbersForAdd.Addends) + " = " + result ;

				if (Request.Headers["X-Evi-Tracking-Id"].Any())
				{
					string calculation = string.Join(" + ", NumbersForAdd.Addends);
					Query Operation = new Query("Sum ", calculation + " = " + result);
					string jsonOperation = JsonConvert.SerializeObject(Operation);
					Journal journal = new Journal(Request.Headers["X-Evi-Tracking-Id"]);
					journal.SaveJournal(jsonOperation);
					stringOperationLog = "ADD  -> ID : " + Request.Headers["X-Evi-Tracking-Id"] + "  OPERATION :" + stringOperationLog;	
				}
				else {
					stringOperationLog = "ADD  -> ID : NULL " + "  OPERATION :" + stringOperationLog;
				}

				logger.Trace(stringOperationLog);

				responseSum.Sum = Convert.ToString(result);
				return JsonConvert.SerializeObject(responseSum);
			}catch(Exception ex) {
				Error objectFinalError = new Error();
				objectFinalError.Error500(ex.StackTrace);
				return JsonConvert.SerializeObject(objectFinalError);
			}
		}//Add

		[HttpPost] //CLOSED
		public string Sub (SubOperators NumbersForSubtract)
		{
			try { 
				double result = 0;
				string stringOperationLog = "";
				SubDiference responseDiference = new SubDiference();

				if (NumbersForSubtract.Operators == null || NumbersForSubtract == null)
				{
					Error objectFinalError = new Error();
					objectFinalError.Error400();
					logger.Trace("ERROR SUB -> ID : " + Request.Headers["X-Evi-Tracking-Id"] + " MENSAJE : OPERADORES NULOS.");
					return JsonConvert.SerializeObject(objectFinalError);
				}

				

				double numD;
				List<double> list = new List<double>();
				foreach (string num in NumbersForSubtract.Operators)
				{
					if (!double.TryParse(num, out numD))
					{
						Error objectFinalError = new Error();
						objectFinalError.Error400();
						logger.Trace("ERROR SUB -> ID : " + Request.Headers["X-Evi-Tracking-Id"] + " MENSAJE : OPERADORES INVALIDOS.");
						return JsonConvert.SerializeObject(objectFinalError);
					}

					if (num.Contains(","))
					{
						string[] arrSub = num.Split(',');
						string numAux = string.Join(".", arrSub);
						list.Add(Convert.ToDouble(numAux));
					}
					else
					{
						list.Add(Convert.ToDouble(num));
					}
				}
				result = list[0];
				list.RemoveRange(0,1);
				
				foreach (double num in list)
				{
					result = result - num;
				
				}
				stringOperationLog = string.Join(" - ", NumbersForSubtract.Operators) + " = " + result;
				result = Math.Round(result,2);
				if (Request.Headers["X-Evi-Tracking-Id"].Any())
				{
					string calculation = string.Join(" - ", NumbersForSubtract.Operators);
					Query Operation = new Query("Sub ", calculation + " = " + result);
					string jsonOperation = JsonConvert.SerializeObject(Operation);
					Journal journal = new Journal(Request.Headers["X-Evi-Tracking-Id"]);
					journal.SaveJournal(jsonOperation);
					stringOperationLog = "SUB  -> ID : " + Request.Headers["X-Evi-Tracking-Id"] + "  OPERATION :" + stringOperationLog;
				}
				else
				{
					stringOperationLog = "SUB  -> ID : NULL " + "  OPERATION :" + stringOperationLog;
				}
				logger.Trace(stringOperationLog);
				responseDiference.Diference = Convert.ToString(result);
				return JsonConvert.SerializeObject(responseDiference);
			}
			catch (Exception ex) 
			{
				Error objectFinalError = new Error();
				objectFinalError.Error500(ex.StackTrace);
				return JsonConvert.SerializeObject(objectFinalError);
			}
		}//Sub

		[HttpPost] //CLOSED
		public string Mult(MultFactors NumbersForMult)
		{ 
			try {
				double result = 1;
				string stringOperationLog = "";
				MultProduct responseProduct = new MultProduct();

				if (NumbersForMult == null || NumbersForMult.Factors == null)
				{
					Error objectFinalError = new Error();
					objectFinalError.Error400();
					logger.Trace("ERROR MULT -> ID : " + Request.Headers["X-Evi-Tracking-Id"] + " MENSAJE : OPERADORES NULOS.");
					return JsonConvert.SerializeObject(objectFinalError);
				}

				double numD;
				foreach (string num in NumbersForMult.Factors)
				{
					if (!double.TryParse(num, out numD))
					{
						Error objectFinalError = new Error();
						objectFinalError.Error400();
						logger.Trace("ERROR MULT -> ID : " + Request.Headers["X-Evi-Tracking-Id"] + " MENSAJE : OPERADORES INVALIDOS.");
						return JsonConvert.SerializeObject(objectFinalError);
					}
					if (num.Contains(","))
					{
						string[] arr = num.Split(',');
						string numAux = string.Join(".", arr);
						result += Convert.ToDouble(numAux);
					}
					else
					{
						result *= Convert.ToDouble(num);
					}
					
				}
				stringOperationLog = string.Join(" * ", NumbersForMult.Factors) + " = " + result;
				if (Request.Headers["X-Evi-Tracking-Id"].Any())
				{
					string calculation = string.Join(" * ", NumbersForMult.Factors);
					Query Operation = new Query("Mult", calculation + " = " + result);
					string jsonOperation = JsonConvert.SerializeObject(Operation);
					Journal journal = new Journal(Request.Headers["X-Evi-Tracking-Id"]);
					journal.SaveJournal(jsonOperation);
					stringOperationLog = "MULT -> ID : " + Request.Headers["X-Evi-Tracking-Id"] + "  OPERATION :" + stringOperationLog;
				}
				else
				{
					stringOperationLog = "MULT -> ID : NULL " + "  OPERATION :" + stringOperationLog;
				}
				logger.Trace(stringOperationLog);

				responseProduct.Product = Convert.ToString(result);
				return JsonConvert.SerializeObject(responseProduct);
			}
			catch (Exception ex) {
				Error objectFinalError = new Error();
				objectFinalError.Error500(ex.StackTrace);
				return JsonConvert.SerializeObject(objectFinalError);
			}
		}//Mult

		[HttpPost] //CLOSED
		public string Div(DivDividendDivisor NumbersForDiv)
		{
			try {
				string stringOperationLog = "";
				if (NumbersForDiv == null || NumbersForDiv.Dividend == null && NumbersForDiv.Divisor == null )
				{
					Error objectFinalError = new Error();
					objectFinalError.Error400();
					logger.Trace("ERROR DIV -> ID : " + Request.Headers["X-Evi-Tracking-Id"] + " MENSAJE : OPERADORES NULOS.");
					return JsonConvert.SerializeObject(objectFinalError);
				}

				double numD;
				if (!double.TryParse(NumbersForDiv.Dividend, out numD) || !double.TryParse(NumbersForDiv.Divisor, out numD))
				{
					Error objectFinalError = new Error();
					objectFinalError.Error400();
					logger.Trace("ERROR DIV -> ID : " + Request.Headers["X-Evi-Tracking-Id"] + " MENSAJE : OPERADORES INVALIDOS.");
					return JsonConvert.SerializeObject(objectFinalError);
				}

				if (Convert.ToDouble(NumbersForDiv.Dividend) == 0){
					Error objectFinalError = new Error();
					objectFinalError.Error400();
					logger.Trace("ERROR DIV -> ID : " + Request.Headers["X-Evi-Tracking-Id"] + " MENSAJE : NO SE PUEDE DIVIDIR ENTRE 0.");
					objectFinalError.ErrorMessage = "Cannot be divided by 0";
					return JsonConvert.SerializeObject(objectFinalError);
				}

				if (NumbersForDiv.Dividend.Contains(","))
				{
					string[] arr = NumbersForDiv.Dividend.Split(',');
					string numAux = string.Join(".", arr);
					NumbersForDiv.Dividend = numAux;
				}

				if (NumbersForDiv.Divisor.Contains(","))
				{
					string[] arr = NumbersForDiv.Divisor.Split(',');
					string numAux = string.Join(".", arr);
					NumbersForDiv.Divisor = numAux;
				}

				double Quotient = Convert.ToDouble(NumbersForDiv.Dividend) / Convert.ToDouble(NumbersForDiv.Divisor);
				double Remainder = Convert.ToDouble(NumbersForDiv.Dividend) % Convert.ToDouble(NumbersForDiv.Divisor);
				DivQuotientRemainder responseDiv = new DivQuotientRemainder(Convert.ToString(Quotient), Convert.ToString(Remainder));
				stringOperationLog = NumbersForDiv.Dividend + " / "+ NumbersForDiv.Divisor + " = Quotient("+Convert.ToString(Quotient) + ") & Remainder(" + Convert.ToString(Remainder) + ")" ;
				if (Request.Headers["X-Evi-Tracking-Id"].Any())
				{
					string calculation = NumbersForDiv.Dividend + " / " + NumbersForDiv.Divisor + " = Quotient(" + responseDiv.Quotient+ ") & Remainder(" + responseDiv.Remainder + ")";
					Query Operation = new Query("Div ", calculation);
					string jsonOperation = JsonConvert.SerializeObject(Operation);
					Journal journal = new Journal(Request.Headers["X-Evi-Tracking-Id"]);
					journal.SaveJournal(jsonOperation);
					stringOperationLog = "DIV  -> ID : " + Request.Headers["X-Evi-Tracking-Id"] + "  OPERATION :" + stringOperationLog;
				}
				else
				{
					stringOperationLog = "DIV  -> ID : NULL " + "  OPERATION :" + stringOperationLog;
				}
				logger.Trace(stringOperationLog);
				return JsonConvert.SerializeObject(responseDiv);
			}
			catch (Exception ex) {
				Error objectFinalError = new Error();
				objectFinalError.Error500(ex.StackTrace);
				return JsonConvert.SerializeObject(objectFinalError);
			}
		}//Div

		[HttpPost] //CLOSED
		public string SQRT(SQRTnumber NumberForSQRT)
		{
			string stringOperationLog = "";
			try
			{
				if(NumberForSQRT == null || NumberForSQRT.Number == null)
				{
					Error objectFinalError = new Error();
					objectFinalError.Error400();
					logger.Trace("ERROR SQRT -> ID : " + Request.Headers["X-Evi-Tracking-Id"] + " MENSAJE : OPERADORES NULOS.");
					return JsonConvert.SerializeObject(objectFinalError);
				}
				double numD;
				if (!double.TryParse(NumberForSQRT.Number, out numD))
				{
					Error objectFinalError = new Error();
					objectFinalError.Error400();
					logger.Trace("ERROR SQRT -> ID : " + Request.Headers["X-Evi-Tracking-Id"] + " MENSAJE : OPERADORES INVALIDOS.");
					return JsonConvert.SerializeObject(objectFinalError);
				}else if (0 > Convert.ToDouble(NumberForSQRT.Number))
				{
					Error objectFinalError = new Error();
					objectFinalError.Error400();
					logger.Trace("ERROR SQRT -> ID : " + Request.Headers["X-Evi-Tracking-Id"] + " MENSAJE : NO SE PUEDE HACER SQRT DE 0.");
					objectFinalError.ErrorMessage = "No se puede hcaer una raiz cuadrada de un numero negativo";
					return JsonConvert.SerializeObject(objectFinalError);
				}

				if (NumberForSQRT.Number.Contains(","))
				{
					string[] arr = NumberForSQRT.Number.Split(',');
					string numAux = string.Join(".", arr);
					NumberForSQRT.Number = numAux;
				}
				SQRTsquare responseSquare = new SQRTsquare();
				double result = Math.Sqrt(Convert.ToDouble(NumberForSQRT.Number));
				stringOperationLog = " √" + NumberForSQRT.Number + " = " + result;
				if (Request.Headers["X-Evi-Tracking-Id"].Any())
				{
					string calculation = " √"+ NumberForSQRT.Number;
					Query Operation = new Query("Sqrt", calculation + " = " + result);
					string jsonOperation = JsonConvert.SerializeObject(Operation);
					Journal journal = new Journal(Request.Headers["X-Evi-Tracking-Id"]);
					journal.SaveJournal(jsonOperation);
					stringOperationLog = "SQRT -> ID : " + Request.Headers["X-Evi-Tracking-Id"] + "  OPERATION :" + stringOperationLog;
				}
				else
				{
					stringOperationLog = "SQRT -> ID : NULL " + "  OPERATION :" + stringOperationLog;
				}

				logger.Trace(stringOperationLog);
				responseSquare.Square = Convert.ToString(result);
				return JsonConvert.SerializeObject(responseSquare);
			}
			catch (Exception ex)
			{
				Error objectFinalError = new Error();
				objectFinalError.Error500(ex.StackTrace);
				return JsonConvert.SerializeObject(objectFinalError);
			}
		}//SQRT
	}
}