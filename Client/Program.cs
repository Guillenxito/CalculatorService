using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Client.Models;

namespace Client
{
	class Program
	{
		protected static string urlServer = "http://localhost:50727/CalculatorS/";
		protected static string action = "";
		protected static string idHistorial = "";
		protected static bool operation = true;
		protected static string mainOption = "Default";
		static void Main(string[] args)
		{
			do {
				DisplayMainMenu();
				mainOption = Console.ReadLine();

				if (mainOption != "0")
				{
					GetMainOption();
					ShowResult();
				}

			} while (mainOption != "0" );

			ProgramCompleted();

		}//Main

		public static void ProgramCompleted() {
			Console.WriteLine(Environment.NewLine + "---------PROGRAMA FINALIZADO-------------");
			Console.ReadKey();
		}//ProgramCompleted

		public static void ShowResult()
		{
			string option = "Default";
			while (operation)
			{
				DisplayMenu();
				option = Console.ReadLine();
				operation = GetOption(option);

				if (operation)
				{
					if (!action.Equals("Default"))
					{
						List<string> data;
						switch (action)
						{
							case "Div":
								data = GetDataReply(2);
								break;
							case "Sqrt":
								data = GetDataReply(1);
								break;
							default:
								data = GetData();
								break;
						}
						if (data != null)
						{
							Console.WriteLine(Calculate(data));
							Console.ReadKey();
						}
					}
				}
			}//while
		}//ShowResult

		public static void GetMainOption() {
			switch (mainOption)
			{
				case "0":
				case "SALIR":
					mainOption = "0";
					break;
				case "1":
					idHistorial = GenerateId();
					mainOption = "Default";
					operation = true;
					break;
				case "2":
					idHistorial = "";
					mainOption = "Default";
					operation = true;
					break;
				case "3":
					Console.Clear();
					Console.Write("Escriba  \'Actual\' para usar el Id actual o escriba el ID del historial que quieres utilizar : ");
					action = "ExistJournal";
					string idHistorialUtility = Console.ReadLine();
					if (!idHistorialUtility.ToUpper().Equals("ACTUAL"))
					{
						idHistorial = idHistorialUtility;
					}
					Journal EJournal = new Journal(idHistorial);
					string resultfinal = MakeRequest(JsonConvert.SerializeObject(EJournal));
					if (resultfinal == "False")
					{
						Console.WriteLine("El ID no existe. No se guardara el historial.");
						idHistorial = "";
						Console.ReadKey();
						operation = false;
					}
					else
					{
						operation = true;
					}
					mainOption = "Default";
					break;
				case "4":
					Console.Clear();
					Console.Write("Escriba \'Actual\' para consultar el Historial actual o el ID del historial que quieres Consultar: ");
					action = "ExistJournal";
					string idHistorialQuery = Console.ReadLine();
					if (!idHistorialQuery.ToUpper().Equals("ACTUAL"))
					{
						idHistorial = idHistorialQuery;
					}
					if (!idHistorialQuery.Equals(""))
					{
						Journal EJournal4 = new Journal(idHistorial);
						string eResultfinal = MakeRequest(JsonConvert.SerializeObject(EJournal4));
						if (eResultfinal == "True")
						{
							action = "Journal";
							Journal Journal = new Journal(idHistorial);
							string resultfinalTwo = MakeRequest(JsonConvert.SerializeObject(Journal));
							string[] resultFinalArr = resultfinalTwo.Split('_');
							Console.Clear();
							foreach (string element in resultFinalArr)
							{
								Console.WriteLine(element);
							}
						}
						else
						{
							Console.Clear();
							idHistorial = "";
							operation = false;
							Console.WriteLine("Historial no existente.");
						}
					}
					else
					{
						operation = false;
						Console.WriteLine("Tienes que introducir un valor...");
					}

					Console.WriteLine(Environment.NewLine + "Pulsa Enter para continuar.");
					Console.ReadKey();
					mainOption = "Default";
					break;
				default:
					Console.WriteLine("Valor \"{0}\" es invalido.", mainOption);
					Console.ReadKey();
					mainOption = "Default";
					break;
			}
		}//GetMainOption
		
		public static string Add(List<string> data) {
			string responseFinal = null;
			string responseServer = "";
			AddAddends objAddAddends = new AddAddends(data);
			responseServer = MakeRequest(JsonConvert.SerializeObject(objAddAddends));

			AddSum objAddSum = new AddSum(JsonConvert.DeserializeObject<AddSum>(responseServer).Sum);

			if (objAddSum.Sum == null)
			{
				Error ErrorAdd = JsonConvert.DeserializeObject<Error>(responseServer);
				responseFinal = ErrorAdd.ErrorMessage;
			}
			else
			{
				responseFinal = objAddSum.Sum;
			}
			return responseFinal;
		}//Add

		public static string Sub(List<string> data)
		{
			string responseFinal = null;
			string responseServer = "";
			SubOperators objSubOperators = new SubOperators(data);
			responseServer = MakeRequest(JsonConvert.SerializeObject(objSubOperators));

			SubDiference objSubDiference = new SubDiference(JsonConvert.DeserializeObject<SubDiference>(responseServer).Diference);

			if (objSubDiference.Diference == null)
			{
				Error ErrorSub = JsonConvert.DeserializeObject<Error>(responseServer);
				responseFinal = ErrorSub.ErrorMessage;
			}
			else
			{
				responseFinal = objSubDiference.Diference;
			}
			return responseFinal;
		}//Sub

		public static string Mult(List<string> data)
		{
			string responseFinal = null;
			string responseServer = "";
			MultFactors objMulFactors = new MultFactors(data);
			responseServer = MakeRequest(JsonConvert.SerializeObject(objMulFactors));

			MultProduct objMultProduct = new MultProduct(JsonConvert.DeserializeObject<MultProduct>(responseServer).Product);

			if (objMultProduct.Product == null)
			{
				Error ErrorMult = JsonConvert.DeserializeObject<Error>(responseServer);
				responseFinal = ErrorMult.ErrorMessage;
			}
			else
			{
				responseFinal = objMultProduct.Product;
			}
			return responseFinal;
		}//Mult

		public static string Div(List<string> data)
		{
			string responseFinal = null;
			string responseServer = "";
			DivDividendDivisor objDivDividendDivisor = new DivDividendDivisor(Convert.ToString(data[0]), Convert.ToString(data[1]));
			responseServer = MakeRequest(JsonConvert.SerializeObject(objDivDividendDivisor));

			var objFinalDiv = JsonConvert.DeserializeObject<DivQuotientRemainder>(responseServer);

			if (objFinalDiv.Quotient == null && objFinalDiv.Remainder == null)
			{
				Error ErrorDiv = JsonConvert.DeserializeObject<Error>(responseServer);
				responseFinal = ErrorDiv.ErrorMessage;
			}
			else
			{
				responseFinal = "Cociente: " + objFinalDiv.Quotient + " Resto: " + objFinalDiv.Remainder;
			}
			return responseFinal;
		}//Div

		public static string Sqrt(List<string> data)
		{
			string responseFinal = null;
			string responseServer = "";
			SQRTnumber objSqrtNumber = new SQRTnumber(Convert.ToString(data[0]));
			responseServer = MakeRequest(JsonConvert.SerializeObject(objSqrtNumber));

			var objFinalSqrt = JsonConvert.DeserializeObject<SQRTsquare>(responseServer);
			if (objFinalSqrt.Square == null)
			{
				Error ErrorSqrt = JsonConvert.DeserializeObject<Error>(responseServer);
				responseFinal = ErrorSqrt.ErrorMessage;
			}
			else
			{
				responseFinal = objFinalSqrt.Square;
			}
			return responseFinal;
		}//Sqrt

		public static string Calculate(List<string> data) {
			string responseFinal = null;
			switch (action)
			{
				case "Add":
					responseFinal = Add(data);
					break;
				case "Sub":
					responseFinal = Sub(data);
					break;
				case "Mult":
					responseFinal = Mult(data);
					break;
				case "Div":
					responseFinal = Div(data);
					break;
				case "Sqrt":
					responseFinal = Sqrt(data);
					break;
			}
			return (Environment.NewLine + "Resultado: " + responseFinal + Environment.NewLine + "Pulse \'Enter\' para continuar.");

		}//Calculate

		public static void DisplayMenu()
		{
			Console.Clear();
			Console.Write("** CALCULATOR SERVICE **");
			if (idHistorial != "")
			{
				Console.WriteLine(" Numero de Historial: " + idHistorial);
			}
			Console.WriteLine("Operaciones:" + Environment.NewLine);
			Console.WriteLine("	0. Salir");
			Console.WriteLine("	1. Suma");
			Console.WriteLine("	2. Resta");
			Console.WriteLine("	3. Multiplicacion");
			Console.WriteLine("	4. Division");
			Console.WriteLine("	5. Raiz Cuadrada" + Environment.NewLine);
			Console.Write("Respuesta: ");
		}//DisplayMenu

		public static void DisplayMainMenu()
		{
			Console.Clear();
			Console.Write("** CALCULATOR SERVICE **");
			if (idHistorial != "")
			{
				Console.Write(" Numero de Historial: " + idHistorial);
			}
			Console.WriteLine(Environment.NewLine);
			Console.WriteLine("0. Salir" + Environment.NewLine);
			Console.WriteLine("1. Operar usando un NUEVO Historial" + Environment.NewLine);
			Console.WriteLine("2. Operar SIN historial" + Environment.NewLine);
			Console.WriteLine("3. Operar usando un historial ya existente." + Environment.NewLine);
			Console.WriteLine("4. Consultar Historial." + Environment.NewLine);
			Console.Write("Respuesta: ");
		}//DisplayMainMenu

		public static void DisplayInstructions() {
			Console.Clear();
			Console.WriteLine("		--INSTRUCCIONES ({0})--", GetAction());
			Console.WriteLine(" * Introduce cada operando uno por uno pulsando \'ENTER\'.");
			Console.WriteLine(" * Para realizar la operacion pulse \'ENTER\' sin escribir nada.");
			Console.WriteLine(" * Para cancelar la operacion escriba \'SALIR\'. 	");
			Console.WriteLine("		      ----" + Environment.NewLine);
		}//DisplayInstructions

		public static bool GetOption(string chosenOption)
		{
			bool response = true;
			chosenOption = chosenOption.ToUpper();

			switch (chosenOption)
			{
				case "0":
				case "SALIR":
					response = false;
					break;
				case "1":
				case "SUMA":
					action = "Add";
					break;
				case "2":
				case "RESTA":
					action = "Sub";
					break;
				case "3":
				case "MULTIPLICACION":
					action = "Mult";
					break;
				case "4":
				case "DIVISION":
					action = "Div";
					break;
				case "5":
				case "RAIZ":
				case "RAIZ CUADRADA":
					action = "Sqrt";
					break;
				default:
					Console.WriteLine("Valor \'{0}\' es Invalido.", chosenOption);
					Console.ReadKey();
					action = "Default";
					response = true;
					break;
			}

			return response;

		}//GetOption

		public static List<string> GetData()
		{
			List<string> data = new List<string>();
			Console.Clear();

			bool stop = true;
			string operatorString;
			double operatorInt;

			DisplayInstructions();

			while (stop == true)
			{
				Console.Write("  ");

				ShowData(data);

				operatorString = Console.ReadLine();
				if (operatorString == String.Empty && data.Count == 0)
				{
					Console.WriteLine("Campo vacio.");
					Console.ReadKey();
					DisplayInstructions();
				}
				else if (operatorString == String.Empty)
				{
					stop = false;
					DisplayInstructions();
					Console.WriteLine(String.Join(GetSymbolAction(), data));
				}
				else if ((operatorString.ToUpper()).Equals("SALIR"))
				{
					stop = false;
					data = null;
				}
				else if (operatorString.EndsWith(".") || operatorString.StartsWith(".") || operatorString.Contains(","))
				{
					Console.WriteLine("Valor \"{0}\" es invalido.", operatorString);
					Console.ReadKey();
					DisplayInstructions();
				}
				else if (double.TryParse(operatorString, out operatorInt))
				{
					data.Add(operatorString);
					DisplayInstructions();
				}
				else
				{
					Console.WriteLine("Valor \"{0}\" es invalido.", operatorString);
					Console.ReadKey();
					DisplayInstructions();
				}
			}

			return data;
		}//GetData

		public static List<string> GetDataReply(int reply) {
		//VARIABLES
			List<string> data = new List<string>();
			string operatorString;
			double operatorDouble;

			Console.Clear();

			for (int i = 0; i < reply; i++)
			{
				Console.Write("  ");
				DisplayInstructions();
				ShowData(data);

				if (action.Equals("Sqrt"))
				{
					Console.Write(GetSymbolAction());
				}

				operatorString = Console.ReadLine();

				if (operatorString == String.Empty)
				{
					Console.WriteLine("Campo vacio.");
					Console.ReadKey();
					DisplayInstructions();
					--i;
					Console.WriteLine(String.Join(GetSymbolAction(), data));
				}
				else if ((operatorString.ToUpper()).Equals("SALIR"))
				{
					data = null;
				}
				else if (operatorString.EndsWith(".") || operatorString.StartsWith(".") || operatorString.Contains(","))
				{
					Console.WriteLine("Valor \"{0}\" es invalido.", operatorString);
					Console.ReadKey();
					--i;
				}
				else if ((Double.TryParse(operatorString, out operatorDouble)))
				{
					DisplayInstructions();
					data.Add(operatorString);
				}
				else
				{
					Console.WriteLine("Valor \"{0}\" es invalido.", operatorString);
					Console.ReadKey();
					--i;
				}
			}//for
			if (data != null)
			{
				ShowDataFinal(data);
			}

			return data;
		}//GetDataReply

		public static void ShowData( List<string> data) {
			foreach (string ele in data)
			{
				if (action.Equals("Sqrt"))
				{
					Console.Write(GetSymbolAction() + ele);
				}
				else
				{
					Console.Write(ele + GetSymbolAction());
				}

			}
		}//ShowData

		public static void ShowDataFinal(List<string> data)
		{
			if (action.Equals("Sqrt"))
			{
				Console.WriteLine(GetSymbolAction() + data[0]);
			}
			else
			{
				Console.WriteLine(String.Join(GetSymbolAction(), data));
			}
		}//ShowDataFinal

		public static string MakeRequest(string objString) {
		var resultJSON = "";
			var httpWebRequest = (HttpWebRequest)WebRequest.Create(urlServer + action);
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Method = "POST";
			httpWebRequest.Headers["X-Evi-Tracking-Id"] = idHistorial;
			try
			{
				using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
				{
					streamWriter.Write(objString);
					streamWriter.Close();
				}

				var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
				{
					resultJSON = streamReader.ReadToEnd();
				}
			}
			catch (System.Net.WebException ex)
			{
				resultJSON = ex.ToString();
			}

			return resultJSON;

		}//MakeRequest

		public static string GetAction() {
			string actionFinal; 
		switch(action) {
				case "Add":
					actionFinal = "SUMA";
					break;
				case "Sub":
					actionFinal = "RESTA";
					break;
				case "Div":
					actionFinal = "DIVIDIR";
					break;
				case "Sqrt":
					actionFinal = "Raiz Cuadrada";
					break;
				case "Mult":
					actionFinal = "MULTIPLICACION";
					break;
				default:
					actionFinal = "";
					break;
			}

			return actionFinal;
		}//GetAction

		public static string GetSymbolAction()
		{
			string actionFinal;
			switch (action)
			{
				case "Add":
					actionFinal = " + ";
					break;
				case "Sub":
					actionFinal = " - ";
					break;
				case "Div":
					actionFinal = " / ";
					break;
				case "Sqrt":
					actionFinal = " √ ";
					break;
				case "Mult":
					actionFinal = " * ";
					break;
				default:
					actionFinal = " -_- ";
					break;
			}

			return actionFinal;
		}//GetSymbolAction

		public static string GenerateId()
		{
			Random rnd = new Random();
			string x = Convert.ToString(rnd.Next(0, 10000));
			x = x.PadLeft(5, '0');
			return x;
		}//GenerateId

	}//program
}
