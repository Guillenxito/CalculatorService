using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client.Models
{
	public class AddAddends
	{
		public List<double> Addends {  get; set; }

		public AddAddends (List<double> data) {
			Addends = data;
		}

		public AddAddends() {
		}
		}//Sum

	}
