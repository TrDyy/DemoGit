using BTTL_Buoi9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTTL_Buoi9.Controllers
{
    public class NguoiDungController : Controller
    {
        //
        // GET: /NguoiDung/
        QuanLyBanSachDataContext qlbs = new QuanLyBanSachDataContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DangKy()
        {

            return View();
        }

        [HttpPost]
        public ActionResult DangKy(KhachHang kh)
        {
            if (ModelState.IsValid)
            {
                qlbs.KhachHangs.InsertOnSubmit(kh);
                qlbs.SubmitChanges();

                return RedirectToAction("DangNhap", "DangNhap");
            }
            return View();
        }
    }
}
