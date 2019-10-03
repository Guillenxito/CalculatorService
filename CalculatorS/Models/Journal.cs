using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CalculatorS.Models
{
	public class Journal
	{
		public string Id { get; set; }

		public Journal() { }
		public Journal(string id) {
			Id = id;
		}

		public void saveJournal(string operation)
		{
			string mainPath = "C:\\dev\\CalculatorS\\CalculatorS\\Tracking\\" + Id;
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
		}//saveJournal

		public bool existJournal() {
			string mainPath = "C:\\dev\\CalculatorS\\CalculatorS\\Traking\\" + Id;
			if ( File.Exists(mainPath)) {
				return true;
			}
			return false;
		}

		public string readJournal() {

			string mainPath = "C:\\dev\\CalculatorS\\CalculatorS\\Traking\\" + Id;

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
		}
				
	}
}