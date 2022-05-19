using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicWS.Models;

namespace MusicWS.Controllers
{
    public class TheLoaiController : Controller
    {
        #region 1-Tạo PartialView sử dụng trên trang  _layout
        //
        // GET: /TheLoai/
        [ChildActionOnly]
        public ActionResult List()
        {
            MusicDataContext db=new MusicDataContext();
            ViewBag.Message = "Thể Loại";
            var theloais = db.TheLoais.ToList();
            return PartialView(theloais);
        }
        #endregion

    }
}
