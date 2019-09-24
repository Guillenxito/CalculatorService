using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client.Models
{
	public class Add
	{
		public List<double> Addends {  get; set; }

		public Add (List<double> data) {
			Addends = data;
		}

		public Add() {
		}
		}//Sum

	}
