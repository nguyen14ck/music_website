using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicWS.Models;

namespace MusicWS.Controllers
{
    public class CaSyController : Controller
    {
        MusicDataContext db = new MusicDataContext();
        //
        // GET: /CaSy/

        public ActionResult Index()
        {
            ViewBag.Title = "Index";
            ViewBag.Message = "Danh sách ca sy";
            return View(db.CaSies.ToList());
        }

        #region Thêm
        //
        // GET: /CaSy/Create
        public ViewResult Create()
        {
            ViewBag.Title = "Create";
            ViewBag.Message = "Thêm ca sỹ";
            return View();
        }
        //
        //// POST: /TheLoai/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CaSy e)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Title = "Create";
                ViewBag.Message = "Thêm Ca Sỹ";
                return View(e);
            }
            try
            {
                db.CaSies.Add(e);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                HandleErrorInfo error = new HandleErrorInfo(new Exception("Them Ca sy khong thanh cong !"), "CaSy", "Greate");
                return View("Error", error);
            }
        }
        #endregion
        #region Sửa
        //
        //POST:/Casy/Edit/5
        public ActionResult Edit(int id = 0)
        {
            try
            {
                ViewBag.Message = "Chinh sưa thog tin";
                CaSy item = db.CaSies.SingleOrDefault(p => p.CaSyId == id);
                return View(item);
            }
            catch
            {
                HandleErrorInfo error = new HandleErrorInfo(new Exception("Ca Sy khong tồn tại!"), "Casy", "Edit");
                return View("Error", error);
            }
        }
        #endregion
        #region Sửa Post
        //
        // POST: Casy/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CaSy e)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Hieu chinh thog tin - Casy";
                return View(e);
            }
            CaSy item = db.CaSies.SingleOrDefault(p => p.CaSyId == e.CaSyId);
            TryUpdateModel(item);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        #endregion
        #region Delete
        //GET : CaSy/Delete/5
        public ActionResult Delete(int id = 0)
        {
            try
            {
                CaSy item = db.CaSies.Single(p => p.CaSyId == id);
                ViewBag.Message = " Huy thog tin ca sy";
                return View(item);
            }
            catch
            {
                HandleErrorInfo error = new HandleErrorInfo(new Exception("CaSy Khong Ton tai!"), " CaSy", "index");
                return View("Error", error);
            }

        }
        //
        //POST : /Casy/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CaSy item = db.CaSies.Single(p => p.CaSyId == id);
            try
            {
                //db.CaSies.DeleteOnSubmit(item);
                //db.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                HandleErrorInfo error = new HandleErrorInfo(new Exception(item.TenCaSy + "khong huy duoc!"), " CaSy", "Delete");
                return View("Error", error);
            }
        }
        #endregion
        #region Details

        //
        //GET:
       /* public ActionResult Details(int id = 0)
        {
            try
            {
                ViewBag.Title = " Details";
                ViewBag.Message = "Thông tin  chi tiet";
                CaSy item = db.CaSies.SingleOrDefault(p => p.CaSyId == id);
                return View(item);
            }
            catch
            {

            }
        }*/
        #endregion
    }
}