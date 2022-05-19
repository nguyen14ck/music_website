using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using MusicWS.Models;
using System.Data.Entity;

namespace MusicWS.Controllers
{
    public class HomeController : Controller
    {
        MusicDataContext db = new MusicDataContext();   
        //
        // Hiện ra danh sách 10 bài hát được nghe nhiều nhất.
        // GET: /Home/
        public ViewResult Index(int? id)
        {
            //List<TrinhBay> items;
            IEnumerable<TrinhBay> items;
            if (id.HasValue)
            {
                TheLoai theloai = db.TheLoais.Where(p => p.TheLoaiId == id).SingleOrDefault();

                if (theloai != null)
                {
                    ViewBag.Message = theloai.TenTheLoai;
                }
                else
                {
                    ViewBag.Message = "Không thể tìm thấy thể loại đó";
                }
          
            items = db.TrinhBays.Where(p => p.BaiHat.TheLoaiId == id).Include("Album").Include("CaSy").Include(p => p.BaiHat.TacGia).ToList();
              
              
            }
            else
            {
                ViewBag.Message = "Bài hát được nghe nhiều nhất";
                // câu lệnh lấy 10 bài hát nghe nhiều nhất
                items = db.TrinhBays.OrderByDescending(p => p.LuotNghe).Take(10).Include("Album").Include("CaSy").Include(p => p.BaiHat.TacGia).ToList();
            
            }
            return View(items);
        }

        //
        // GET: /TheLoai/
        [ChildActionOnly]
        public ActionResult _TheLoaiPartial()
        {
            ViewBag.Message = "Thể Loại";
            var theloais = db.TheLoais.ToList();
            return PartialView(theloais);
        }

        //
        //


    }
}
