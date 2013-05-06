using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Ninject;
using Ninject.MockingKernel;
using Perks.Mail;
using Perks.Wrappers;

namespace Perks.Tests.Mail
{
    public class NetSmtpMailerTests : FixtureWithKernel
    {
        private NetSmtpMailer mailer;

        public NetSmtpMailerTests()
        {
            kernel.Bind<SmtpWrapper>().ToMock().InSingletonScope();
        }

        [SetUp]
        public void SetUp()
        {
            mailer = kernel.Get<NetSmtpMailer>();
        }

        [Test]
        public void Send_For_null_email_Should_throw_argument_null_exception()
        {
            // act
            Action call = () => mailer.Send(null);

            // asserts
            call.ShouldThrow<ArgumentNullException>().Where(x => x.ParamName == "email");
        }

        [Test]
        public void Send_For_null_from_address_Should_throw_argument_null_exception()
        {
            // setups
            var email = new Email();

            // act
            Action call = () => mailer.Send(email);

            // asserts
            call.ShouldThrow<ArgumentNullException>().Where(x => x.ParamName == "from");
        }

        [Test]
        public void Send_For_empty_from_address_Should_throw_argument_exception()
        {
            // setups
            var email = new Email {From = string.Empty};

            // act
            Action call = () => mailer.Send(email);

            // asserts
            call.ShouldThrow<ArgumentException>().Where(x => x.ParamName == "from");
        }

        [Test]
        public void Send_Should_send_the_message_by_smtp()
        {
            // setups
            var email = new Email
                {
                    From = "me@m.com",
                    To = new[] {"mom@m.com", "dad@m.com"},
                    Subject = "Hello!",
                    Body = "<h1>Hello from Ukraine!</h1><p>So good is here!</p>",
                    IsHtml = true
                };

            // act
            mailer.Send(email);

            // asserts
            kernel.Get<SmtpWrapper>().Received().Send(Arg.Is<MailMessage>(mail =>
                mail.From.Address == "me@m.com" &&
                mail.To.Any(x => x.Address == "mom@m.com") &&
                mail.To.Any(x => x.Address == "dad@m.com") &&
                mail.Subject == "Hello!" &&
                mail.Body == "<h1>Hello from Ukraine!</h1><p>So good is here!</p>" &&
                mail.IsBodyHtml)
            );
        }
    }
}
