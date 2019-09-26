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
		protected string urlServer = "http://localhost:50727/CalculatorS/Saluda";
		protected static string action = "";

		static void Main(string[] args)
		{

			bool operation = false;
			string option = "-1";

			do
			{
				displayMenu();
				option = Console.ReadLine();
				operation = getOption(option);
				List<double> data = getData();

				if (data == null)
				{
					Console.WriteLine("OPERACION CANCELADA");
				}
				else
				{
					showResult(data);
					Console.ReadKey();
				}
			} while (operation);

			Console.ReadKey();




		}//Main

		public static  void showResult(List<double> data) {
			string responseFinal = null;

			switch (action)
			{
				case "Add":
					AddAddends objAddAddends = new AddAddends(data);
					string responseServer = makeRequest(JsonConvert.SerializeObject(objAddAddends));
					AddSum objAddSum = new AddSum(JsonConvert.DeserializeObject<AddSum>(responseServer).Sum);
					responseFinal = objAddSum.Sum;
					break;
				case "Sub":
					//Sub objSub = new Sub(data);
					//respuestaTemporal = makeRequest(JsonConvert.SerializeObject(objSub));
					break;
			}

			switch (action) {

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
			Console.Write("Respuesta: ");
		}//displayMenu

		public static bool getOption(string chosenOption)
		{
			bool response= true;
			chosenOption = chosenOption.ToUpper();

			switch (chosenOption)
			{
				case "0":
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
				default:
					Console.WriteLine("Valor Invalido");
					response = true;
					break;
			}

			return response;

		}//getOption

		public static List<double> getData()
		{
			List<double> data = new List<double>();
			Console.WriteLine("	 * Operando *	");
			Console.WriteLine("	 * Para salir escriba \"SALIR\". *	");
			bool stop = true;
			string operatorString;
			int operatorInt;

			do
			{
				operatorString = Console.ReadLine();

				if (operatorString == String.Empty)
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
				}else {
					Console.WriteLine("Valor \"{0}\" es invalido.", operatorString);
				}
			} while (stop);

			return data;
		}//getData

		//Hacerle un try para controlar los errores posibles:

		public static string makeRequest(string objString) {
		var resultJSON = "";
			var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:50727/CalculatorS/" + action);
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Method = "POST";


			using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
			{
				//string json = JsonConvert.SerializeObject(objString);
				streamWriter.Write(objString);
				streamWriter.Close();
			}

			var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				
			using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
			{
				resultJSON = streamReader.ReadToEnd();
			}
			return resultJSON;

		}//MakeRequest
	}//program
}
