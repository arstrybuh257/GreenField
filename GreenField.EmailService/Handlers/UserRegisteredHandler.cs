using System;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentEmail.Smtp;
using GreenField.Common.Messaging.Messages;
using GreenField.DAL.DataAccess.Interfaces;
using GreenField.EmailService.Models;
using MediatR;

namespace GreenField.EmailService.Handlers
{
    public class UserRegisteredHandler : INotificationHandler<UserRegisteredMessage>
    {
        private IRepository<EmailServiceLog> _repository;

        public UserRegisteredHandler(IRepository<EmailServiceLog> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UserRegisteredMessage message, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Email: {message.Email},  Password: {message.Password}");

            var sender = new SmtpSender(() => new SmtpClient("localhost")
            {
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                PickupDirectoryLocation = @"C:\Demos"
            });

            Email.DefaultSender = sender;

            SendResponse email;
            EmailServiceLog log;
            try
            {
                email = await Email.From("arseniy.trybukh@gmail.com")
                    .To(message.Email, message.Email)
                    .Subject("GreenField registration")
                    .Body($"You have been successfully registered in GreenField platform." +
                          $"Your credentials: Email: {message.Email},  Password: {message.Password}.")
                    .SendAsync();
            }
            catch (Exception ex)
            {
                log = new EmailServiceLog()
                {
                    Date = DateTime.Now,
                    Message = $"Error occured while sending email. Error message: {ex.Message}",
                    Status = LogStatus.Info
                };
                await _repository.AddAsync(log);
                return;
            }
            
            if (email.Successful)
            {
                log = new EmailServiceLog()
                {
                    Date = DateTime.Now,
                    Message = $"Email was successfully sent for user {message.Email}",
                    Status = LogStatus.Info
                };
            }
            else
            {
                log = new EmailServiceLog()
                {
                    Date = DateTime.Now,
                    Message = $"Error occured while sending email. Error message: {email.ErrorMessages.FirstOrDefault()}",
                    Status = LogStatus.Error
                };
            }

            await _repository.AddAsync(log);
        }
    }
}