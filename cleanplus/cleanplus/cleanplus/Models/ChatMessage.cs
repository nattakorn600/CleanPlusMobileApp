using System;
using System.Collections.Generic;
using System.Text;

namespace cleanplus.Models
{
	public class ChatMessage
	{
		public int Id { get; set; }
		public string User { get; set; }
		public string Message { get; set; }
		public string Status { get; set; }
	}
}
