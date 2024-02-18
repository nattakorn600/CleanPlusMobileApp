using System;
using System.Collections.Generic;
using System.Text;

namespace cleanplus.Models
{
	public class AdminData
	{
		public int Id { get; set; }
		public int User_Id { get; set; }
		public string User_Name { get; set; }
		public string Date { get; set; }
		public string Time { get; set; }
		public int Price { get; set; }
		public string PriceFormat { get; set; }
		public string Hour { get; set; }
		public string Image { get; set; }
	}
	public class UserChat
	{
		public int User_Id { get; set; }
		public string User_Name { get; set; }
		public int Count { get; set; }
		public string LastMassage { get; set; }
	}
}
