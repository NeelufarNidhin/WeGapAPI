using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.IdentityModel.Tokens;
using WeGapApi.Data;
using WeGapApi.Models;
using WeGapApi.Models.Dto;
using WeGapApi.Services.Services.Interface;
using WeGapApi.Utility;

namespace WeGapApi.Services
{
	public class AuthenticationService : IAuthenticationService
	{
        private readonly ApplicationDbContext _db;
       
        private string secretKey;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;
       
        public AuthenticationService(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
             IConfiguration configuration, IEmailSender emailSender)
		{
            _db = db;
            _roleManager = roleManager;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
            _userManager = userManager;
            _emailSender = emailSender;
          
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            ApplicationUser userFromDb = _db.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == loginRequestDto.Email.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(userFromDb, loginRequestDto.Password);

            if (!isValid)
                throw new InvalidOperationException("UserName or Password is incorrect");

            if (userFromDb.IsBlocked)
            {
                throw new InvalidOperationException("User Blocked, Please contact Adminstrator");
            }

            var roles = await _userManager.GetRolesAsync(userFromDb);
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(secretKey);

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("id", userFromDb.Id.ToString()),
                    new Claim("firstName", userFromDb.FirstName.ToString()),
                    new Claim("lastName", userFromDb.LastName.ToString()),
                    new Claim(ClaimTypes.Email, userFromDb.UserName.ToString()),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault()),
                    new Claim("isBlocked", userFromDb.IsBlocked.ToString())
                }),

               Expires = DateTime.UtcNow.AddDays(7),
               // Expires = DateTime.UtcNow.AddSeconds(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);


            LoginResponseDto loginResponse = new()
            {
                Email = userFromDb.Email,
                Role = userFromDb.Role,
                Token = tokenHandler.WriteToken(token)
            };


            if (loginResponse.Email == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                throw new InvalidOperationException("Please check the user credentials");
            }

            return loginResponse;

        }

        public async Task<bool> LoginWithOTP(OTPLoginDto otpLoginDto)
        {
            var user = await _userManager.FindByEmailAsync(otpLoginDto.Email);


            var result = await _userManager.VerifyTwoFactorTokenAsync(user, "Email", otpLoginDto.Otp);

            if (!result)
            {
                throw new InvalidOperationException("Invalid Otp");
            }
            return true;
        }

        public async Task<IdentityResult> Register(SignUpRequestDto signupRequestDto)
        {
            ApplicationUser userFromDb = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == signupRequestDto.UserName.ToLower());
            if (userFromDb != null)
            {
                throw new InvalidOperationException("User Name already exists");
            }

            ApplicationUser user = new()
            {
                FirstName = signupRequestDto.FirstName,
                LastName = signupRequestDto.LastName,
                UserName = signupRequestDto.UserName,
                Email = signupRequestDto.UserName,
                NormalizedEmail = signupRequestDto.UserName.ToUpper(),
                Role = signupRequestDto.Role,
                TwoFactorEnabled = true
            };



                var result = await _userManager.CreateAsync(user, signupRequestDto.Password);

            if (!result.Succeeded)
            {
                // Handle other errors if necessary
                throw new InvalidOperationException("Failed to register user");
            }



            if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
                    {
                        //create roles in database


                        await _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin));
                        await _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee));
                        await _roleManager.CreateAsync(new IdentityRole(SD.Role_Employer));
                    }


                    if (signupRequestDto.Role.ToLower() == SD.Role_Admin)
                    {
                        await _userManager.AddToRoleAsync(user, SD.Role_Admin);

                    }
                    if (signupRequestDto.Role.ToLower() == SD.Role_Employer)
                    {
                        await _userManager.AddToRoleAsync(user, SD.Role_Employer);

                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, SD.Role_Employee);

                    }
                    var otptoken = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
                    //  string otptoken = GenerateRandomOtp();
                    await _emailSender.SendEmailAsync(user.Email, "OTP Confirmation", otptoken);
                    return result;


                
                 



        }


        public async Task<bool> ResendOTP(ResendOtpDto resendOtp)
        {
            var user = await _userManager.FindByEmailAsync(resendOtp.Email);
            if (user is null)
            {
                throw new InvalidOperationException("User not found");
            }
            var otptoken = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

            user.TwoFactorEnabled = true;
            await _userManager.UpdateAsync(user);

            // Send the new OTP to the user's email
            await _emailSender.SendEmailAsync(user.Email, "New OTP", $"Your new OTP is: {otptoken}");

            return true;
        }
    }
}

