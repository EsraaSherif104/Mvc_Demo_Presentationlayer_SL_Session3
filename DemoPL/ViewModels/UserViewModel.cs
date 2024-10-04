using System.Collections;
using System.Collections.Generic;

namespace DemoPL.ViewModels
{
	public class UserViewModel
	{
        public string id { get; set; }
		public string Fname { get; set; }
		public string Lname { get; set; }

	    public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public IEnumerable<string> Roles { get; set; }




	}
}
