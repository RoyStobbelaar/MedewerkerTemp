using Xamarin.Forms;
using MCCForms.Models;
using System;
using System.Collections.Generic;

namespace MCCForms
{
	public partial class ItemEditPage : ContentPage
	{
		public ItemEditPage (Appointment existingAppointment)
		{
			//Just a test
			existingAppointment = new Appointment (new Employee ("Johan", "Bakker", 0291, 06222222, "jbakker@test.com", "medewerker"),
				new List<Collector> {new Collector ()},
				new List<Visitor> {new Visitor ("Henk", "DeBezoeker", "Bezoekr@test.com", 06221122, "Sony", "klant")},
				"Apeldoorn",
				new DateTime (2016, 11, 11, 10, 15, 30));
			
			existingAppointment.Comment = "Het is prachtig weer vandaag.";

			InitializeComponent ();

			BindingContext = new ItemEditViewModel (existingAppointment);

			if (existingAppointment != null) {
				Title = "Bestaande afspraak";
			}
		}
	}
}