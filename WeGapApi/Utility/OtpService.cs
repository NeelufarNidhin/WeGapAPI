using System;
using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using WeGapApi.Data;
using WeGapApi.Models;
using Azure.Core;
using System.Net;

namespace WeGapApi.Utility
{
	public class OtpService
	{
		private readonly ApplicationDbContext _context;
        public UserManager<ApplicationUser> _userManager;
		public static Random random = new Random();


        public OtpService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			_context = context;
            _userManager = userManager;
		}


		public async Task GenerateOTP(string userId)
		{
            //string otp = RandomString(6, "0123456789");
            
           string otp = random.Next(100000, 999999).ToString();
            var otpRecord = new OTPRecord
			{
               
				UserId = userId,
				Otp = otp,
				TimeStamp = DateTime.UtcNow
			};

			_context.OTPRecord.Add(otpRecord);
			_context.SaveChanges();
            await SendOTPEmail(userId, otp);

        }

        public  bool VerifyOTP(string userId, string otp)
        {
            // Retrieve stored OTP and timestamp
           
                var otpRecord =  _context.OTPRecord.FirstOrDefault(r => r.UserId == userId);

                // Check if OTP matches and hasn't expired (e.g., within 5 minutes)
                if (otpRecord != null && otpRecord.Otp == otp &&
                    otpRecord.TimeStamp >= DateTime.UtcNow.AddMinutes(-5))
                {
                    // Clear OTP from storage
                    _context.OTPRecord.Remove(otpRecord);
                    _context.SaveChangesAsync();
                    return true;
                }
            

            return false;
        }

        private async Task SendOTPEmail(string userId, string otp)
        {
            // Retrieve user's email address from Identity
            var user = await _userManager.FindByIdAsync(userId);
            string emailAddress = user.Email;

            // Build email content
            string body = $"Your OTP is: {otp}. This OTP will expire in 5 minutes.";

            // Use a library like MailKit to send the email via Gmail API
            // (replace with your Gmail API integration logic)
            using (var client = new SmtpClient())
            {
                MailMessage message = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();
                message.From = new MailAddress("neelufar.nidhin@gmail.com");
                message.To.Add(emailAddress);
              //  message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = body;
                // message.Body = messageotp;

                smtpClient.Port = 587;
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("neelufar.nidhin@gmail.com", "ixobimgtkbsijgyk");
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Send(message);

               // client.SendAsync(message);
            }

            
        }
    }
	}


