using System.Collections.Generic;

namespace Client.Models
{
	public class AddAddends
	{
		public List<string> Addends {  get; set; }

		public AddAddends (List<string> data) {
			Addends = data;
		}

		public AddAddends() {
		}
		}//Sum

	}
