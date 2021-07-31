using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace ThueXe.Areas.Admin.Models.Entities
{
    public class Blog
    {
        public int BlogId { set; get; }
        [DisplayName("Tiêu đề")]
        public string TieuDe { set; get; }
        [DisplayName("Hình ảnh")]
        public string Images { set; get; }
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImagesFile { set; get; } // add by Nghia
        [DisplayName("Tóm tắt")]
        public string TomTat { set; get; }
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [DisplayName("Nội dung")]

        public string NoiDung { set; get; }
        [DisplayName("Ngày tạo")]
        public DateTime NgayTao { set; get; }

        //Fk key 
        public ICollection<Comment> Comments { get; set; }     
    }
}
