using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Client.Models;

namespace Client
{
	class Program
	{
		protected static string urlServer = "http://localhost:50727/CalculatorS/";
		protected static string action = "";

		static void Main(string[] args)
		{

			bool operation = false;
			string option = "Default";

			do
			{
				
				displayMenu();
				option = Console.ReadLine();
				operation = getOption(option);

				if (!action.Equals("Default")) {
					List<double> data;
					if (action.Equals("Div")) {
						data = getData(2);
					}
					else
					{
						data = getData();
					}

					if (data == null)
					{
						Console.WriteLine("OPERACION CANCELADA");
					}
					else
					{
						showResult(data);
						Console.ReadKey();
					}
				}
			} while (operation);

			Console.ReadKey();




		}//Main

		public static  void showResult(List<double> data) {
			string responseFinal = null;
			string responseServer = "";
			switch (action)
			{
				case "Add":
					AddAddends objAddAddends = new AddAddends(data);
					responseServer = makeRequest(JsonConvert.SerializeObject(objAddAddends));

					if (responseServer.Equals("404"))
					{
						Console.WriteLine("400 Petición Incorrecta");
					}
					else if(responseServer.Equals("500"))
					{
						Console.WriteLine("500 - Error interno");
					}
					else
					{
						AddSum objAddSum = new AddSum(JsonConvert.DeserializeObject<AddSum>(responseServer).Sum);
						responseFinal = objAddSum.Sum;
					}
					break;

				case "Sub":
					SubOperators objSubOperators = new SubOperators(data);
					responseServer = makeRequest(JsonConvert.SerializeObject(objSubOperators));

					if (responseServer.Equals("404"))
					{
						Console.WriteLine("400 Petición Incorrecta");
					}
					else if (responseServer.Equals("500"))
					{
						Console.WriteLine("500 - Error interno");
					}
					else
					{
						SubDiference objSubDiference = new SubDiference(JsonConvert.DeserializeObject<SubDiference>(responseServer).Diference);
						responseFinal = objSubDiference.Diference;
					}
					break;
				case "Mult":
					MultFactors objMulFactors = new MultFactors(data);
					responseServer = makeRequest(JsonConvert.SerializeObject(objMulFactors));

					if (responseServer.Equals("404"))
					{
						Console.WriteLine("400 Petición Incorrecta");
					}
					else if (responseServer.Equals("500"))
					{
						Console.WriteLine("500 - Error interno");
					}
					else
					{
						MultProduct objMultProduct = new MultProduct(JsonConvert.DeserializeObject<MultProduct>(responseServer).Product);
						responseFinal = objMultProduct.Product;
					}
					break;
				case "Div":
					DivDividendDivisor objDivDividendDivisor = new DivDividendDivisor(Convert.ToString(data[0]), Convert.ToString(data[1]));
					responseServer = makeRequest(JsonConvert.SerializeObject(objDivDividendDivisor));

					if (responseServer.Equals("404"))
					{
						Console.WriteLine("400 Petición Incorrecta");
					}
					else if (responseServer.Equals("500"))
					{
						Console.WriteLine("500 - Error interno");
					}
					else
					{
						var objFinalDiv = JsonConvert.DeserializeObject<DivQuotientRemainder>(responseServer);
						responseFinal = "Cociente: " + objFinalDiv.Quotient + " Resto: " + objFinalDiv.Remainder;
					}
					break;
			}

			Console.WriteLine(responseFinal);
			//Console.WriteLine("---------PROGRAMA TERMINADO-------");
			//Console.ReadKey();
		}

		public static void displayMenu()
		{
			Console.Clear();
			Console.WriteLine("CALCULATOR SERVICE");
			Console.WriteLine("Operaciones:");
			Console.WriteLine("	0. Salir");
			Console.WriteLine("	1. Suma");
			Console.WriteLine("	2. Resta");
			Console.WriteLine("	3. Multiplicacion");
			Console.WriteLine("	4. Division");
			Console.Write("Respuesta: ");
		}//displayMenu

		public static bool getOption(string chosenOption)
		{
			bool response= true;
			chosenOption = chosenOption.ToUpper();

			switch (chosenOption)
			{
				case "0":
				case "SALIR":
					Console.WriteLine("---------PROGRAMA TERMINADO-------");
					Environment.Exit(-1);
					response = false;
					break;
				case "1":
				case "SUMA":
					Console.WriteLine("Sumando");
					action = "Add";
					//response = false;
					break;
				case "2":
				case "RESTA":
					Console.WriteLine("Restando");
					action = "Sub";
					//response = false;
					break;
				case "3":
				case "MULTIPLICACION":
					Console.WriteLine("MULTIPLICANDO");
					action = "Mult";
					//response = false;
					break;
				case "4":
				case "DIVISION":
					Console.WriteLine("DIVIENDO");
					action = "Div";
					//response = false;
					break;
				default:
					Console.WriteLine("Valor Invalido");
					action = "Default";
					response = true;
					break;
			}

			return response;

		}//getOption

		public static List<double> getData( int reply = 0)
		{
			List<double> data = new List<double>();
			Console.WriteLine("	 * Operando *	");
			Console.WriteLine("	 * Para salir escriba \"SALIR\". *	");
			bool stop = true;
			string operatorString;
			int operatorInt;
			int counter = 1;

			do
			{
				operatorString = Console.ReadLine();

				if (operatorString == String.Empty )
				{
					stop = false;
				}
				else if((operatorString.ToUpper()).Equals("SALIR")) {
					stop = false;
					data = null;
				}
				else if (Int32.TryParse(operatorString, out operatorInt))
				{
					data.Add(operatorInt);
					if(counter == reply) {
						stop = false;
					}
				}else {
					Console.WriteLine("Valor \"{0}\" es invalido.", operatorString);
				}
				counter++;
			} while (stop);

			return data;
		}//getData

		//Hacerle un try para controlar los errores posibles:

		public static string makeRequest(string objString) {
		var resultJSON = "";
			var httpWebRequest = (HttpWebRequest)WebRequest.Create(urlServer + action);
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Method = "POST";


			using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
			{
				//string json = JsonConvert.SerializeObject(objString);
				streamWriter.Write(objString);
				streamWriter.Close();
			}
			try
			{
				var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
				{
					resultJSON = streamReader.ReadToEnd();
				}
			}
			catch (System.Net.WebException ex)
			{
				var httpResponse = (HttpWebResponse)ex.Response;

				switch (httpResponse.StatusCode)
				{
					case HttpStatusCode.NotFound: // 404
						resultJSON = "404";
						break;

					case HttpStatusCode.InternalServerError: // 500
						resultJSON = "404";
						break;

					default:
						throw;
				}
			}

			return resultJSON;

		}//MakeRequest
	}//program
}
