using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WeGapApi.Data;
using WeGapApi.Models;
using WeGapApi.Models.Dto;
using WeGapApi.Utility;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeGapApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly ApplicationDbContext _db;
        private ApiResponse _response;
        private string secretKey;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public AuthController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, IConfiguration configuration, IEmailSender emailSender, SignInManager<ApplicationUser> signInManager)
        {
            _db = db;
            _response = new ApiResponse();
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
            _userManager = userManager;
            _roleManager = roleManager;
           
            _emailSender = emailSender;
            _signInManager = signInManager;

        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequestDto model)
        {
            ApplicationUser userFromDb = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());

            if (userFromDb != null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages="User Name already exists";
                return BadRequest(_response);
            }


            ApplicationUser user = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.UserName,
                NormalizedEmail = model.UserName.ToUpper(),
                Role = model.Role,
                TwoFactorEnabled = true
            };

            try
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
                    {
                        //create roles in database


                        await _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin));
                        await _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee));
                        await _roleManager.CreateAsync(new IdentityRole(SD.Role_Employer));
                    }


                    if (model.Role.ToLower() == SD.Role_Admin)
                    {
                        await _userManager.AddToRoleAsync(user, SD.Role_Admin);

                    }
                    if (model.Role.ToLower() == SD.Role_Employer)
                    {
                        await _userManager.AddToRoleAsync(user, SD.Role_Employer);

                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, SD.Role_Employee);

                    }

                    //  Add token to Verify the email...
                    //  _logger.LogInformation($"Generating user info for user {user.UserName}");

                    var otptoken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action(nameof(ConfirmEmail), "Auth", new { otptoken, email = user.Email }, Request.Scheme);

                    await _emailSender.SendEmailAsync(user.Email!, "Confirmation email link", confirmationLink!);

                    _response.StatusCode = HttpStatusCode.OK;
                    _response.Message = $"User Created & Email Sent to {user.Email} Successfully ";
                    _response.IsSuccess = true;
                    return Ok(_response);



                }
            }

            catch (Exception ex)
            {
                // _logger.LogError("Failed to generate email confirmation token for user {user.UserName}: {ex.Message}", ex);
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }



            return BadRequest(_response);

        }

        //[HttpGet]
        //public async Task<IActionResult> TestEmail(string email)
        //{

        //    var user = await _userManager.GetUserAsync(User);
        //    string otp = GenerateRandomOtp();

        //    await _emailSender.SendEmailAsync(email, "Confirm your email",
        //    //   otp);
        //           $"Please confirm your account by <a href={otp}>clicking here</a>.");
        //    _response.StatusCode = HttpStatusCode.OK;
        //    _response.IsSuccess = true;
        //    _response.Message = "Email Sent SuccessFully";

        //    return Ok(_response);
        //    // return (IActionResult)Task.FromResult(false);

        //}

        //private string GenerateRandomOtp()
        //{
        //    Random random = new Random();
        //    return random.Next(100000, 999999).ToString();
        //}


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {

            ApplicationUser userFromDb = _db.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == model.Email.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(userFromDb, model.Password);

            if (isValid == false)
            {
                _response.Result = new LoginResponseDto();
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = "Username or Password is Incorrect";
                return BadRequest(_response);
            }
            if (userFromDb.IsBlocked)
            {
                _response.Result = new LoginResponseDto();
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = "User Blocked, Please contact Adminstrator";
                return BadRequest(_response);
            }

           

          //  var roles = await _userManager.GetRolesAsync(userFromDb);
            if (userFromDb.TwoFactorEnabled)
            {

                await _signInManager.SignOutAsync();
                await _signInManager.PasswordSignInAsync(userFromDb, model.Password, false, true);


                var otptoken = await _userManager.GenerateTwoFactorTokenAsync(userFromDb, "Email");
                await _emailSender.SendEmailAsync(userFromDb.Email, "OTP Confirmation", otptoken);

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Message = $"We have sent OTP to your Email {userFromDb.Email}";
                return Ok(_response);
            }

            else
            {
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Message = $"We have sent OTP to your Email {userFromDb.Email}";
                return Ok(_response);
            }
        }

            


        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail ( string email, string otptoken)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if(user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, otptoken);
                if (result.Succeeded)
                {
                    _response.StatusCode = HttpStatusCode.OK;
                    _response.Message = "Email Verified Succesfully";
                    _response.IsSuccess = true;
                    return Ok(_response);
                }
            }

            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.ErrorMessages="This user doesnt exist";
            _response.IsSuccess = false;


            return Ok(_response);
        }


        [HttpPost("login-2FA")]
        public async Task<IActionResult> LoginWithOTP([FromBody] OTPLoginDto model)
        {
            try
            {
                // Find the user by email
                var user = await _userManager.FindByEmailAsync(model.Email);

                // Verify the OTP using the user's email
                var result = await _userManager.VerifyTwoFactorTokenAsync(user, "Email", model.Otp);

                if (result)
                {
                    // OTP verification succeeded

                    if (user != null)
                    {
                        var roles = await _userManager.GetRolesAsync(user);
                        JwtSecurityTokenHandler tokenHandler = new();
                        byte[] key = Encoding.ASCII.GetBytes(secretKey);

                        SecurityTokenDescriptor tokenDescriptor = new()
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                            {
                        new Claim("id", user.Id.ToString()),
                        new Claim("firstName",user.FirstName.ToString()),
                        new Claim(ClaimTypes.Email,user.UserName.ToString()),
                        new Claim(ClaimTypes.Role , roles.FirstOrDefault()),
                            }),

                            Expires = DateTime.UtcNow.AddDays(7),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                        };

                        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);


                        LoginResponseDto loginResponse = new()
                        {
                            Email = user.Email,
                            Token = tokenHandler.WriteToken(token)
                        };

                        if (loginResponse.Email == null || string.IsNullOrEmpty(loginResponse.Token))
                        {

                            _response.StatusCode = HttpStatusCode.BadRequest;
                            _response.IsSuccess = false;
                            _response.ErrorMessages="Username or Password is Incorrect";
                            _response.ErrorMessages="";
                            return BadRequest(_response);
                        }

                        _response.StatusCode = HttpStatusCode.OK;
                        _response.Result = loginResponse;
                        _response.IsSuccess = true;
                        return Ok(_response);
                    }

                       
                }
                else
                {
                    // OTP verification failed
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages="Invalid OTP";
                    return BadRequest(_response);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages= ex.Message;
                return BadRequest(_response);

            }
            return Ok();
           
        }

        [HttpPost("resend-otp")]
        public async Task<IActionResult> ResendOTP([FromBody] ResendOtpDto model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = "User not found";
                    return BadRequest(_response);
                }

                var otptoken = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
                // string newOtp = GenerateRandomOtp();

                // Update the user's OTP (if necessary)
                // You might have to adjust this according to your implementation
                user.TwoFactorEnabled = true;
                await _userManager.UpdateAsync(user);

                // Send the new OTP to the user's email
                await _emailSender.SendEmailAsync(user.Email, "New OTP", $"Your new OTP is: {otptoken}");

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Message = "New OTP sent successfully";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages = ex.Message;
                return StatusCode(500, _response); // Internal Server Error
            }
        }




    }
}

