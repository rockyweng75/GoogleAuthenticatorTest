using Google.Authenticator;

namespace GoogleAuthenticatorTest.Services
{
    public abstract class AbstractGoogleAuthService : IGoogleAuthService
    {
        private TwoFactorAuthenticator twoFactorAuthenticator;
        public AbstractGoogleAuthService(TwoFactorAuthenticator twoFactorAuthenticator)
        { 
            this.twoFactorAuthenticator = twoFactorAuthenticator; 
        }

        public abstract bool Exists(string Account);

        public abstract string FindKey(string Account);

        public virtual string GenerateKey(string Account)
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);
        }

        public string GeneratePassword(string Account)
        {
            var key = this.FindKey(Account);
            if (string.IsNullOrEmpty(key)) throw new Exception("Account has not Found");

            return twoFactorAuthenticator.GetCurrentPIN(key);
        }

        public virtual SetupCode GenerateSetup(string SystemName, string Account, string Key)
        {
            return twoFactorAuthenticator.GenerateSetupCode(SystemName, Account, Key, false, 3);
        }

        public abstract bool RemoveKey(string Account);

        public virtual bool Validate(string Account, string Password)
        {
            var key = this.FindKey(Account);
            if (string.IsNullOrEmpty(key)) return false;
            return twoFactorAuthenticator.ValidateTwoFactorPIN(key, Password);
        }
    }
}
