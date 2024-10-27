using BTTL_Buoi9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTTL_Buoi9.Controllers
{
    public class NhaXuatBanController : Controller
    {
        //
        // GET: /NhaXuatBan/
        QuanLyBanSachDataContext qlbs = new QuanLyBanSachDataContext();
        public ActionResult NhaXuatBanPartial()
        {
            List<NhaXuatBan> _dsNXB = qlbs.NhaXuatBans.Select(t => t).OrderBy(t => t.TenNXB).Skip(0).Take(7).ToList<NhaXuatBan>();
            return View(_dsNXB);
        }

        public ActionResult ShowSachTheoNXB(int id)
        {

            List<Sach> _ds = qlbs.Saches.Where(t => t.MaNXB == id).ToList<Sach>();
            return View(_ds);
        }

    }
}
