using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly.Core.Entities;
using Bookly.Service.Abstract;
using Bookly.Service.Concrete;
using Bookly.WebUI.ExtensionMethods;
using Bookly.WebUI.Models;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Bookly.WebUI.Controllers
{
    public class CartController : Controller
    {
        private readonly IService<Product> _serviceProduct;
        private readonly IService<Core.Entities.Address> _serviceAddress;
        private readonly IService<AppUser> _serviceAppUser;
        private readonly IService<Order> _serviceOrder;
        private readonly IConfiguration _configuration;

        public CartController(IService<Product> serviceProduct, IService<Core.Entities.Address> serviceAddress, IService<AppUser> serviceAppUser, IService<Order> serviceOrder, IConfiguration configuration)
        {
            _serviceProduct = serviceProduct;
            _serviceAddress = serviceAddress;
            _serviceAppUser = serviceAppUser;
            _serviceOrder = serviceOrder;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var cart = GetCart();
            var model = new CartViewModel
            {
                CartLines = cart.CartLines,
                TotalPrice = cart.TotalPrice()
            };
            return View(model);
        }

        public IActionResult Add(int ProductId, int quantity = 1)
        {
            var product = _serviceProduct.Find(ProductId);
            if (product != null)
            {
                var cart = GetCart();
                cart.AddProduct(product, quantity);
                HttpContext.Session.SetJson("Cart", cart);
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Update(int ProductId, int quantity = 1)
        {
            var product = _serviceProduct.Find(ProductId);
            if (product != null)
            {
                var cart = GetCart();
                cart.UpdateProduct(product, quantity);
                HttpContext.Session.SetJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int ProductId)
        {
            var product = _serviceProduct.Find(ProductId);
            if (product != null)
            {
                var cart = GetCart();
                cart.RemoveProduct(product);
                HttpContext.Session.SetJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            var cart = GetCart();
            var appUser = await _serviceAppUser.GetAsync(x => x.UserGuid.ToString() == HttpContext.User.FindFirst("UserGuid").Value);

            if (appUser == null)
                return RedirectToAction("SignIn", "Account");

            var addresses = await _serviceAddress.GetAllAsync(a => a.AppUserId == appUser.Id && a.IsActive);
            var model = new CheckoutViewModel
            {
                CartProducts = cart.CartLines,
                TotalPrice = cart.TotalPrice(),
                Addresses = addresses
            };
            return View(model);
        }

        [Authorize, HttpPost]
        public async Task<IActionResult> Checkout(string CardNameSurname, string CardNumber, string CardMonth, string CardYear, string CVV, string DeliveryAddress, string BillingAddress)
        {
            var cart = GetCart();
            var appUser = await _serviceAppUser.GetAsync(x => x.UserGuid.ToString() == HttpContext.User.FindFirst("UserGuid").Value);
            if (appUser == null)
                return RedirectToAction("SignIn", "Account");

            var addresses = await _serviceAddress.GetAllAsync(a => a.AppUserId == appUser.Id && a.IsActive);
            var model = new CheckoutViewModel
            {
                CartProducts = cart.CartLines,
                TotalPrice = cart.TotalPrice(),
                Addresses = addresses
            };

            // Zorunlu alan kontrolü
            if (string.IsNullOrWhiteSpace(CardNumber) ||
                string.IsNullOrWhiteSpace(CardMonth) ||
                string.IsNullOrWhiteSpace(CardYear) ||
                string.IsNullOrWhiteSpace(CVV) ||
                string.IsNullOrWhiteSpace(DeliveryAddress) ||
                string.IsNullOrWhiteSpace(BillingAddress))
            {
                TempData["Message"] = "<div class='alert alert-warning'>Lütfen tüm alanları eksiksiz doldurunuz.</div>";
                return View(model);
            }

            var faturaAdresi = addresses.FirstOrDefault(a => a.AddressGuid.ToString() == BillingAddress);
            var teslimatAdresi = addresses.FirstOrDefault(a => a.AddressGuid.ToString() == DeliveryAddress);

            if (faturaAdresi == null || teslimatAdresi == null)
            {
                TempData["Message"] = "<div class='alert alert-danger'>Adres bilgileri bulunamadı.</div>";
                return View(model);
            }

            var siparis = new Order
            {
                AppUserId = appUser.Id,
                BillingAddress = $"{faturaAdresi.OpenAddress} {faturaAdresi.District} {faturaAdresi.City}",
                DeliveryAddress = $"{teslimatAdresi.OpenAddress} {teslimatAdresi.District} {teslimatAdresi.City}",
                CustomerId = appUser.UserGuid.ToString(),
                OrderDate = DateTime.Now,
                TotalPrice = cart.TotalPrice(),
                OrderNumber = Guid.NewGuid().ToString(),
                OrderState = 0,
                OrderLines = new List<OrderLine>()
            };

            #region OdemeIslemi

            var options = new Options
            {
                ApiKey = _configuration["IyzicOptions:ApiKey"],
                SecretKey = _configuration["IyzicOptions:SecretKey"],
                BaseUrl = _configuration["IyzicOptions:BaseUrl"]
            };

            var request = new CreatePaymentRequest
            {
                Locale = Locale.TR.ToString(),
                ConversationId = HttpContext.Session.Id,
                Price = siparis.TotalPrice.ToString().Replace(",", "."),
                PaidPrice = siparis.TotalPrice.ToString().Replace(",", "."),
                Currency = Currency.TRY.ToString(),
                Installment = 1,
                BasketId = "B" + HttpContext.Session.Id,
                PaymentChannel = PaymentChannel.WEB.ToString(),
                PaymentGroup = PaymentGroup.PRODUCT.ToString(),
                PaymentCard = new PaymentCard
                {
                    CardHolderName = CardNameSurname, // Test girişi için kullanılacaklar; John Doe
                    CardNumber = CardNumber, // Test kart numarası: 5528790000000008

                    ExpireMonth = CardMonth, // Test kart ayı: 12
                    ExpireYear = CardYear, // Test kart yılı: 2030
                    Cvc = CVV, // Test CVV: 123
                    RegisterCard = 0
                }
            };

            // Buyer bilgileri
            request.Buyer = new Buyer
            {
                Id = "BY" + appUser.Id,
                Name = appUser.Name,
                Surname = appUser.Surname,
                GsmNumber = appUser.Phone,
                Email = appUser.Email,
                IdentityNumber = "11111111111", // Test için dummy TC no
                LastLoginDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                RegistrationDate = appUser.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                RegistrationAddress = siparis.DeliveryAddress,
                Ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1",
                City = teslimatAdresi.City,
                Country = "Turkey",
                ZipCode = "34732"
            };

            // Shipping Address
            request.ShippingAddress = new Iyzipay.Model.Address
            {
                ContactName = appUser.Name + " " + appUser.Surname,
                City = teslimatAdresi.City,
                Country = "Turkey",
                Description = teslimatAdresi.OpenAddress,
                ZipCode = "34742"
            };

            // Billing Address
            request.BillingAddress = new Iyzipay.Model.Address
            {
                ContactName = appUser.Name + " " + appUser.Surname,
                City = faturaAdresi.City,
                Country = "Turkey",
                Description = faturaAdresi.OpenAddress,
                ZipCode = "34742"
            };

            // Basket items
            var basketItems = new List<BasketItem>();
            foreach (var item in cart.CartLines)
            {
                siparis.OrderLines.Add(new OrderLine
                {
                    ProductId = item.Product.Id,
                    OrderId = siparis.Id,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price
                });

                basketItems.Add(new BasketItem
                {
                    Id = item.Product.Id.ToString(),
                    Name = item.Product.Name,
                    Category1 = "Collectibles",
                    ItemType = BasketItemType.PHYSICAL.ToString(),
                    Price = (item.Product.Price * item.Quantity).ToString().Replace(",", ".")
                });
            }
            request.BasketItems = basketItems;

            Payment payment;
            try
            {
                payment = await Payment.Create(request, options);
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"<div class='alert alert-danger'>Ödeme isteği sırasında hata oluştu: {ex.Message}</div>";
                return View(model);
            }

            if (payment.Status == "success")
            {
                await _serviceOrder.AddAsync(siparis);
                var sonuc = await _serviceOrder.SaveChangesAsync();
                if (sonuc > 0)
                {
                    HttpContext.Session.Remove("Cart");
                    return RedirectToAction("Thanks");
                }
                else
                {
                    TempData["Message"] = "<div class='alert alert-danger'>Sipariş veritabanına kaydedilemedi.</div>";
                    return View(model);
                }
            }
            else
            {
                TempData["Message"] = $"<div class='alert alert-danger'>Ödeme İşlemi Başarısız!<br/>Hata Kodu: {payment.ErrorCode}<br/>Hata Mesajı: {payment.ErrorMessage}</div>";
                return View(model);
            }

            #endregion
        }

        public IActionResult Thanks()
        {
            return View();
        }

        private CartService GetCart()
        {
            return HttpContext.Session.GetJson<CartService>("Cart") ?? new CartService();
        }
    }
}
