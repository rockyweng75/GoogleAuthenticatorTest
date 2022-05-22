using Google.Authenticator;

namespace GoogleAuthenticatorTest.Services
{
    public class SampleGoogleAuthService : AbstractGoogleAuthService
    {
        public SampleGoogleAuthService(TwoFactorAuthenticator twoFactorAuthenticator) : base(twoFactorAuthenticator)
        {
        }

        public override bool Exists(string Account)
        {
            // FIND DB
            return false;
        }

        public override string FindKey(string Account)
        {
            // FIND DB
            return this.GenerateKey(Account);
        }

        public override bool RemoveKey(string Account)
        {
            // UPDATE DB
            return false;
        }
    }
}
