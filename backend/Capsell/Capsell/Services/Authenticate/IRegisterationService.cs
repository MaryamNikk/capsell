using System;
using Capsell.Models.Authenticate;

namespace Capsell.Services.Authenticate
{
	public interface IRegisterationService
	{
        public Task<bool> CreateOrganizationService(OrganizationRegisterModel model, string userId);
    }
}

