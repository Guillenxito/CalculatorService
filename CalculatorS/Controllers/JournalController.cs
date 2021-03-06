﻿using System;
using System.Web.Mvc;

using Newtonsoft.Json;

using CalculatorS.Models;


namespace CalculatorS.Controllers
{
    public class JournalController : Controller
    {

		[HttpGet]
		public bool ExistJournal(Journal IdForJournal)
		{
			if (IdForJournal == null || IdForJournal.Id == null)
			{
				return false;
			}

			return IdForJournal.ExistJournal();

		}//ExistJournal

		[HttpGet]
		public string Journal(Journal IdForJournal)
		{
			try
			{
				if ( IdForJournal == null || IdForJournal.Id == null)
				{
					Error objectFinalError = new Error();
					objectFinalError.Error400();
					return JsonConvert.SerializeObject(objectFinalError);
				}

				if (IdForJournal.ExistJournal())
				{
					var x = IdForJournal.ReadJournal();
					return IdForJournal.ReadJournal();
				}
				return null;
			}
			catch (Exception ex)
			{
				Error objectFinalError = new Error();
				objectFinalError.Error500(ex.StackTrace);
				return JsonConvert.SerializeObject(objectFinalError);
			}
		}//Journal
	}
}