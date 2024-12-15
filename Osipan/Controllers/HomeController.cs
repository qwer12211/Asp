using System.Diagnostics;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Osipan.Models;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Osipan.Controllers
{
    public class HomeController : Controller
    {
        public practosElectricASPnetContext _context; 
        public readonly ILogger<HomeController> _logger; 

        
        public HomeController(ILogger<HomeController> logger, practosElectricASPnetContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Authorization(string email, string password)
        {
            // Хеширует пароль
            string hashedPassword = HashPassword(password);

            // Проверяет наличие пользователя с указанным email и хешированным паролем
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.UserPassword == hashedPassword);
            if (user != null)
            {
                // Если пользователь найден, создается список клеймов (утверждений)
                var claim = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.FirstName),
                    new Claim(ClaimTypes.NameIdentifier, user.Email),
                    new Claim(ClaimTypes.Role, user.RoleId == 1 ? "Customer" : "OtherRole")
                };

                // Создается объект идентификации и авторизации
                var claimIdentity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimPrincipal = new ClaimsPrincipal(claimIdentity);

                // Устанавливается cookie для авторизации
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal);

                // Перенаправляет пользователя на страницу корзины
                return RedirectToAction("Index", "Home");
            }

            // Если пользователь не найден, выводит сообщение об ошибке
            ViewBag.ErrorMessage = "КЕНСЕЛЛ!!!!!";
            return View();
        }

        [HttpGet]
        public IActionResult Authorization()
        {
            // Возвращает страницу авторизации
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string fullname, string email, string phone, string password)
        {
            // Проверяет, существует ли пользователь с указанным email
            if (_context.Users.Any(u => u.Email == email))
            {
                ViewBag.ErrorMessage = "Пользователь с таким email уже существует";
                return View();
            }

            // Хеширует пароль
            string hashedPassword = HashPassword(password);

            // Создает нового пользователя
            var user = new User
            {
                FirstName = fullname,
                Email = email,
                PhoneNumber = phone,
                UserPassword = hashedPassword,
                RoleId = 1
            };

            // Добавляет пользователя в базу данных и сохраняет изменения
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Перенаправляет на страницу корзины
            return RedirectToAction("Cart", "Home");
        }

        private string HashPassword(string password)
        {
            // Хеширует пароль с использованием SHA-256
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        public IActionResult Privacy()
        {
            // Возвращает страницу конфиденциальности
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Возвращает страницу ошибки
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> covrik()
        {
            // Возвращает список товаров в категории "коврики"
            return View(await _context.Catalogs.ToListAsync());
        }

        public async Task<IActionResult> notebook()
        {
            // Возвращает список товаров в категории "ноутбуки"
            return View(await _context.Catalogs.ToListAsync());
        }

        public async Task<IActionResult> accessories()
        {
            // Возвращает список товаров в категории "аксессуары"
            return View(await _context.Catalogs.ToListAsync());
        }

        public IActionResult create()
        {
            // Возвращает форму для создания нового товара
            return View();
        }

        public async Task<IActionResult> details(int? id)
        {
            // Возвращает подробную информацию о товаре с указанным id
            if (id != null)
            {
                Catalog catalogss = await _context.Catalogs.FirstOrDefaultAsync(p => p.IdCatalog == id);
                if (catalogss != null)
                {
                    ViewBag.CategoryId = catalogss.CategoryId;
                    return View(catalogss);
                }
            }
            return NotFound();
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> confirmdelete(int? id)
        {
            // Возвращает страницу подтверждения удаления товара
            if (id != null)
            {
                Catalog catalogss = await _context.Catalogs.FirstOrDefaultAsync(p => p.IdCatalog == id);
                if (catalogss != null)
                {
                    ViewBag.CategoryId = catalogss.CategoryId;
                    return View(catalogss);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> delete(int? id)
        {
            // Удаляет товар из базы данных
            if (id != null)
            {
                Catalog catalogss = await _context.Catalogs.FirstOrDefaultAsync(p => p.IdCatalog == id);
                if (catalogss != null)
                {
                    _context.Catalogs.Remove(catalogss);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult CatalogSearch()
        {
            //  страница поиска по каталогу
            var catalogs = _context.Catalogs.ToList();
            return View(catalogs);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Выполняет выход пользователя
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> AddToCart(int catalogId, int quantity)
        {
            // Добавляет товар в корзину
            var userIdString = User.Identity?.Name;
            if (userIdString == null || !int.TryParse(userIdString, out int userId))
            {
                return RedirectToAction("authorization", "Home");
            }

            var catalog = await _context.Catalogs.FindAsync(catalogId);
            if (catalog == null)
            {
                return NotFound("Продукт не найден");
            }

            if (catalog.Quantity < quantity)
            {
                return BadRequest("Недостаточно товара на складе.");
            }

            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.CatalogId == catalogId && c.UsersId == userId);

            if (cart == null)
            {
                cart = new Cart()
                {
                    CatalogId = catalogId,
                    Quantity = quantity,
                    Price = catalog.Price * quantity,
                    UsersId = userId
                };
                _context.Carts.Add(cart);
            }
            else
            {
                cart.Quantity += quantity;
                if (cart.Quantity > catalog.Quantity)
                {
                    return BadRequest("Недостаточно товара на складе для обновленного количества");
                }
                cart.Price = cart.Quantity * catalog.Price;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Cart");
        }

        public IActionResult Cart()
        {
            // Возвращает корзину  пользователя
            var userIdString = User.Identity?.Name;
            if (userIdString == null || !int.TryParse(userIdString, out int userId))
            {
                return RedirectToAction("Authorization", "Home");
            }

            var cart = _context.Carts.Where(c => c.UsersId == userId).Include(c => c.Catalog).ToList();
            return View(Cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromCart(int cartId)
        {
            // Удаляет товар из корзины
            var cartItem = _context.Carts.FirstOrDefault(c => c.CatalogId == cartId);
            if (cartItem != null)
            {
                _context.Carts.Remove(cartItem);
                _context.SaveChanges();
            }
            return RedirectToAction("Cart");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateQuantity(int cartId, int quantity)
        {
            // Обновляет количество товара в корзине
            var cartItem = _context.Carts.FirstOrDefault(c => c.CatalogId == cartId);
            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                cartItem.Price = quantity * cartItem.Catalog.Price;
                _context.SaveChanges();
            }
            return RedirectToAction("Cart");
        }
    }
}
