using System;
using Capsell.Models.Authenticate;

namespace Capsell.Repositories.Authenticate
{
	public interface IRegisterationRepo
	{
        public Task<bool> CreateOrganization(OrganizationRegisterModel model, string userId);
    }
}

