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
	}
}