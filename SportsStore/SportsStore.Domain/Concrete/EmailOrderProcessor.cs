using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System.Net;

namespace SportsStore.Domain.Concrete
{
    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;
        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }
        public void ProcessOrder(Cart cart, ShoppingDetails shoppingDetails)
        {
            using (var smtpClient = new SmtpClient())
            {
                MailMessage mailMessage = new MailMessage(emailSettings.MailFromAddress,emailSettings.MailToAddress,"New Order","");
                if(emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.ASCII;
                }
                //smtpClient.Send(mailMessage);
            }
        }
    }

    public class EmailSettings
    {
        public string MailToAddress = "1040658628@qq.com";
        public string MailFromAddress = "1040658628@qq.com";
        public bool UseSsl = true;
        public string Username = "1040658628@qq.com";
        public string Password = "";
        public string ServerName = "smtp.example.com";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"";
    }
}
