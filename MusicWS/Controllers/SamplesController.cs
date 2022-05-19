using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicWS.Models;

namespace MusicWS.Controllers
{
    public class SamplesController : Controller
    {
        //
        // GET: /Samples/

        public ActionResult Index()
        {
            return View();
        }
        MusicDataContext db = new MusicDataContext();
        public ActionResult CustomHelper1()
        {
            ViewBag.Message = "";
            var album = db.Albums;
            return View(album.ToList());
        }
        public ActionResult CustomHelper2()
        {
            ViewBag.Message = "";
            var album = db.Albums;
            return View(album.ToList());
        }
    }
}
