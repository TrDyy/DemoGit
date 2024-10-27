using BTTL_Buoi9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTTL_Buoi9.Controllers
{
    public class SachController : Controller
    {
        //
        // GET: /Sach/
        QuanLyBanSachDataContext qlbs = new QuanLyBanSachDataContext();
        public ActionResult ShowSach()
        {
            List<Sach> _ds = qlbs.Saches.Select(t => t).ToList<Sach>();
            return View(_ds);
        }

        public ActionResult ChiTietSach(int id)
        {
            Sach ctSach = qlbs.Saches.Where(t => t.MaSach == id).FirstOrDefault();
            if (ctSach ==null)
            {
                return HttpNotFound();
            }
            return View(ctSach);
        }

       
    }
}
