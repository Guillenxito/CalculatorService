﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalculatorS.Models
{
	public class AddAddends
	{
		public List<string> Addends { get; set; }

		public AddAddends (List<string> data) {
			Addends = data;
		}

		public AddAddends() {
		}
		

	}
}