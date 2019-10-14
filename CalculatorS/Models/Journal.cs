using System;
using System.IO;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace CalculatorS.Models
{
	public class Journal
	{
		private static object locker = new object();
		private const string directoryPath= "Tracking\\";
		public string Id { get; set; }

		public Journal() { }
		public Journal(string id) {
			Id = id;
		}

		public void SaveJournal(string operation)
		{
			lock (locker)
			{
				string mainPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @directoryPath + Id);

				if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @directoryPath)))
				{
					DirectoryInfo di = Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @directoryPath));
				}

				if (!File.Exists(mainPath))
				{
					using (StreamWriter mylogs = File.AppendText(mainPath))
					{
						mylogs.WriteLine("_** Operations History **_");
						mylogs.Close();
					}
				}
				using (StreamWriter writer = new StreamWriter(mainPath, true))
				{
					writer.WriteLine(operation);
					writer.Close();
				}
			}
		}//SaveJournal

		public bool ExistJournal() {
			string mainPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @directoryPath + Id);

			if ( File.Exists(mainPath)) {
				return true;
			}
			return false;
		}//ExistJournal

		public string ReadJournal() {
				string mainPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @directoryPath + Id);

				string line = "";
				try
				{
				var operationsList = new List<Query>();
				var counter = 0;
					using (StreamReader file = new StreamReader(mainPath))
					{
						while ((line = file.ReadLine()) != null)
						{
						if(counter != 0) {
							var operationDeserialize = JsonConvert.DeserializeObject<Query>(line);
							operationsList.Add(operationDeserialize);
						}
						counter++;
						}
					}

					var operationsResponse = new Operations(operationsList);
					var x = JsonConvert.SerializeObject(operationsResponse);

					return x;
				}
				catch (Exception ex)
				{
					return "Error al cargar el archivo.";
				}			
		}//ReadJournal

	}
}