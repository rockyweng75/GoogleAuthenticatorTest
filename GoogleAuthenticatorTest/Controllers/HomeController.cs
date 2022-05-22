using Google.Authenticator;
using Microsoft.AspNetCore.Mvc;

namespace GoogleAuthenticatorTest
{
    public class HomeController : Controller
    {



        public IActionResult Index()
        {
            HttpContext.Session.SetString("Key", Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10));
            ViewData["SessionId"] = HttpContext.Session.Id;
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginFrom data)
        {
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            var key =
                     HttpContext.Session.GetString("Key");

            bool result = tfa.ValidateTwoFactorPIN(key, data.Password, TimeSpan.FromMinutes(10));
           
            return Json(result ? "200" : "401");
        }
        [HttpPost]
        public IActionResult Auth([FromBody] LoginFrom data)
        {
            var key =
                HttpContext.Session.GetString("Key");

            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            SetupCode setupInfo = tfa.GenerateSetupCode("Test Two Factor", data.User, key, false, 3);
            return Json(setupInfo);
        }

        [HttpGet]
        public IActionResult GetPin()
        {
            var key =
                HttpContext.Session.GetString("Key");

            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            return Json(tfa.GetCurrentPIN(key));
        }

        [Serializable]
        public class LoginFrom { 
            public string User { get; set; }

            public string Password { get; set; }
        }
    }
}
