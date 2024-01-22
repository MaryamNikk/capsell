using System;
using Capsell.Controllers;
using Capsell.Models.Authenticate;
using Capsell.Repositories.Authenticate;

namespace Capsell.Services.Authenticate
{
	public class RegisterationService : IRegisterationService
	{
		private readonly IRegisterationRepo _repo;
        private readonly ILogger<RegisterationService> _logger;

        public RegisterationService(IRegisterationRepo repo, ILogger<RegisterationService> logger)
		{
			_repo = repo;
			_logger = logger;
		}


		public async Task<bool> CreateOrganizationService(OrganizationRegisterModel model, string userId)
		{

			try
			{
				var isOrgCreateSuccessfuly = await _repo.CreateOrganization(model, userId);
				return isOrgCreateSuccessfuly;

			}
			catch (Exception ex)
			{
				_logger.LogInformation($"exception occured in CreateOrganizationService: {ex}");

				return false;
			}
		}

    }
}

