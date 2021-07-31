using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThueXe.Areas.Admin.Models.Entities;
using WebCar.Areas.Admin.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;

namespace WebCar.Areas.Admin.Models.EF
{
    public class CarWebDbContext : DbContext
    {
        public CarWebDbContext( DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure using Fluent API
            modelBuilder.Entity<Xe>(entity=>
            {
                entity.HasKey(e => e.CarId);
                entity.ToTable("Xe");
                // entity.Property(e=>e.Image).HasMaxLength(100);
                entity.Property(e => e.TomTat).HasMaxLength(1000);
                entity.Property(e => e.NoiDung).HasMaxLength(100000);
            });
            modelBuilder.Entity<DanhMuc>(entity =>
            {
                entity.HasKey(e => e.DanhMucId);
                entity.ToTable("DanhMuc");
                entity.Property(e => e.NameCategory).HasMaxLength(500);
            });
            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.HasKey(e => e.NguoiDungId);
                entity.ToTable("NguoiDung");
                entity.Property(e => e.HoVaTen).HasMaxLength(50);
                entity.Property(e => e.Email).HasMaxLength(50);
                entity.Property(e => e.SDT).HasMaxLength(15);
                entity.Property(e => e.DangNhap).HasMaxLength(20);
                entity.Property(e => e.PassWord).HasMaxLength(30);
            });
            modelBuilder.Entity<NhanVien>(entity => 
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("NhanVien");
                entity.Property(e => e.HoVaTen).HasMaxLength(100);
                entity.Property(e => e.GioiTinh).HasMaxLength(10);
                entity.Property(e => e.Email).HasMaxLength(30);
                entity.Property(e => e.SDT).HasMaxLength(15);
                entity.Property(e => e.SoCanCuoc).HasMaxLength(20);
                entity.Property(e => e.TenDangNhap).HasMaxLength(20);
                entity.Property(e => e.MatKhau).HasMaxLength(20);
                entity.Property(e => e.ChucVu).HasMaxLength(50);
            });
            modelBuilder.Entity<Comment>(entity => 
            {
                entity.HasKey(e => e.CMId);
                entity.ToTable("Comment");
                entity.Property(e => e.NoiDung).HasMaxLength(100000);
                entity.Property(e => e.DanhGia).HasMaxLength(100);
            });
            modelBuilder.Entity<Blog>(entity => 
            {
                entity.HasKey(e => e.BlogId);
                entity.ToTable("Blog");
                entity.Property(e => e.Images).HasMaxLength(100);
                entity.Property(e => e.TomTat).HasMaxLength(1000);
                entity.Property(e => e.NoiDung).HasMaxLength(100000);
            });
            //base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Xe> Xe { set; get; }
        public virtual DbSet<DanhMuc> CategoriesCar { set; get; }
        public virtual DbSet<NhanVien> NhanVien { set; get; }
        public virtual DbSet<NguoiDung> NguoiDung { set; get; }
        public virtual DbSet<Comment> Comments { set; get; }
        public virtual DbSet<Blog> Blogs { set; get; }

        
    }
}
