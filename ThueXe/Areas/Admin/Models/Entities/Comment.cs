using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebCar.Areas.Admin.Models.Entities;

namespace ThueXe.Areas.Admin.Models.Entities
{
    public class Comment
    {
        public int CMId { set; get; }

        [Column(TypeName = "ntext")]
        public string NoiDung { set; get; }
        public string DanhGia { set; get; }
        public DateTime NgayTao { set; get; }

        //FK key 
        public int BlogId { set; get; }
        [ForeignKey("BlogId")]
        public Blog Blog { set; get; }

        public int CarId { set; get; }
        [ForeignKey("CarId")]
        public Xe Xe { set; get; }

        public int NguoiDungId { set; get; }
        [ForeignKey("NguoiDungId")]
        public NguoiDung NguoiDung { set; get; }

    }
}
