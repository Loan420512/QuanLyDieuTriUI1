

using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System; // Cần thiết để sử dụng Console.WriteLine

namespace QLDT.BLL.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string message);
    }

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var emailSettings = _configuration.GetSection("EmailSettings");
            Console.WriteLine("1");
            var senderEmail = emailSettings["SenderEmail"];
            Console.WriteLine("2");
            var senderPassword = emailSettings["SenderPassword"];
            Console.WriteLine("3");
            var smtpHost = emailSettings["SmtpHost"];
            Console.WriteLine("4");
            var smtpPort = int.Parse(emailSettings["SmtpPort"]);
            
            Console.WriteLine($"[EmailService] Attempting to send email to: {toEmail}");
            Console.WriteLine($"[EmailService] From: {senderEmail}, Host: {smtpHost}, Port: {smtpPort}");

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(senderEmail));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            try
            {
                Console.WriteLine("[EmailService] Connecting to SMTP server...");
                await smtp.ConnectAsync(smtpHost, smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                Console.WriteLine("[EmailService] Connected. Authenticating...");

                await smtp.AuthenticateAsync(senderEmail, senderPassword);
                Console.WriteLine("[EmailService] Authenticated. Sending email...");

                await smtp.SendAsync(email);
                Console.WriteLine("[EmailService] Email sent successfully!");
            }
            catch (Exception ex)
            {
                // Ghi log chi tiết lỗi gửi email
                Console.WriteLine($"[EmailService ERROR] Failed to send email: {ex.Message}");
                Console.WriteLine($"[EmailService ERROR] Stack Trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"[EmailService ERROR] Inner Exception: {ex.InnerException.Message}");
                }
                throw; // Ném lại ngoại lệ để lỗi được xử lý ở lớp cao hơn
            }
            finally
            {
                // Luôn ngắt kết nối
                if (smtp.IsConnected)
                {
                    await smtp.DisconnectAsync(true);
                    Console.WriteLine("[EmailService] Disconnected from SMTP server.");
                }
            }
        }
    }
}