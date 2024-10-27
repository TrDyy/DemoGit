using BTTL_Buoi9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTTL_Buoi9.Controllers
{
    public class GioHangController : Controller
    {
        //
        // GET: /GioHang/
        QuanLyBanSachDataContext db = new QuanLyBanSachDataContext();
        
        public List<GioHang> LayGioHang()
        {
            List<GioHang> _ds = Session["GioHang"] as List<GioHang>;
            if(_ds ==null)
            {
                _ds = new List<GioHang>();
                Session["GioHang"] = _ds;
            }
            return _ds;
        }

        public ActionResult ThemGioHang(int ms, string strUrl)
        {
            List<GioHang> _ds = LayGioHang();
            GioHang sp = _ds.Find(t => t.iMaSach == ms);
            if(sp == null)
            {
                sp = new GioHang(ms);
                _ds.Add(sp);
                return Redirect(strUrl);
            }
            else
            {
                sp.iSoLuong++;
                return Redirect(strUrl);
            }
          
        }

        public ActionResult GioHangPartial()
        {
            if(Session["GioHang"] ==null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> _ds = LayGioHang();
            ViewBag.TongSoLuong = TongSL();
            ViewBag.ThanhTien = TongThanhTien();
            return View(_ds);
        }

        public ActionResult XoaGioHang(int MaSP)
        {
            List<GioHang> _ds = LayGioHang();
            GioHang sp = _ds.Single(t => t.iMaSach == MaSP);
            if(sp !=null)
            {
                _ds.RemoveAll(t => t.iMaSach == MaSP);
                return RedirectToAction("GioHangPartial", "GioHang");
            }
            if(_ds.Count==0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction( "GioHangPartial","GioHang");
        }

        public ActionResult XoaAllGioHang()
        {
            List<GioHang> _ds = LayGioHang();
            _ds.Clear();
           return RedirectToAction("Index", "Home");
        }

        
        public ActionResult CapNhatGioHang(int MaSP, FormCollection f)
        {
            List<GioHang> _ds = LayGioHang();
            GioHang sp = _ds.Single(t => t.iMaSach == MaSP);
            if(sp !=null)
            {
                string txtsp = "txtSoLuong_" + MaSP.ToString();
                sp.iSoLuong = int.Parse(f[txtsp].ToString());
            }
            return RedirectToAction("GioHangPartial", "GioHang");

        }

        [HttpPost]
        public ActionResult Checkout()
        {
            return RedirectToAction("DangKy", "NguoiDung");
        }


        private int TongSL()
        {
            int sum = 0;
            List<GioHang> _ds = Session["GioHang"] as List<GioHang>;
            if(_ds !=null)
            {
                sum = _ds.Sum(t => t.iSoLuong);
            }
            return sum;
        }

        private double TongThanhTien()
        {
            double sum = 0;
            List<GioHang> _ds = Session["GioHang"] as List<GioHang>;
            if (_ds != null)
            {
                sum = _ds.Sum(t => t.dThanhTien);
            }
            return sum;
        }

    }
}
