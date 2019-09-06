using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class HousekeeperHelperTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IStatementGenerator> _statementGenerator;
        private Mock<IEmailSender> _emailSender;
        private Mock<IXtraMessageBox> _messageBox;
        private HousekeeperService _service;
        private DateTime _testDate = new DateTime(2017, 1, 1);
        private Housekeeper _housekeeper;
        private string _statementFilename;

        [SetUp]
        public void SetUp()
        {
            _housekeeper = new Housekeeper {Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c"};
            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork
                .Setup(uow => uow.Query<Housekeeper>())
                .Returns(new List<Housekeeper>
                {
                    _housekeeper
                }.AsQueryable());

            _statementFilename = "filename";
            _statementGenerator = new Mock<IStatementGenerator>();
            _statementGenerator
                .Setup(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _testDate))
                .Returns(() => _statementFilename);
            
            _emailSender = new Mock<IEmailSender>();
            _messageBox = new Mock<IXtraMessageBox>();
            
            _service = new HousekeeperService(
                _unitOfWork.Object,
                _statementGenerator.Object,
                _emailSender.Object,
                _messageBox.Object
            );
        }

        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            _service.SendStatementEmails(_testDate);
            
            _statementGenerator.Verify(sg => 
                sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _testDate));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SendStatementEmails_HoeseKeepersEmailIsInvalid_ShouldNotGenerateStatement(string email)
        {
            _housekeeper.Email = email;
            
            _service.SendStatementEmails(_testDate);
            
            _statementGenerator.Verify(sg => 
                sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _testDate), Times.Never);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_EmailTheStatement()
        {
            _statementGenerator
                .Setup(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _testDate))
                .Returns(_statementFilename);
            
            _service.SendStatementEmails(_testDate);
            
            VerifyEmailSent();
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SendStatementEmails_StatementFilenameIsInvalid_ShouldNotEmailTheStatement(string returnFilename)
        {
            _statementFilename = returnFilename;
            
            _service.SendStatementEmails(_testDate);
            
            VerifyEmailNotSent();
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SendStatementEmails_EmailSendingFails_DisplayAMessageBox(string returnFilename)
        {
            _emailSender.Setup(es => es.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>())
            ).Throws<Exception>();
            
            _service.SendStatementEmails(_testDate);

            VerifyMessageBoxDisplay();
        }

        private void VerifyMessageBoxDisplay()
        {
            _messageBox.Verify(mb => mb.Show(
                It.IsAny<string>(),
                It.IsAny<string>(),
                MessageBoxButtons.OK));
        }

        private void VerifyEmailNotSent()
        {
            _emailSender.Verify(es => es.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()
            ), Times.Never);
        }

        private void VerifyEmailSent()
        {
            _emailSender.Verify(es => es.EmailFile(
                _housekeeper.Email,
                _housekeeper.StatementEmailBody,
                _statementFilename,
                It.IsAny<string>()
            ));
        }
    }
}