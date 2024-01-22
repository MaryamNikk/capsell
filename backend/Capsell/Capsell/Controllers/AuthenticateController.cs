using System;
using Capsell.DataProvide;
using Capsell.Models.Authenticate;
using Capsell.Services.Authenticate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Capsell.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
	{
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthenticateController> _logger;
        private readonly IRegisterationService _service;


        public AuthenticateController(
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            ILogger<AuthenticateController> logger,
            IRegisterationService service)
        {
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    _logger.LogInformation($"user login successfuly with id:{user.Id}");

                    var success = new LoginDto
                    {
                        UserId = user.Id,
                        Name = user.Name,
                        Role = user.Role
                    };

                    return Ok(success);
                }
                _logger.LogInformation($"user didn't authorized");
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"exception: {ex}");
                _logger.LogInformation($"user didn't authorized");
                return Unauthorized();
            }

        }

        [HttpPost]
        [Route("customerRegister")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new()
            {
                Name = model.Name,
                Role = "customer",
                PhoneNumber = model.Mobile,
                Address = model.Address,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            _logger.LogInformation($"user registered successfuly with id: {user.Id}");

            if (!result.Succeeded)
            {
                _logger.LogError($"Exception occured while creating user");
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            }

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }


        [HttpPost]
        [Route("organizationRegister")]
        public async Task<IActionResult> Register([FromBody] OrganizationRegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new()
            {
                Name = model.Name,
                Role = model.Role,
                PhoneNumber = model.Mobile,
                Address = model.Address,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);


            _logger.LogInformation($"user registered successfuly with id: {user.Id}");

            if (!result.Succeeded)
            {
                _logger.LogError($"Exception occured while creating user");
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            }

            var u = await _userManager.FindByNameAsync(model.Username);

            var isOrgCreateSuccessfuly = await _service.CreateOrganizationService(model, u.Id);

            if (!isOrgCreateSuccessfuly)
            {
                _logger.LogError($"Exception occured while creating user");
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            }

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });


        }

    }
}

