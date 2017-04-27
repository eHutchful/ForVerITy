using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using SendGrid;
using System.Net;
using System.Configuration;
using System.Diagnostics;

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
            myMessage.From = new System.Net.Mail.MailAddress("VerityEmail", "Verity Solutions");
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
}