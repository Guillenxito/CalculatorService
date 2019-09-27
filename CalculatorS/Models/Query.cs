using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CalculatorS.Models
{
	public class Query
	{
		public List<string> Operations = new List<string>() ;
		public Query() { 
		}

		public Query(List<string> listOperations) {
			Operations = listOperations;
		}

		public void setOperation(QueryOperation operation) {
			Operations.Add(JsonConvert.SerializeObject(operation));	
		}

		public void saveOperations(QueryOperation operation) {
			StreamWriter escribir = new StreamWriter("C:\\dev\\CalculatorS\\CalculatorS\\Traking");
			escribir.WriteLine(JsonConvert.SerializeObject(operation)); 
		}

	}
}