using CalculatorS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CalculatorS.Controllers
{
    public class CalculatorSController : Controller
    {
        //// GET: CalculatorS
        //public ActionResult Index()
        //{
        //    return View();
        //}

		[HttpPost]
		public double Add (Add numbersForAdd)
		{
			var result = numbersForAdd.Sum();
			return result;
		}

		[HttpPost]
		public double Sub (Sub numbersForSubtract)
		{
			var result = numbersForSubtract.Subtract();
			return result;
		}

	}
}