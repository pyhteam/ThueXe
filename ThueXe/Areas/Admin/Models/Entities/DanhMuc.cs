using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace WebCar.Areas.Admin.Models.Entities
{
    public class DanhMuc
    {
        public int DanhMucId { set; get; }
        [DisplayName("Tên danh mục")]
        public string NameCategory { set; get; }

        public ICollection<Xe> Xes { set; get; }
    }
}
