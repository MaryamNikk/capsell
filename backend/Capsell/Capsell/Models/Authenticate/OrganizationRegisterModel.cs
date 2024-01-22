using System;
namespace Capsell.Models.Authenticate
{
	public class OrganizationRegisterModel : RegisterModel
	{
		public string Role { get; set; }
		public long LicenseNumber { get; set; }
	}
}

