using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BitirmeProjesi1.Services;
using BitirmeProjesi1.DTOS;


namespace BitirmeProjesi1.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IHotelService _hotelService;

        public AccountController(IUserService userService, IHotelService hotelService)
        {
            _userService = userService;
            _hotelService = hotelService;
        }

        // Giriş sayfası
        [HttpGet]
        public async Task<IActionResult> LoginAsync()
        {
            if (User.Identity.IsAuthenticated) //YETKİLENMİŞ GİRİŞ BAŞARILI
            {

                var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                if (userRole == "Admin")
                {
                    return RedirectToAction("AdminPanel", "Account"); // Admin paneline yönlendir
                }
                else if (userRole == "Customer")
                {
                    return RedirectToAction("CustomerDashboard", "Account"); // Müşteri paneline yönlendir
                }

            }

            var description = await _hotelService.GetHotelDescriptionForSlider();
            var foto = await _hotelService.GetHotelFotoForSlider();
            var name = await _hotelService.GetHotelNameForSlider();

            TempData["MainPhotos"] = foto;
            TempData["Descriptions"] = description;
            TempData["Name"] = name;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO model)
        {

            var foundUser = await _userService.FindUserByEmailPasswordAsync(model.Email, model.Password);

            if (foundUser == null)
            {
                var foto = await _hotelService.GetHotelFotoForSlider();
                var description = await _hotelService.GetHotelDescriptionForSlider();
                var name = await _hotelService.GetHotelNameForSlider();


                TempData["MainPhotos"] = foto;
                TempData["Descriptions"] = description;
                TempData["Name"] = name;

                ModelState.AddModelError("", "Geçersiz giriş.");

                return View(model);
            }
            else
            {
                // Kullanıcı bulunduysa, ID'yi model'e atıyoruz
                model.ID = foundUser.ID;

                // Kullanıcı doğrulanıyor
                var user = await _userService.ValidateUserAsync(model.Email, model.Password, model.ID);

                // Admin kontrolü
                if (model.Email == "admin@gmail.com" && model.Password == "admin123")
                {
                    user = new UserDTO
                    {
                        FullName = "ADMİN",
                        Email = model.Email,
                        UserType = "Admin",
                        Password = model.Password,
                        Id = model.ID
                    };
                }

                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.FullName),
                        new Claim(ClaimTypes.Role, user.UserType), // Admin, Customer
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim(ClaimTypes.Email, user.Email)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);


                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));


                    var userRole = user.UserType;
                    if (userRole == "Admin")
                    {
                        return RedirectToAction("AdminPanel", "Account"); // Admin paneline yönlendir
                    }
                    else if (userRole == "Customer")
                    {
                        return RedirectToAction("CustomerDashboard", "Account"); // Müşteri paneline yönlendir
                    }
                }
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        // Üyelik sayfası
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO model, bool rememberMe)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.RegisterUserAsync(model);

                if (user != null) // Üyelik başarılıysa
                {
                    // Kullanıcıyı otomatik olarak oturum açtırmak için Login işlemi
                    var loginDTO = new LoginDTO
                    {
                        Email = model.Email,
                        Password = model.Password,
                        ID = model.Id,
                    };

                    return await Login(loginDTO); // Login işlemini tetikle
                }
            }
            var foto = await _hotelService.GetHotelFotoForSlider();
            var description = await _hotelService.GetHotelDescriptionForSlider();
            var name = await _hotelService.GetHotelNameForSlider();


            TempData["MainPhotos"] = foto;
            TempData["Descriptions"] = description;
            TempData["Name"] = name;

            ModelState.AddModelError("", "Üyelik işlemi başarısız.");
            return View(model);

        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminPanel()
        {
            return View();
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CustomerDashboardAsync()
        {

            var foto = await _hotelService.GetHotelFotoForSlider();
            var description = await _hotelService.GetHotelDescriptionForSlider();
            var name = await _hotelService.GetHotelNameForSlider();

                TempData["MainPhotos"] = foto;
                TempData["Descriptions"] = description;
                TempData["Name"] = name;

            return View();
        }

        [AllowAnonymous] //herkes yönlenebilsin girişsiz
        public IActionResult Companies()
        {
            return View();
        }

        [AllowAnonymous] //herkes yönlenebilsin girişsiz
        public IActionResult CompanyDetails()
        {
            return View();
        }
    }
}
