﻿using netDumbster.smtp;
using NUnit.Framework;
using SmartRestaurant.Application.Common.Tools;

namespace SmartRestaurant.Application.IntegrationTests.Helpers
{
    [TestFixture]
    public class EmailHelperTest
    {
        private SimpleSmtpServer smtpServer;
        private SmtpConfig smtpConfig = new SmtpConfig
        {
            Email = "no-reply@fakesmtp.com",
            Pass = "",
            Server = "localhost",
            Port = 789
        };

        [SetUp]
        public void Setup()
        {
            smtpServer = SimpleSmtpServer.Start(smtpConfig.Port);
        }

        [TearDown]
        public void TearDown()
        {
            smtpServer.Stop();
            smtpServer.Dispose();
        }

        [Test]
        public void SendEmail()
        {        
            //var userEmail = "user@gmail.com";
            //var emailSubject = "Email test";
            //var emailContent = "<h1> Email content </h1>";

            //new EmailHelper(smtpConfig).SendEmail(userEmail, emailSubject, emailContent);

            //Assert.AreEqual(1, smtpServer.ReceivedEmailCount);
            //SmtpMessage mail = smtpServer.ReceivedEmail[0];
            //Assert.AreEqual(userEmail, mail.Headers["To"]);
            //Assert.AreEqual(smtpConfig.Email, mail.Headers["From"]);
            //Assert.AreEqual(emailSubject, mail.Headers["Subject"]);
            //Assert.AreEqual(emailContent, mail.MessageParts[0].BodyData);
        }
    }
}
