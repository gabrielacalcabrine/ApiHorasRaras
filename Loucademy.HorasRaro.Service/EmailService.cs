using AutoMapper;
using Loucademy.HorasRaro.Domain.Entities;
using Loucademy.HorasRaro.Domain.Interfaces.Repositorys;
using Loucademy.HorasRaro.Domain.Interfaces.Services;
using Loucademy.HorasRaro.Domain.Settings;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using System.IO;


namespace Loucademy.HorasRaro.Service
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private MimeMessage _emailMessage = new MimeMessage();

        public EmailService(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }
        public async Task ConfiguraEnvio(Usuario usuario)
        {
            _emailMessage.From.Add(new MailboxAddress("email", _emailSettings.From));
            _emailMessage.To.Add(new MailboxAddress("email", usuario.Email));
        }

        public async Task DefineAssuntoEmail(string assunto)
        {
            _emailMessage.Subject = assunto;
        }

        public async Task DefineCorpoEmail(string corpo)
        {
            _emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = corpo };
        }

        public async Task EnviaEmail()
        {
            var client = new MailKit.Net.Smtp.SmtpClient();
            try
            {
                client.Connect(_emailSettings.SmtpServer, _emailSettings.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailSettings.UserName, _emailSettings.Password);
                client.Send(_emailMessage);
            }
            catch
            {
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}
