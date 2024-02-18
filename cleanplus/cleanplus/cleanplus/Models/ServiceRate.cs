using System;
using System.Collections.Generic;
using System.Text;

namespace cleanplus.Models
{
	public class ServiceRate
	{
		public int Min { get; set; }
		public int Max { get; set; }
		public int Hour { get; set; }
		public int Empoyee { get; set; }
	}

	public class ServicePayment
	{
		public int Id { get; set; }
		public List<Empos> Emp { get; set; }
		public string TarangMate { get; set; }
		public double Price { get; set; }
		public int Empoyee { get; set; }
		public string Date { get; set; }
		public string Time { get; set; }
		public string Name { get; set; }
		public string Phone { get; set; }
		public int Hour { get; set; }
		public string Star1 { get; set; }
		public string Star2 { get; set; }
		public string Star3 { get; set; }
		public string Star4 { get; set; }
		public string Star5 { get; set; }
		public double Latitude { get; set; }
		public string Image { get; set; }
		public double Longitude { get; set; }
		public string Address { get; set; }
		public string Note { get; set; }
		public string Payment { get; set; }
		public string Confirm { get; set; }
		public int Finish { get; set; }
		public string CountStar { get; set; }
		public string Comment { get; set; }
		public bool VisableComment { get; set; }
		public int Noti_Id { get; set; }
		public int Booking_Id { get; set; }
		public string Title { get; set; }
		public string User_Name { get; set; }
		public string Emp_Name { get; set; }
		public string Status { get; set; }
	}
	public class Empos
	{
		public int Id { get; set; }
	}
}
