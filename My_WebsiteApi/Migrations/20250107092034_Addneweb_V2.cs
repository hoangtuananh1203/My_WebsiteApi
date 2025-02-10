using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace My_WebsiteApi.Migrations
{
    public partial class Addneweb_V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FirtName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    sdt = table.Column<string>(type: "text", nullable: true),
                    diachi = table.Column<string>(type: "text", nullable: true),
                    Ngay_sinh = table.Column<DateTime>(type: "date", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "bannerIndex",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Image = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bannerIndex", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "bannerthrees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Image = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    mota = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bannerthrees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "danhmucs",
                columns: table => new
                {
                    Id_danhmuc = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ten_danhmuc = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Mota_danhmuc = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_danhmucs", x => x.Id_danhmuc);
                });

            migrationBuilder.CreateTable(
                name: "hangsxes",
                columns: table => new
                {
                    Id_hangsx = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ten_hangsx = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Diachi_hangsx = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Mota_hangsx = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hangsxes", x => x.Id_hangsx);
                });

            migrationBuilder.CreateTable(
                name: "lienHes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    noidung = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    sdt = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ngaythem = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lienHes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sanphamQuangbas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title1 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Title2 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Image = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    mota = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sanphamQuangbas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tintucs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tieude = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    noidung = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    image = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ngaythem = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tintucs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "website_Infomations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "text", nullable: false),
                    diachi = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    sdt = table.Column<string>(type: "text", nullable: false),
                    tencongty = table.Column<string>(type: "text", nullable: false),
                    mota = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_website_Infomations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "danhsach_Loves",
                columns: table => new
                {
                    Id_dslove = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_danhsach_Loves", x => x.Id_dslove);
                    table.ForeignKey(
                        name: "FK_danhsach_Loves_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "donhangs",
                columns: table => new
                {
                    Id_donhang = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Ngay_dat = table.Column<DateTime>(type: "date", nullable: false),
                    Nguoinhan = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Diachi = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    sdt = table.Column<string>(type: "text", nullable: false),
                    trangthai = table.Column<int>(type: "integer", nullable: false),
                    type_thanhtoan = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_donhangs", x => x.Id_donhang);
                    table.ForeignKey(
                        name: "FK_donhangs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "giohang",
                columns: table => new
                {
                    Id_giohang = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_giohang", x => x.Id_giohang);
                    table.ForeignKey(
                        name: "FK_giohang_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sanphams",
                columns: table => new
                {
                    Id_sanpham = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name_sanpham = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Id_danhmuc = table.Column<int>(type: "integer", nullable: false),
                    Id_hangsx = table.Column<int>(type: "integer", nullable: false),
                    mota_sp = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Mausac = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Loai = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    gia = table.Column<decimal>(type: "numeric", nullable: false),
                    soluong = table.Column<int>(type: "integer", nullable: false),
                    image1 = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    image2 = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Ngay_add = table.Column<DateTime>(type: "date", nullable: false),
                    Ngay_update = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sanphams", x => x.Id_sanpham);
                    table.ForeignKey(
                        name: "FK_sanphams_danhmucs_Id_danhmuc",
                        column: x => x.Id_danhmuc,
                        principalTable: "danhmucs",
                        principalColumn: "Id_danhmuc",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sanphams_hangsxes_Id_hangsx",
                        column: x => x.Id_hangsx,
                        principalTable: "hangsxes",
                        principalColumn: "Id_hangsx",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "lichsumuahangs",
                columns: table => new
                {
                    Id_lichsu = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id_donhang = table.Column<int>(type: "integer", nullable: false),
                    Ngaynhan = table.Column<DateTime>(type: "date", nullable: false),
                    trangthai = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lichsumuahangs", x => x.Id_lichsu);
                    table.ForeignKey(
                        name: "FK_lichsumuahangs_donhangs_Id_donhang",
                        column: x => x.Id_donhang,
                        principalTable: "donhangs",
                        principalColumn: "Id_donhang",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "danhgia_Sps",
                columns: table => new
                {
                    Id_danhgia = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Diem = table.Column<int>(type: "integer", nullable: false),
                    noidung = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Ngay_add = table.Column<DateTime>(type: "date", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Id_sanpham = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_danhgia_Sps", x => x.Id_danhgia);
                    table.ForeignKey(
                        name: "FK_danhgia_Sps_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_danhgia_Sps_sanphams_Id_sanpham",
                        column: x => x.Id_sanpham,
                        principalTable: "sanphams",
                        principalColumn: "Id_sanpham",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Item_Donhangs",
                columns: table => new
                {
                    Id_itemdonhang = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id_donhang = table.Column<int>(type: "integer", nullable: false),
                    Id_sanpham = table.Column<int>(type: "integer", nullable: false),
                    soluong = table.Column<int>(type: "integer", nullable: false),
                    gia = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item_Donhangs", x => x.Id_itemdonhang);
                    table.ForeignKey(
                        name: "FK_Item_Donhangs_donhangs_Id_donhang",
                        column: x => x.Id_donhang,
                        principalTable: "donhangs",
                        principalColumn: "Id_donhang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Item_Donhangs_sanphams_Id_sanpham",
                        column: x => x.Id_sanpham,
                        principalTable: "sanphams",
                        principalColumn: "Id_sanpham",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "item_DsLoves",
                columns: table => new
                {
                    Id_item_dslove = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id_dslove = table.Column<int>(type: "integer", nullable: false),
                    Id_sanpham = table.Column<int>(type: "integer", nullable: false),
                    Ngay_add = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_DsLoves", x => x.Id_item_dslove);
                    table.ForeignKey(
                        name: "FK_item_DsLoves_danhsach_Loves_Id_dslove",
                        column: x => x.Id_dslove,
                        principalTable: "danhsach_Loves",
                        principalColumn: "Id_dslove",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_item_DsLoves_sanphams_Id_sanpham",
                        column: x => x.Id_sanpham,
                        principalTable: "sanphams",
                        principalColumn: "Id_sanpham",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "item_Giohangs",
                columns: table => new
                {
                    Id_itemgiohang = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id_giohang = table.Column<int>(type: "integer", nullable: false),
                    Id_sanpham = table.Column<int>(type: "integer", nullable: false),
                    soluong = table.Column<int>(type: "integer", nullable: false),
                    Ngay_add = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_Giohangs", x => x.Id_itemgiohang);
                    table.ForeignKey(
                        name: "FK_item_Giohangs_giohang_Id_giohang",
                        column: x => x.Id_giohang,
                        principalTable: "giohang",
                        principalColumn: "Id_giohang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_item_Giohangs_sanphams_Id_sanpham",
                        column: x => x.Id_sanpham,
                        principalTable: "sanphams",
                        principalColumn: "Id_sanpham",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_danhgia_Sps_Id_sanpham",
                table: "danhgia_Sps",
                column: "Id_sanpham");

            migrationBuilder.CreateIndex(
                name: "IX_danhgia_Sps_UserId",
                table: "danhgia_Sps",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_danhsach_Loves_UserId",
                table: "danhsach_Loves",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_donhangs_UserId",
                table: "donhangs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_giohang_UserId",
                table: "giohang",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_Donhangs_Id_donhang",
                table: "Item_Donhangs",
                column: "Id_donhang");

            migrationBuilder.CreateIndex(
                name: "IX_Item_Donhangs_Id_sanpham",
                table: "Item_Donhangs",
                column: "Id_sanpham");

            migrationBuilder.CreateIndex(
                name: "IX_item_DsLoves_Id_dslove",
                table: "item_DsLoves",
                column: "Id_dslove");

            migrationBuilder.CreateIndex(
                name: "IX_item_DsLoves_Id_sanpham",
                table: "item_DsLoves",
                column: "Id_sanpham");

            migrationBuilder.CreateIndex(
                name: "IX_item_Giohangs_Id_giohang",
                table: "item_Giohangs",
                column: "Id_giohang");

            migrationBuilder.CreateIndex(
                name: "IX_item_Giohangs_Id_sanpham",
                table: "item_Giohangs",
                column: "Id_sanpham");

            migrationBuilder.CreateIndex(
                name: "IX_lichsumuahangs_Id_donhang",
                table: "lichsumuahangs",
                column: "Id_donhang");

            migrationBuilder.CreateIndex(
                name: "IX_sanphams_Id_danhmuc",
                table: "sanphams",
                column: "Id_danhmuc");

            migrationBuilder.CreateIndex(
                name: "IX_sanphams_Id_hangsx",
                table: "sanphams",
                column: "Id_hangsx");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "bannerIndex");

            migrationBuilder.DropTable(
                name: "bannerthrees");

            migrationBuilder.DropTable(
                name: "danhgia_Sps");

            migrationBuilder.DropTable(
                name: "Item_Donhangs");

            migrationBuilder.DropTable(
                name: "item_DsLoves");

            migrationBuilder.DropTable(
                name: "item_Giohangs");

            migrationBuilder.DropTable(
                name: "lichsumuahangs");

            migrationBuilder.DropTable(
                name: "lienHes");

            migrationBuilder.DropTable(
                name: "sanphamQuangbas");

            migrationBuilder.DropTable(
                name: "tintucs");

            migrationBuilder.DropTable(
                name: "website_Infomations");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "danhsach_Loves");

            migrationBuilder.DropTable(
                name: "giohang");

            migrationBuilder.DropTable(
                name: "sanphams");

            migrationBuilder.DropTable(
                name: "donhangs");

            migrationBuilder.DropTable(
                name: "danhmucs");

            migrationBuilder.DropTable(
                name: "hangsxes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
