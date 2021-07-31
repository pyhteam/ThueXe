using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNetCore.Http;
using ThueXe.Areas.Admin.Models.Entities;

namespace WebCar.Areas.Admin.Models.Entities
{
    public class Xe 
    {
        public int CarId { set; get; }
        [DisplayName("Tên")]
        public string NameCar { set; get; }
         [DisplayName("Hình ảnh")]
        public string ImageName { set; get; }
        
        [NotMapped]
        [DisplayName("Upload File")]
         public IFormFile ImageFile { get; set; }

        [DisplayName("Tóm tắt")]
        public string TomTat { set; get; }
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [DisplayName("Nội dung")]
        public string NoiDung { set; get; } // chi tiết xe

        [DisplayName("Giá thuê")]
        public decimal GiaThue { set; get; }
        [DisplayName("Ngày tạo")]
        public DateTime CreateDate { set; get; }

        //Add  by Van Nghia
        [DisplayName("Nội bật")]
        public bool NoiBat { get; set; }
        [DisplayName("Lượt xem")]
        public int LuotXem { get; set; }
        [DisplayName("Danh mục loại")]
        public int DanhMucId {set;get;}
        [ForeignKey("DanhMucId")]
        public DanhMuc DanhMuc { set; get; }
        public ICollection<Comment> Comments { get; set; }


    }
}
