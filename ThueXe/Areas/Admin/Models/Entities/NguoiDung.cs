using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThueXe.Areas.Admin.Models.Entities
{
    public class NguoiDung
    {
        public int NguoiDungId { set; get; }

        public string HoVaTen { set; get; }

        public string Email { set; get; }

        public string SDT { set; get; }

        public string DangNhap { set; get; }

        public string PassWord { set; get; }

        public ICollection<Comment> Comments { get; set; }

    }
}
