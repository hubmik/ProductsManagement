using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace CustomerApp.Models
{
    public class MailSender
    {
        public static string GmailUsername { get; set; }
        public static string GmailPassword { get; set; }
        public static string GmailHost { get; set; }
        public static int GmailPort { get; set; }
        public static bool GmailSSL { get; set; }

        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }

        static MailSender()
        {
            GmailHost = "smtp.gmail.com";
            GmailPort = 587;
            GmailSSL = true;
            GmailPassword = "Wolperishe";
            GmailUsername = "hubmigcp@gmail.com";
        }

        private async Task Init()
        {
            SmtpClient client = new SmtpClient()
            {
                Host = GmailHost,
                Port = GmailPort,
                EnableSsl = GmailSSL,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(GmailUsername, GmailPassword)
            };

            using (var message = new MailMessage(GmailUsername, ToEmail))
            {
                message.Subject = Subject;
                message.Body = Body;
                message.IsBodyHtml = IsHtml;
                await client.SendMailAsync(message);
            }
        }

        private string InitMessageBody(Cart cart)
        {
            string message = "";
            int quant = 0;
            decimal value;
            IEnumerable<CartLine> cartLines = cart.Lines;

            foreach (var item in cartLines)
            {
                message += $"{item.Product.ProductName}({item.Quantity}), price: {item.Product.UnitPrice * item.Quantity}</br>";
                quant += item.Quantity;
            }
            value = cart.ComputeTotalValue();
            message += $"</br>Total products quantity: {quant}</br>";
            message += $"Total value to pay: {value}";

            return message;
        }

        public async Task Send(ViewModels.CartSummaryViewModel csVM, Cart cart)
        {
            MailSender mailer = new MailSender()
            {
                ToEmail = csVM.Email,
                Subject = $"From MerchanApp to: {csVM.CompanyName}.",
                Body = $"Thank you for your order.</br> {InitMessageBody(cart)}",
                IsHtml = true,                
            };
            await mailer.Init();
        }
    }
}