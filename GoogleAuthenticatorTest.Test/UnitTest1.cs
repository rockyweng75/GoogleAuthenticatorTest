using NUnit.Framework;
using Google.Authenticator;
using System;

namespace GoogleAuthenticatorTest.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            string key = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);

            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            SetupCode setupInfo = tfa.GenerateSetupCode("Test Two Factor", "user@example.com", key, false, 3);

            string qrCodeImageUrl = setupInfo.QrCodeSetupImageUrl;
            string manualEntrySetupCode = setupInfo.ManualEntryKey;

            //imgQrCode.ImageUrl = qrCodeImageUrl;
            //lblManualSetupCode.Text = manualEntrySetupCode;

            //// verify
            //TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            //bool result = tfa.ValidateTwoFactorPIN(key, txtCode.Text)


            Assert.Pass();
        }
    }
}