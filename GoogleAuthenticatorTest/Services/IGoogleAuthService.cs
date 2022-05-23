using Google.Authenticator;

namespace GoogleAuthenticatorTest.Services
{
    public interface IGoogleAuthService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Account"></param>
        /// <returns>Key</returns>
        public string GenerateKey(string Account);

        public string FindKey(string Account);

        public bool BindKey(string Account, string Key);

        public bool Exists(string Account);

        public bool RemoveKey(string Account);

        public SetupCode GenerateSetup(string SystemName, string Account, string Key);

        public bool Validate(string Account, string Password);

        public string GeneratePassword(string Account);



    }
}
