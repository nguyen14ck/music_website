using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicWS.Models;
using MusicWS.ViewModels;

namespace MusicWS.Controllers
{
    public class CartController : Controller
    {
        //
        // GET: /Cart/      

        MusicDataContext db = new MusicDataContext();

        #region Xử lý liên quan đến Giỏ hàng
        private Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectToRouteResult AddToCart(int albumId, string returnUrl)
        {
            Album album = db.Albums.FirstOrDefault(p => p.AlbumId == albumId);
            if (album != null)
            {
                GetCart().Add(album, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public ViewResult Index(string returnUrl)
        {
            ViewBag.Title = "Your Cart";
            ViewBag.Message = "Giỏ hàng của bạn";
            CartIndex e = new CartIndex(GetCart(), returnUrl);
            return View(e);
        }

        public RedirectToRouteResult UpdateToCart(int albumId, string returnUrl, int SoLuong)
        {
            GetCart().Update(albumId, SoLuong);
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(int albumId, string returnUrl)
        {
            GetCart().Remove(albumId);
            return RedirectToAction("Index", new { returnUrl }); 
        }
        #endregion

        #region Xử lí phát sinh đơn đặt hàng
        [Authorize]
        public ViewResult Checkout()
        {
            ViewBag.Title = "Checkout";
            ViewBag.Message = "Nhập Thông tin của bạn!";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ViewResult Checkout(DatHang e)
        {
            Cart cart = GetCart();
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Giỏ hàng trống!");

            }
            
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Nhập thông tin của bạn";
                return View(e);
            }


            e.NgayDatHang = DateTime.Today;
            e.TenDangNhap = User.Identity.Name;
            e.TriGia = cart.ComputeTotal();
            db.DatHangs.Add(e);
            db.SaveChanges();
            foreach (var line in cart.Lines)
            {
                DatHangCt ct = new DatHangCt();
                ct.DatHangCtId = e.DatHangId;
                ct.AlbumId = line.Album.AlbumId;
                ct.SoLuong = line.SoLuong;
                ct.GiaBan = line.Album.GiaBan;
                db.DatHangCts.Add(ct);
            }
            db.SaveChanges();
            cart.Clear();
            ViewBag.Message = "Hoàn tất = Thông tin đặt hàng !";
            return View("Completed");


        }

        #endregion
    }
}
