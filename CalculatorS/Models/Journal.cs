using System;
using System.IO;

namespace CalculatorS.Models
{
	public class Journal
	{
		private const string directoryPath= "Tracking\\";
		public string Id { get; set; }

		public Journal() { }
		public Journal(string id) {
			Id = id;
		}

		public void SaveJournal(string operation)
		{
			string mainPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @directoryPath + Id);

			if(!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @directoryPath))) {
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
				writer.WriteLine("_"+operation+"_"); 

				writer.Close();
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
			string text = "";

			try {
				using (StreamReader file = new StreamReader(mainPath))
				{
					while ((line = file.ReadLine()) != null)
					{
						text = text + line + "\b";
					}

				}
				return text;
			} catch {
				return "Error al cargar el archivo.";	
			}
			
		}//ReadJournal

	}
}