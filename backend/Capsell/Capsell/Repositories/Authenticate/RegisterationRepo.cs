using System;
using Capsell.DataProvide;
using Capsell.Models.Authenticate;
using Capsell.Services.Authenticate;

namespace Capsell.Repositories.Authenticate
{
	public class RegisterationRepo : IRegisterationRepo
	{
        private readonly ILogger<RegisterationRepo> _logger;
        private readonly CapsellDbContext _context;

        public RegisterationRepo(CapsellDbContext context, ILogger<RegisterationRepo> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> CreateOrganization(OrganizationRegisterModel model, string userId)
        {
            try
            {
                if (model.Role == "shop")
                {
                    await AddShopToDb(model, userId);

                    return true;
                }
                await AddCompanyToDb(model, userId);

                return true;

            }
            catch(Exception ex)
            {
                _logger.LogInformation($"exception occured in CreateOrganization: {ex}");

                return false;
            }
        }

        private async Task AddShopToDb(OrganizationRegisterModel model, string userId)
        {
            try
            {
                var shop = new Shop
                {
                    Name = model.Name,
                    RegistrationLicenseNumber = model.LicenseNumber,
                    UserId = userId,
                };
                await _context.Shops.AddAsync(shop);
                await SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"exception occured in CreateOrganization: {ex}");
            }
        }

        private async Task AddCompanyToDb(OrganizationRegisterModel model, string userId)
        {
            try
            {
                var company = new Company
                {
                    Name = model.Name,
                    LicenseNumber = model.LicenseNumber,
                    UserId = userId
                };
                await _context.Companies.AddAsync(company);
                await SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"exception occured in CreateOrganization: {ex}");
            }

        }

        private async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

    }
}

