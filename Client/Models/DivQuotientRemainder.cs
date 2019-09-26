using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client.Models
{
	public class DivQuotientRemainder
	{
		public string Quotient { set; get; }
		public string Remainder { set; get; }

		public DivQuotientRemainder() { }
		public DivQuotientRemainder(string quotient,string remainder) {
			Quotient = quotient;
			Remainder = remainder;
		}
	}
}