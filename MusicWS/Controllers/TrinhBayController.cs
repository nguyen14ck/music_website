using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MusicWS.Models;

namespace MusicWS.Controllers
{
    public class TrinhBayController : Controller
    {
        MusicDataContext db = new MusicDataContext();
        #region 1-Ca khúc trình bày theo Album
        // GET: /TrinhBay/
        // GET: /TrinhBay/1
        // GET: /TrinhBay/1/21

        public ViewResult Album(int? id,int? idTB)
        {
            List<TrinhBay> trinhBays;
            try
            {
                if (id.HasValue)
                { // GET: /TrinhBay/Album/1
                    trinhBays = db.TrinhBays.Where(p => p.AlbumId == id).Include("CaSy").Include("Album").Include("BaiHat").Include(p => p.BaiHat.TacGia).ToList();
                    ViewBag.Message = string.Format("Album: {0}", trinhBays[0].Album.TenAlbum);
                    if (idTB.HasValue)
                    {
                        TrinhBay trinhBay = trinhBays.Find(p => p.TrinhBayId == idTB);
                        ViewBag.TrinhBay = trinhBay;
                    }
                    return View(trinhBays);
                }
                // GET: /TrinhBay/Album
                ViewBag.Title = "Trình bày";
                ViewBag.Message = "Ca khúc - Album";
                trinhBays = db.TrinhBays.OrderBy(p => p.AlbumId).Include("CaSy").Include("Album").Include("BaiHat").Include(p => p.BaiHat.TacGia).ToList();
                return View(trinhBays);
            }
            catch
            {
                HandleErrorInfo error = new HandleErrorInfo(new Exception("Album không tồn tại!"), "TrinhBay", "Album");
                return View("Error", error);
            }
        }

        [ChildActionOnly]
        public ActionResult _PlayPartial(TrinhBay trinhbay)
        {
            ViewBag.TrinhBay = trinhbay;
            return PartialView();
        }

        #endregion

        #region 2-Ca khúc trình bày theo Ca sỹ
        // GET: /TrinhBay/
        // GET: /TrinhBay/2/
        public ViewResult CaSy(int? id, int? dTB)
        {
            try
            {
                if (id.HasValue)
                { // GET: /TrinhBay/CaSy/1

                    CaSy casy = db.CaSies.Where(p => p.CaSyId == id).SingleOrDefault();
                    ViewBag.Message = string.Format("Ca Sỹ:{0}", casy.TenCaSy);
                    List<TrinhBay> itemss = db.TrinhBays.Where(p => p.CaSyId == id).ToList();
                    return View(itemss);
                }
                // GET: /TrinhBay/CaSy/
                ViewBag.Message = "Ca khúc - Ca Sỹ";
                return View(db.TrinhBays.OrderBy(p => p.CaSyId).ToList());
            }
            catch
            {
                HandleErrorInfo error = new HandleErrorInfo(new Exception("Ca Sỹ không tồn tại!"), "TrinhBay", "CaSy");
                return View("Error", error);
            }
        }



        #endregion

        #region 3-Ca khúc trình bày theo tác giả
        // GET: /TrinhBay/
        // GET: /TrinhBay/3/
        public ViewResult TacGia(int? id, int? dTB)
        {
            try
            {
                if (id.HasValue)
                { // GET: /TrinhBay/TacGia/1

                    TacGia tacgia = db.TacGias.Where(p => p.TacGiaId == id).SingleOrDefault();
                    ViewBag.Message = string.Format("Tác Giả:{0}", tacgia.TenTacGia);
                    List<BaiHat> itemsss = db.BaiHats.Where(p => p.TacGiaId == id).ToList();
                    return View(itemsss);
                }
                // GET: /TrinhBay/TacGia/
                ViewBag.Message = "Ca khúc - Tác Giả";
                return View(db.BaiHats.OrderBy(p => p.TacGiaId).ToList());
            }
            catch
            {
                HandleErrorInfo error = new HandleErrorInfo(new Exception("Tác giả không tồn tại!"), "BaiHat", "TacGia");
                return View("Error", error);
            }
        }



        #endregion

        #region Search
        // 
        //GET
        [HttpGet]
        public ViewResult SearchGet(string nd)
        {
            ViewBag.Title = "Search";
            ViewBag.Message = "Kết quả search-GET";
            var cktb = db.TrinhBays.Include("CaSy").Include("Album").Include("BaiHat").Include(p => p.BaiHat.TacGia).Where(p => p.BaiHat.TenBaiHat.Contains(nd));
            return View("Search",cktb.ToList());
        }
        //
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(string nd)
        {
            ViewBag.Title = "Search";
            ViewBag.Message = "Kết quả Search-POST";
            var cktb = db.TrinhBays.Include("CaSy").Include("Album").Include("BaiHat").Include(p => p.BaiHat.TacGia).Where(p => p.BaiHat.TenBaiHat.Contains(nd));
            return View(cktb.ToList());
        }
        #endregion
    }
}
