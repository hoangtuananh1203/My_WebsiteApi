using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace My_WebsiteApi.Data
{
    public class MyDbcontext :IdentityDbContext<ApplicationUser>
    {
        public MyDbcontext(DbContextOptions<MyDbcontext> options):base(options) { }

        #region Dbset
        public DbSet<Danhgia_sp> danhgia_Sps { get; set; }
        public DbSet<Danhmuc> danhmucs { get; set; }
        public DbSet<Danhsach_love> danhsach_Loves { get; set; }
        public DbSet<Donhang> donhangs { get; set; }
        public DbSet<Giohang> giohang { get; set; }
        public DbSet<Hangsx> hangsxes { get; set; }
        public DbSet<Item_donhang> Item_Donhangs {  get; set; }
        public DbSet<Item_dsLove> item_DsLoves { get; set; }
        public DbSet<Item_giohang> item_Giohangs { get; set; }
        public DbSet<Lichsumuahang> lichsumuahangs { get; set; }
        public DbSet<Sanpham> sanphams { get; set; }
       
        public DbSet<Website_infomation> website_Infomations { get; set; }
        public DbSet<BannerIndex> bannerIndex { get; set; }
        public DbSet<Bannerthree> bannerthrees { get; set; }
        public DbSet<SanphamQuangba> sanphamQuangbas { get; set; }
        public DbSet<Tintuc> tintucs { get; set; }
        public DbSet<LienHe> lienHes { get; set; }

        #endregion



    }
}
