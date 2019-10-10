using System;
using System.IO;

namespace CalculatorS.Models
{
	public class Journal
	{
		public string Id { get; set; }

		public Journal() { }
		public Journal(string id) {
			Id = id;
		}

		public void SaveJournal(string operation)
		{
			string mainPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Tracking\" + Id);

			if(!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Tracking\"))) {
				DirectoryInfo di = Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Tracking\"));
			}

			if (!File.Exists(mainPath))
			{
				using (StreamWriter mylogs = File.AppendText(mainPath)) 
				{
					mylogs.WriteLine("_** Operations History **_");

					mylogs.Close();
				}
			}
			using (StreamWriter filew = new StreamWriter(mainPath, true))
			{
				filew.WriteLine("_"+operation+"_"); 

				filew.Close();
			}
		}//SaveJournal

		public bool ExistJournal() {
			string mainPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Tracking\" + Id);
			if ( File.Exists(mainPath)) {
				return true;
			}
			return false;
		}//ExistJournal

		public string ReadJournal() {

			string mainPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Tracking\" + Id);

			string line = "";
			string text = "";
			using (StreamReader file = new StreamReader(mainPath))
			{
				while ((line = file.ReadLine()) != null)
				{
					text = text + line + "\b";
				}

			}
			return text;
		}//ReadJournal

	}
}