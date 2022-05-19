using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity; // Bắt buộc phải thêm
using System.Web;
using System.Web.Mvc;
using MusicWS.Models;
using System.Data;

namespace MusicWS.Controllers
{
    public class BaiHatController : Controller
    {
        //
        // GET: /BaiHat/

        private MusicDataContext db = new MusicDataContext();
        public ActionResult Index()
        {

            ViewBag.Title = "Index";
            ViewBag.Message = "Danh sách bài hát";
            var baihats = db.BaiHats.Include(p => p.TacGia).ToList();
            return View(baihats.ToList());
        }

        public ActionResult List1()
        {
            ViewBag.Title = "Bài hát";
            ViewBag.Message = "Giới thiệu bài hát";
            return View(db.BaiHats.Include(p=>p.TacGia).Include(p=>p.TheLoai).ToList());
        }

        public ViewResult Detail(int id = 0)
        {
            try
            {
                ViewBag.Title = "Detail";
                ViewBag.Message = "Thông tin chi tiết";                                
                BaiHat baihat = db.BaiHats.Include(b => b.TacGia).Include(b => b.TheLoai).SingleOrDefault(p => p.BaiHatId == id);
                return View(baihat);
            }
            catch
            {
                HandleErrorInfo error = new HandleErrorInfo(new Exception("Bài hát không tồn tại!"), "BaiHat", "Detail");
                return View("Error", error);
            }
        }
        public ActionResult Create()
        {
            ViewBag.Title = "Create";
            ViewBag.Message = "Thêm bài hát";
            ViewBag.TacGiaId = new SelectList(db.TacGias, "TacGiaId", "TenTacGia");
            ViewBag.TheLoaiId = new SelectList(db.TheLoais, "TheLoaiId", "TenTheLoai");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BaiHat e)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.BaiHats.Add(e);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    HandleErrorInfo error = new HandleErrorInfo(new Exception("Bài hát không tồn tại!"), "BaiHat", "Create");
                    return View("Error", error);
                }
            }
            ViewBag.Title = "Create";
            ViewBag.Message = "Thêm bài hát";
            ViewBag.TacGiaId = new SelectList(db.TacGias, "TacGiaId", "TenTacGia", e);
            ViewBag.TheLoaiId = new SelectList(db.TheLoais, "TheLoaiId", "TenTheLoai", e);
            return View(e);
        }

        public ActionResult Edit(int id = 0)
        {
            BaiHat baihat = db.BaiHats.Find(id);
            if (baihat == null)
            {
                HandleErrorInfo error = new HandleErrorInfo(new Exception("Bài hát không tồn tại!"), "BaiHat", "Edit");
                return View("Error", error);
            }
            ViewBag.Title = "Edit";
            ViewBag.Message = "Hiệu chỉnh";
            ViewBag.TacGiaId = new SelectList(db.TacGias, "TacGiaId", "TenTacGia", baihat.TacGiaId);
            ViewBag.TheLoaiId = new SelectList(db.TheLoais, "TheLoaiId", "TenTheLoai", baihat.TheLoaiId);
            return View(baihat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BaiHat baihat)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(baihat).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    HandleErrorInfo error = new HandleErrorInfo(new Exception("Bài hát không tồn tại!"), "BaiHat", "Create");
                    return View("Error", error);
                }
            }
            ViewBag.Title = "Edit";
            ViewBag.Message = "Hiệu chỉnh";
            ViewBag.TacGiaId = new SelectList(db.TacGias, "TacGiaId", "TenTacGia", baihat.TacGiaId);
            ViewBag.TheLoaiId = new SelectList(db.TheLoais, "TheLoaiId", "TenTheLoai", baihat.TheLoaiId);
            return View(baihat);
        }

        public ActionResult Delete(int id = 0)
        {
            BaiHat baihat = db.BaiHats.Include(b => b.TacGia).SingleOrDefault(p => p.BaiHatId == id);
            if (baihat == null)
            {
                HandleErrorInfo error = new HandleErrorInfo(new Exception("Bài hát không tồn tại!"), "BaiHat", "Delete");
                return View("Error", error);
            }

            return View(baihat);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                BaiHat baihat = db.BaiHats.Find(id);
                db.BaiHats.Remove(baihat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                HandleErrorInfo error = new HandleErrorInfo(new Exception("Bài hát không hủy được!"), "BaiHat", "Delete");
                return View("Error", error);
            }

        }


        public JsonResult ValidateTenBaiHat(string TenBaiHat)
        {
            int kq = db.BaiHats.Count(p => p.TenBaiHat.Trim() == TenBaiHat);
            if (kq > 0)
            {
                return Json("Bài hát này đã có rồi!", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
