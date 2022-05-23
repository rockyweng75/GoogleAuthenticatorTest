using Google.Authenticator;

namespace GoogleAuthenticatorTest.Services
{
    public class SampleGoogleAuthService : AbstractGoogleAuthService
    {
        public SampleGoogleAuthService(TwoFactorAuthenticator twoFactorAuthenticator) : base(twoFactorAuthenticator)
        {
        }

        public override bool BindKey(string Account, string Key)
        {
            if (Validate(Account, Key))
            {
                // UPDATE DB
                return true;
            }
            else 
            {
                return false;
            }
        }

        public override bool Exists(string Account)
        {
            // FIND DB
            return false;
        }

        public override string FindKey(string Account)
        {
            // Modify to FIND DB 
            return this.GenerateKey(Account);
        }

        public override bool RemoveKey(string Account)
        {
            // UPDATE DB
            return false;
        }
    }
}
