using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;
using SendGrid;
using System.Net;

using System.Diagnostics;
using DivineApp.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using DivineApp.Contexts;
using Microsoft.Owin.Security;
using System.Security.Claims;
using Microsoft.Azure;
using System.Configuration;

namespace DivineApp.App_Start
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            await configSendGridasync(message);
        }

        private async Task configSendGridasync(IdentityMessage message)
        {
            var myMessage = new SendGridMessage();
            myMessage.AddTo(message.Destination);
            myMessage.From = new System.Net.Mail.MailAddress("veritysolutions2017@outlook.com", "Verity Solutions");
            myMessage.Subject = message.Subject;
            myMessage.Text = message.Body;

            
            var credentials = new NetworkCredential(
                ConfigurationManager.AppSettings["emailServiceUserName"],
                ConfigurationManager.AppSettings["emailServicePassword"]
                );
            var transportWeb = new Web(credentials);
            if(transportWeb != null)
            {
                await transportWeb.DeliverAsync(myMessage);
            }
            else
            {
                Trace.TraceError("Failed to create Web transport.");
                await Task.FromResult(0);
            }

        }
    }
    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
    public class CompanyUserManager : UserManager<CompanyUser>
    {
        public CompanyUserManager(IUserStore<CompanyUser> store) : base(store)
        {
        }

        public static CompanyUserManager Create(IdentityFactoryOptions<CompanyUserManager> options, IOwinContext context)
        {
            
            var textx = context.Get<MyContext>();
            var userstore = new UserStore<CompanyUser>(textx);
            var manager = new CompanyUserManager(userstore);
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<CompanyUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<CompanyUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<CompanyUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<CompanyUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
    public class ApplicationSignInManager : SignInManager<CompanyUser, string>
    {
        public ApplicationSignInManager(CompanyUserManager userManager, IAuthenticationManager authenticationManager) :
            base(userManager, authenticationManager)
        { }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(CompanyUser user)
        {
            return user.GenerateUserIdentityAsync((CompanyUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<CompanyUserManager>(), context.Authentication);
        }
    }
}