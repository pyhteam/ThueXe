using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThueXe.Areas.Admin.Models.Entities
{
    public class NhanVien
    {
        public int Id { set; get; }

        public string HoVaTen { set; get; }

        public bool GioiTinh { set; get; }

        public string Email { set; get; }

        public string SDT { set; get; }

        public string SoCanCuoc { set; get; }

        public string TenDangNhap { set; get; }

        public string MatKhau { set; get; }

        public string ChucVu { set; get; }

    }
}
