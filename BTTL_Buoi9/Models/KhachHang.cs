using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BTTL_Buoi9.Models
{
    [MetadataType(typeof(KhachHangMetaData))]
    public partial class KhachHang
    {
        private string _NhapLaiMatKhau;

        [Required(ErrorMessage = "Vui lòng nhập lại mật khẩu")]
        public string NhapLaiMatKhau
        {
            get { return _NhapLaiMatKhau; }
            set { _NhapLaiMatKhau = value; }
        }
    }

    public class KhachHangMetaData
    {
        [Required(ErrorMessage = "Họ tên không được để trống.")]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Điện thoại không được để trống.")]
        public string DienThoai { get; set; }


        [Required(ErrorMessage = "Tên đăng nhập không được để trống.")]
        public string TaiKhoan { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        public string MatKhau { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày sinh.")]
        public DateTime NgaySinh { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập Email.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập địa chỉ.")]
        public string DiaChi { get; set; }
      

    }

}