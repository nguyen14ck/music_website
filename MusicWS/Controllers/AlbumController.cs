using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using MusicWS.Models;
using MusicWS.ViewModels;

namespace MusicWS.Controllers
{
    public class AlbumController : Controller
    {
        //
        // GET: /Album/
        MusicDataContext db = new MusicDataContext();
        public ActionResult Index()
        {
            ViewBag.Title = "Index";
            ViewBag.Message = "Danh sách Album";
            return View(db.Albums.ToList());
        }

        public ViewResult Browse()
        {
            ViewBag.Title = "Browse";
            ViewBag.Message = "Album mới phát hành";
            var albums = db.Albums.OrderByDescending(p => p.NgayPhatHanh).Take(10);
            return View(albums.ToList());
        }

        public ViewResult List(int page = 1)
        {
            int PageSize = 4;
            var albums=db.Albums.OrderBy(p=>p.AlbumId).Skip((page-1)*PageSize).Take(PageSize);
            var pagingInfo=new PagingInfo{CurrentPage=page, ItemsPerPage=PageSize, TotalItems=db.Albums.Count()};
            AlbumsList model = new AlbumsList
            {
                Albums = albums,
                PagingInfo = pagingInfo
            };
            ViewBag.Title = "Album";
            ViewBag.Message = "Danh Sách - Album";
            return View(model);
        }

        public ActionResult List1()
        {
            ViewBag.Title = "Album";
            ViewBag.Message = "Giới thiệu Album";
            return View(db.Albums.ToList());
        }

        public ActionResult List2()
        {
            ViewBag.Title = "Album";
            ViewBag.Message = "Giới thiệu Album";
            return View(db.Albums.ToList());
        }

        public ViewResult Detail(int id = 0)
        {
            try
            {
                ViewBag.Title = "Detail";
                ViewBag.Message = "Thông tin chi tiết";
                Album Item = db.Albums.SingleOrDefault(p => p.AlbumId == id);
                return View(Item);
            }
            catch
            {
                HandleErrorInfo error = new HandleErrorInfo(new Exception("Album không tồn tại!"), "Album", "Detail");
                return View("Error", error);
            }
        }
        public ViewResult Create()
        {
            ViewBag.Title = "Create";
            ViewBag.Message = "Thêm Album";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Album e, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string path = "";
                    if (file != null && file.ContentLength > 0)
                    {
                        e.AnhBia = file.FileName;
                        path = Server.MapPath("~/Photos/Album/" + file.FileName);
                    }

                    db.Albums.Add(e);
                    db.SaveChanges();
                    if (path != "") file.SaveAs(path);
                    return RedirectToAction("Index");
                }
                ViewBag.Title = "Create";
                ViewBag.Message = "Thêm ca sỹ";
                return View(e);
            }
            catch
            {
                HandleErrorInfo error = new HandleErrorInfo(new Exception("Album không tồn tại!"), "Album", "Index");
                return View("Error", error);
            }
            
        }

        public ActionResult Edit(int id = 0)
        {
            try
            {
                ViewBag.Message = "Hiệu chỉnh Album";
                Album item = db.Albums.SingleOrDefault(p => p.AlbumId == id);
                return View(item);
            }
            catch
            {
                HandleErrorInfo error = new HandleErrorInfo(new Exception("Album không tồn tại!"), "Album", "Edit");
                return View("Error", error);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Album e)
        {
            Album item = db.Albums.SingleOrDefault(p => p.AlbumId == e.AlbumId);
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Hiệu chỉnh thông tin - Album";
                return View(e);
            }
            TryUpdateModel(item);
            //db.SubmitChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id = 0)
        {
            try
            {
                Album item = db.Albums.Single(p => p.AlbumId == id);
                ViewBag.Message = "Hủy thông tin Album";
                return View(item);
            }
            catch
            {
                HandleErrorInfo error = new HandleErrorInfo(new Exception("Album không tồn tại!"), "Album", "Index");
                return View("Error", error);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Album Item = db.Albums.Single(p => p.AlbumId == id);
            try
            {
                Album album = db.Albums.Find(id);
                db.Albums.Remove(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                HandleErrorInfo error = new HandleErrorInfo(new Exception(Item.TenAlbum + "không hủy dc!"), "Album", "Delete");
                return View("Error", error);
            }
        }
    }
}
