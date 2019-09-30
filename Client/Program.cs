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
							Console.WriteLine("Generando nuevo historial o usar uno existente");
							idHistorial = generarId();
							mainOption = "Default";
							operation = true;
							break;
						case "2":
							Console.WriteLine("Quitando Historial");
							idHistorial = "";
							mainOption = "Default";
							operation = true;
							break;
						case "3":
							Console.Write("Escriba  \'Actual\' para usar el Id actual o escriba el ID del historial que quieres utilizar : ");
							action = "ExistJounal";
							string idHistorialUtility = Console.ReadLine();
							if (!idHistorialUtility.ToUpper().Equals("ACTUAL"))
							{
								idHistorial = idHistorialUtility;
							}
							Journal journal = new Journal(idHistorial);
							string resultfinal = makeRequest(JsonConvert.SerializeObject(journal));
							if(resultfinal == null) {
								Console.WriteLine("El ID no existe. No se guardara el historial.");
								idHistorial = "";
								operation = false;
							}else {
								operation = true;
							}
							mainOption = "Default";
							break;
						case "4":
							Console.Write("Escriba \'Actual\' para consultar el Historial actual o el ID del historial que quieres Consultar: ");
							action = "ExistJounal";
							string idHistorialQuery = Console.ReadLine();
							if(!idHistorialQuery.ToUpper().Equals("ACTUAL")) {
								idHistorial = idHistorialQuery;
							}
							Journal eJournal = new Journal(idHistorial);
							string eResultfinal = makeRequest(JsonConvert.SerializeObject(eJournal));
							if (eResultfinal != "")
							{
								action = "Jounal";
								Journal journalTwo = new Journal(idHistorial);
								string resultfinalTwo = makeRequest(JsonConvert.SerializeObject(journalTwo));
								string[] resultFinalArr = resultfinalTwo.Split('_');
								Console.Clear();
								foreach (string element in resultFinalArr) {
									Console.WriteLine(element);
								}
							}else {
								Console.Clear();
								idHistorial = "";
								operation = false;
								Console.WriteLine("Historial no existente");
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
								List<double> data;
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
								}
							}
						}
					}

					//do
					//{
					//	displayMenu();
					//	option = Console.ReadLine();
					//	operation = getOption(option);
					//	if (operation)
					//	{
					//		if (!action.Equals("Default"))
					//		{
					//			List<double> data;
					//			switch (action)
					//			{
					//				case "Div":
					//					data = getDataPro(2);
					//					break;
					//				case "Sqrt":
					//					data = getDataPro(1);
					//					break;
					//				default:
					//					data = getData();
					//					break;
					//			}


					//			if (data == null)
					//			{
					//				Console.WriteLine("OPERACION CANCELADA");
					//			}
					//			else
					//			{
					//				showResult(data);
					//				Console.ReadKey();
					//			}
					//		}
					//	}
					//} while (operation);	
				}
			} while (mainOption != "0" );

			Console.WriteLine("---------PROGRAMA FINALIZADO-------------");
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

		

		public static  void showResult(List<double> data) {
			string responseFinal = null;
			string responseServer = "";
			switch (action)
			{
				case "Add":
					AddAddends objAddAddends = new AddAddends(data);
					responseServer = makeRequest(JsonConvert.SerializeObject(objAddAddends));
					try {
						AddSum objAddSum = new AddSum(JsonConvert.DeserializeObject<AddSum>(responseServer).Sum);
						responseFinal = objAddSum.Sum;
					} catch (Exception) {
						Error objAddSum = JsonConvert.DeserializeObject<Error>(responseServer);
						responseFinal = objAddSum.ErrorMessage;
					}
					break;

				case "Sub":
					SubOperators objSubOperators = new SubOperators(data);
					responseServer = makeRequest(JsonConvert.SerializeObject(objSubOperators));
					try{
						SubDiference objSubDiference = new SubDiference(JsonConvert.DeserializeObject<SubDiference>(responseServer).Diference);
						responseFinal = objSubDiference.Diference;
					} catch (Exception) {
						Error objAddSum = JsonConvert.DeserializeObject<Error>(responseServer);
						responseFinal = objAddSum.ErrorMessage;
					}
					break;

				case "Mult":
					MultFactors objMulFactors = new MultFactors(data);
					responseServer = makeRequest(JsonConvert.SerializeObject(objMulFactors));
					try{
						MultProduct objMultProduct = new MultProduct(JsonConvert.DeserializeObject<MultProduct>(responseServer).Product);
						responseFinal = objMultProduct.Product;
					}catch (Exception){
						Error objAddSum = JsonConvert.DeserializeObject<Error>(responseServer);
						responseFinal = objAddSum.ErrorMessage;
					}
					break;

				case "Div":
					DivDividendDivisor objDivDividendDivisor = new DivDividendDivisor(Convert.ToString(data[0]), Convert.ToString(data[1]));
					responseServer = makeRequest(JsonConvert.SerializeObject(objDivDividendDivisor));
					try{
						var objFinalDiv = JsonConvert.DeserializeObject<DivQuotientRemainder>(responseServer);
						responseFinal = "Cociente: " + objFinalDiv.Quotient + " Resto: " + objFinalDiv.Remainder;
					} catch (Exception) {
						Error objAddSum = JsonConvert.DeserializeObject<Error>(responseServer);
						responseFinal = objAddSum.ErrorMessage;
					}
					break;
				case "Sqrt":
					SQRTnumber objSqrtNumber = new SQRTnumber(Convert.ToString(data[0]));
					responseServer = makeRequest(JsonConvert.SerializeObject(objSqrtNumber));

					try
					{
						var objFinalDiv = JsonConvert.DeserializeObject<SQRTsquare>(responseServer);
						responseFinal = objFinalDiv.Square;
					}
					catch (Exception)
					{
						Error objAddSum = JsonConvert.DeserializeObject<Error>(responseServer);
						responseFinal = objAddSum.ErrorMessage;
					}
					break;
			}

			Console.WriteLine(responseFinal);
			//Console.WriteLine("---------PROGRAMA TERMINADO-------");
			Console.ReadKey();
		}

		public static void displayMenu()
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
			int counter = 0;

			while (stop == true) {
				//Console.WriteLine("COUNTER:"+ Convert.ToInt32(counter) + "  Reply:" + Convert.ToInt32(reply));
				operatorString = Console.ReadLine();

				if (operatorString == String.Empty)
				{
					stop = false;
				}
				else if ((operatorString.ToUpper()).Equals("SALIR"))
				{
					stop = false;
					data = null;
				}
				else if (Int32.TryParse(operatorString, out operatorInt))
				{
					data.Add(operatorInt);
				}
				else
				{
					Console.WriteLine("Valor \"{0}\" es invalido.", operatorString);
				}
				++counter;
			}

			return data;
		}//getData

		public static List<double> getDataPro(int reply = 0)
		{
			List<double> data = new List<double>();
			Console.WriteLine("	 * Operando *	");
			Console.WriteLine("	 * Para salir escriba \"SALIR\". *	");
			string operatorString;
			int operatorInt;

			for(int i = 0; i < reply;i++) {
				Console.Write("-> ");
				operatorString = Console.ReadLine();
				if (operatorString == String.Empty) {
					--i;
				} else if ((operatorString.ToUpper()).Equals("SALIR")) {
					i = 1 + reply;
					data = null;
				} else if ((Int32.TryParse(operatorString, out operatorInt))) {
					data.Add(operatorInt);
				} else {
					Console.WriteLine("Valor \"{0}\" es invalido.", operatorString);
				}
			}
			return data;
		}//getData



		//Hacerle un try para controlar los errores posibles:

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
