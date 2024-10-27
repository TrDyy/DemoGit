using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTTL_Buoi9.Models;

namespace BTTL_Buoi9.Controllers
{
    public class ChuDeController : Controller
    {
        //
        // GET: /ChuDe/


        QuanLyBanSachDataContext qlbs = new QuanLyBanSachDataContext();
        public ActionResult ChuDePartial()
        {
            List<ChuDe> top7 = qlbs.ChuDes.Select(cd => cd).OrderBy(cd=>cd.TenChuDe).Skip(0).Take(7).ToList<ChuDe>();
            return View(top7);
        }


        public ActionResult ShowSachTheoChuDe(int id)
        {

            List<Sach> _ds = qlbs.Saches.Where(t => t.MaChuDe == id).ToList<Sach>();
            return View(_ds);
        }
    }
}
