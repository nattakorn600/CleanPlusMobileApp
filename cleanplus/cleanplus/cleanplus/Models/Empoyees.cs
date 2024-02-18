using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace cleanplus.Models
{
	public class Empoyees
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Position { get; set; }
		public string Star1 { get; set; }
		public string Star2 { get; set; }
		public string Star3 { get; set; }
		public string Star4 { get; set; }
		public string Star5 { get; set; }
		public bool CommentIsVisible { get; set; }
		public string Image { get; set; }
		public double Rotate { get; set; }
		public List<Comments> CommList { get; set; }
		public string CerImage { get; set; }
		public string CommNumber { get; set; }
	}
	public class Comments
	{
		public string Comment { get; set; }
	}
	public class EmpoyeeCon
	{
		public int Id { get; set; }
		public string TarangMate { get; set; }
		public double Price { get; set; }
		public string DetailPrice { get; set; }
		public string HomePrice { get; set; }
		public int Empoyee { get; set; }
		public string Date { get; set; }
		public string Time { get; set; }
		public int Hour { get; set; }
		public int Finish { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public string Address { get; set; }
		public string Note { get; set; }
		public User User { get; set; }
		public bool VisableWait { get; set; }
		public bool VisableWork { get; set; }
	}
	public class User
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Phone { get; set; }
		public string Address { get; set; }
	}
}
