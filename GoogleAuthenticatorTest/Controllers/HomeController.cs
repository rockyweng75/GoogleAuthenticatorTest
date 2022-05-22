using Google.Authenticator;
using GoogleAuthenticatorTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoogleAuthenticatorTest
{
    public class HomeController : Controller
    {
        private IGoogleAuthService googleAuthService;
        private IConfiguration configuration;
        public HomeController(
            IConfiguration configuration,
            IGoogleAuthService googleAuthService
            ) {
            this.googleAuthService = googleAuthService;
            this.configuration = configuration; 
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginFrom data)
        {
            var result = googleAuthService.Validate(data.User, data.Password);
            return Json(result ? "200" : "401");
        }

        [HttpPost]
        public IActionResult Register([FromBody] LoginFrom data)
        {
            if (string.IsNullOrEmpty(data.User)) return this.BadRequest();

            string key = null;
            if (googleAuthService.Exists(data.User))
            {
                key = googleAuthService.FindKey(data.User);
            }
            else 
            {
                key = googleAuthService.GenerateKey(data.User);
            }
            var systemName = configuration["SystemName"];
            var setupInfo = googleAuthService.GenerateSetup(systemName, data.User, key);
            return Json(setupInfo);
        }

        [HttpGet]
        public IActionResult GetPin(LoginFrom data)
        {
            return Json(googleAuthService.GeneratePassword(data.User));
        }

        [Serializable]
        public class LoginFrom { 
            public string User { get; set; }

            public string Password { get; set; }
        }
    }
}
