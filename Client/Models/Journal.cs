using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Client.Models
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
			string mainPath = "C:\\dev\\CalculatorS\\CalculatorS\\Traking\\" + Id;
			if (!File.Exists(mainPath))
			{
				using (StreamWriter mylogs = File.AppendText(mainPath)) 
				{
					mylogs.WriteLine("** Operations History **");

					mylogs.Close();
				}
			}
			using (StreamWriter filew = new StreamWriter(mainPath, true))
			{
				filew.WriteLine(operation); 

				filew.Close();
			}
		}//saveJournal

			

		
	}
}