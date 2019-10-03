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
		protected static string idHistorial = "";
		static void Main(string[] args)
		{



			//bool operation = false;
			string mainOption = "Default";
			bool operation = true;
			do {
				displayMainMenu();
				mainOption = Console.ReadLine();

				if (mainOption != "0")
				{
					switch (mainOption)
					{
						case "0":
						case "SALIR":
							mainOption = "0";
							//Environment.Exit(-1);
							break;
						case "1":
							idHistorial = generarId();
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
							Journal journal = new Journal(idHistorial);
							string resultfinal = makeRequest(JsonConvert.SerializeObject(journal));
							if(resultfinal == "") {
								Console.WriteLine("El ID no existe. No se guardara el historial.");
								idHistorial = "";
								Console.ReadKey();
								operation = false;
							}else {
								operation = true;
							}
							mainOption = "Default";
							break;
						case "4":
							Console.Clear();
							Console.Write("Escriba \'Actual\' para consultar el Historial actual o el ID del historial que quieres Consultar: ");
							action = "ExistJournal";
							string idHistorialQuery = Console.ReadLine();
							if(!idHistorialQuery.ToUpper().Equals("ACTUAL")) {
								idHistorial = idHistorialQuery;
							}
							if (!idHistorialQuery.Equals(""))
							{
								Journal eJournal = new Journal(idHistorial);
								string eResultfinal = makeRequest(JsonConvert.SerializeObject(eJournal));
								if (eResultfinal != "")
								{
									action = "Journal";
									Journal journalTwo = new Journal(idHistorial);
									string resultfinalTwo = makeRequest(JsonConvert.SerializeObject(journalTwo));
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
									Console.WriteLine("Historial no existente");
								}
							} else {
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

					//bool operation = false;
					
					string option = "Default";
					while(operation) {
						displayMenu();
						option = Console.ReadLine();
						operation = getOption(option);
						if (operation)
						{
							if (!action.Equals("Default"))
							{
								List<string> data;
								switch (action)
								{
									case "Div":
										data = getDataPro(2);
										break;
									case "Sqrt":
										data = getDataPro(1);
										break;
									default:
										data = getData();
										break;
								}
								if (data == null)
								{
									Console.WriteLine("OPERACION CANCELADA");
								}
								else
								{
									showResult(data);
									Console.ReadKey();
									//displayMainMenu();
								}
							}
						}
					}

				}
			} while (mainOption != "0" );

			Console.WriteLine(Environment.NewLine + "---------PROGRAMA FINALIZADO-------------");
			Console.ReadKey();

		}//Main

		public static string generarId() {
			Random rnd = new Random();
			string x = Convert.ToString(rnd.Next(0, 10000));
			x = x.PadLeft(5, '0');
			return x;
		}

		public static void displayMainMenu() {
			Console.Clear();
			Console.Write("** CALCULATOR SERVICE **");
			if (idHistorial != "") {
				Console.Write(" Numero de Historial: " + idHistorial);
			}
			Console.WriteLine(Environment.NewLine);
			Console.WriteLine("0. Salir" + Environment.NewLine);
			Console.WriteLine("1. Operar usando un NUEVO Historial"+ Environment.NewLine);
			Console.WriteLine("2. Operar SIN historial" + Environment.NewLine);
			Console.WriteLine("3. Operar usando un historial ya existente."+ Environment.NewLine);
			Console.WriteLine("4. Consultar Historial." + Environment.NewLine);
			Console.Write("Respuesta: ");
		}

		public static void showResult(List<string> data) {
			string responseFinal = null;
			string responseServer = "";
			switch (action)
			{
				case "Add":
					AddAddends objAddAddends = new AddAddends(data);
					responseServer = makeRequest(JsonConvert.SerializeObject(objAddAddends));

					AddSum objAddSum = new AddSum(JsonConvert.DeserializeObject<AddSum>(responseServer).Sum);

					if (objAddSum.Sum == null){
						Error ErrorAdd = JsonConvert.DeserializeObject<Error>(responseServer);
						responseFinal = ErrorAdd.ErrorMessage;
					}else {
						responseFinal = objAddSum.Sum;
					}
					break;

				case "Sub":
					SubOperators objSubOperators = new SubOperators(data);
					responseServer = makeRequest(JsonConvert.SerializeObject(objSubOperators));

					SubDiference objSubDiference = new SubDiference(JsonConvert.DeserializeObject<SubDiference>(responseServer).Diference);

					if(objSubDiference.Diference == null) {
						Error ErrorSub = JsonConvert.DeserializeObject<Error>(responseServer);
						responseFinal = ErrorSub.ErrorMessage;
					}else {
						responseFinal = objSubDiference.Diference;
					}
					break;

				case "Mult":
					MultFactors objMulFactors = new MultFactors(data);
					responseServer = makeRequest(JsonConvert.SerializeObject(objMulFactors));

					MultProduct objMultProduct = new MultProduct(JsonConvert.DeserializeObject<MultProduct>(responseServer).Product);

					if (objMultProduct.Product == null){
						Error ErrorMult = JsonConvert.DeserializeObject<Error>(responseServer);
						responseFinal = ErrorMult.ErrorMessage;
					} else {
						responseFinal = objMultProduct.Product;
					}
					break;

				case "Div":
					DivDividendDivisor objDivDividendDivisor = new DivDividendDivisor(Convert.ToString(data[0]), Convert.ToString(data[1]));
					responseServer = makeRequest(JsonConvert.SerializeObject(objDivDividendDivisor));

					var objFinalDiv = JsonConvert.DeserializeObject<DivQuotientRemainder>(responseServer);

					if (objFinalDiv.Quotient == null && objFinalDiv.Remainder == null) {
						Error ErrorDiv = JsonConvert.DeserializeObject<Error>(responseServer);
						responseFinal = ErrorDiv.ErrorMessage;
					} else {
						responseFinal = "Cociente: " + objFinalDiv.Quotient + " Resto: " + objFinalDiv.Remainder;
					}
					break;
				case "Sqrt":
					SQRTnumber objSqrtNumber = new SQRTnumber(Convert.ToString(data[0]));
					responseServer = makeRequest(JsonConvert.SerializeObject(objSqrtNumber));

					var objFinalSqrt = JsonConvert.DeserializeObject<SQRTsquare>(responseServer);
					if(objFinalSqrt.Square == null) {
						Error ErrorSqrt = JsonConvert.DeserializeObject<Error>(responseServer);
						responseFinal = ErrorSqrt.ErrorMessage;
					}else {
						responseFinal = objFinalSqrt.Square;
					}
					break;
			}
			Console.Write(Environment.NewLine + "Resultado: " + responseFinal);
			Console.Write(Environment.NewLine + "Pulse \'Enter\' para continuar.");
			Console.ReadKey();

		}

		public static void displayMenu()
		{
			Console.Clear();
			Console.WriteLine("** CALCULATOR SERVICE **");
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
		}//displayMenu

		public static bool getOption(string chosenOption)
		{
			bool response= true;
			chosenOption = chosenOption.ToUpper();

			switch (chosenOption)
			{
				case "0":
				case "SALIR":
					response = false;
					break;
				case "1":
				case "SUMA":
					Console.WriteLine("SUMANDO");
					action = "Add";
					//response = false;
					break;
				case "2":
				case "RESTA":
					Console.WriteLine("RESTANDO");
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
				case "5":
				case "RAIZ":
				case "RAIZ CUADRADA":
					Console.WriteLine("REALIZANDO RAIZ CUADRADA");
					action = "Sqrt";
					//response = false;
					break;
				default:
					Console.WriteLine("Valor \'{0}\' es Invalido.", chosenOption);
					Console.ReadKey();
					action = "Default";
					response = true;
					break;
			}

			return response;

		}//getOption

		public static List<string> getData()
		{
			List<string> data = new List<string>();
			Console.Clear();
			
			bool stop = true;
			string operatorString;
			double operatorInt;

			displayInstructions();

			while (stop == true) {
				Console.Write("  ");

				foreach(string ele in data) {
					Console.Write(ele + getSymbolAction());
				}

				operatorString = Console.ReadLine();

				if (operatorString == String.Empty)
				{
					stop = false;
					displayInstructions();
					Console.WriteLine(String.Join(getSymbolAction(),data));
				}
				else if ((operatorString.ToUpper()).Equals("SALIR"))
				{
					stop = false;
					data = null;
				}
				else if (operatorString.StartsWith("."))
				{
					Console.WriteLine("Valor \"{0}\" es invalido.", operatorString);
					Console.ReadKey();
					displayInstructions();
				}
				else if (double.TryParse(operatorString, out operatorInt))
				{
					data.Add(operatorString);
					displayInstructions();
				}
				else
				{
					Console.WriteLine("Valor \"{0}\" es invalido.", operatorString);
					Console.ReadKey();
					displayInstructions();
				}
			}

			return data;
		}//getData

		public static void displayInstructions() {
			Console.Clear();
			Console.WriteLine("		--INSTRUCCIONES ({0})--", getAction());
			Console.WriteLine(" * Introduce cada operando uno por uno pulsando \'ENTER\'.");
			Console.WriteLine(" * Para realizar la operacion pulse \'ENTER\' sin escribir nada.");
			Console.WriteLine(" * Para cancelar la operacion escriba \'SALIR\'. 	");
			Console.WriteLine("		      ----" + Environment.NewLine);
		}

		public static List<string> getDataPro(int reply = 0)
		{
			List<string> data = new List<string>();
			string operatorString;
			double operatorDouble;
	
			for (int i = 0; i < reply;i++) {
				Console.Write("  "); 
				displayInstructions();
				foreach (string ele in data)
				{
					if (action.Equals("Sqrt"))
					{
						Console.Write(getSymbolAction() + ele);
					}
					else
					{
						Console.Write(ele + getSymbolAction() );
					}
					
				}

				if (action.Equals("Sqrt"))
				{
					Console.Write(getSymbolAction());
				}

				operatorString = Console.ReadLine();

				if (operatorString == String.Empty) {
					displayInstructions();
					--i;
					Console.WriteLine(String.Join(getSymbolAction(), data));
				} else if ((operatorString.ToUpper()).Equals("SALIR")) {
					i = 1 + reply;
					data = null;
				}else if (operatorString.StartsWith(".")){
					Console.WriteLine("Valor \"{0}\" es invalido.", operatorString);
					Console.ReadKey();
					--i;
				}
				else if ((Double.TryParse(operatorString, out operatorDouble))) {
					displayInstructions();
					data.Add(operatorString);
				} else {
					Console.WriteLine("Valor \"{0}\" es invalido.", operatorString);
					Console.ReadKey();
					--i;
				}
			}//for
			if(data!= null){
				if (action.Equals("Sqrt"))
				{
					Console.WriteLine(getSymbolAction() + data[0]);
				}
				else
				{
					Console.WriteLine(String.Join(getSymbolAction(), data));
				}
			}

			return data;
		}//getData

		public static string makeRequest(string objString) {
		var resultJSON = "";
			var httpWebRequest = (HttpWebRequest)WebRequest.Create(urlServer + action);
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Method = "POST";
			httpWebRequest.Headers["X-Evi-Tracking-Id"] = idHistorial;
			
			using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
			{
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
				resultJSON = ex.ToString();
			}

			return resultJSON;

		}//MakeRequest

		public static string getAction() {
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
		}

		public static string getSymbolAction()
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
		}
	}//program
}
