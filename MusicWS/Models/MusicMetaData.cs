using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MusicWS.Models.MyAnnotation;

namespace MusicWS.Models
{
    [MetadataTypeAttribute(typeof(TheLoai.TheLoaiMetadata))]
    public partial class TheLoai
    {
        internal sealed class TheLoaiMetadata
        {
            private TheLoaiMetadata() { }
            [Display(Name = "Mã thế loại")]
            public int TheLoaiId { get; set; }

            [Display(Name = "Tên thế loại")]
            [Required(ErrorMessage = "{0} không dc để trống")]
            [StringLength(50, ErrorMessage = "{0} tối đa là {1} ký tự!")]
            public string TenTheLoai { get; set; }

        }
    }

    [MetadataTypeAttribute(typeof(TacGia.TacGiaMetadata))]
    public partial class TacGia
    {
        internal sealed class TacGiaMetadata
        {
            private TacGiaMetadata() { }
            [Display(Name = "Mã tác giả")]
            public int TacGiaId { get; set; }

            [Display(Name = "Tên tác giả")]
            [Required(ErrorMessage = "{0} không dc để trống")]
            [StringLength(50, ErrorMessage = "{0} phải từ {2} đến {1} ký tự!", MinimumLength = 2)]
            public string TenTacGia { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(CaSy.CaSyMetadata))]
    public partial class CaSy
    {
        internal sealed class CaSyMetadata
        {
            private CaSyMetadata() { }
            [Display(Name = "Mã ca sỹ")]
            public int CaSyId { get; set; }

            [Display(Name = "Tên ca sỹ")]
            [Required(ErrorMessage = "{0} không dc để trống")]
            [MaxWords(4)]
            public string TenCaSy { get; set; }

            [Display(Name = "Tiểu sử")]
            [DataType(DataType.MultilineText)]
            public string TieuSu { get; set; }

            [Display(Name = "Hình ca sĩ")]
            [StringLength(30, ErrorMessage = "{0} tối đa là {1} ký tự!")]
            public string Hinh { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(Sach.SachMetadata))]
    public partial class Sach
    {
        internal sealed class SachMetadata
        {
            private SachMetadata() { }
            [ScaffoldColumn(false)]
            [Display(Name = "Sách Id")]
            public int AlbumId { get; set; }

            [Display(Name = "Tên Sách")]
            [Required(ErrorMessage = "{0} không dc để trống")]
            [MinLength(2, ErrorMessage = "{0} tối thiểu là {1} ký tự")]
            [MaxLength(100, ErrorMessage = "{0} tối đa là {1} ký tự")]
            public string TenAlbum { get; set; }


            [Display(Name = "Ngày phát hành")]
            [Required(ErrorMessage = "{0} không dc để trống")]
            [DataType(DataType.Date, ErrorMessage = "{0} không hợp lệ")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
            public DateTime NgayPhatHanh { get; set; }

            [Display(Name = "Giá bán")]
            [Required(ErrorMessage = "{0} không dc để trống")]
            [Range(0, 2000000, ErrorMessage = "{0} phải từ {1} đến {2}")]
            [DataType(DataType.Currency)]
            [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:#,##0VNĐ}")]
            [RegularExpression(@"[0-9.]+", ErrorMessage = "{0} phải nhập số")]
            public decimal GiaBan { get; set; }

            [Display(Name = "Ảnh bìa")]
            public string AnhBia { get; set; }

            [Display(Name = "Giới thiệu")]
            [DataType(DataType.MultilineText)]
            public string GioiThieu { get; set; }
        }
    }
    [MetadataTypeAttribute(typeof(BaiHat.BaiHatMetadata))]
    public partial class BaiHat
    {
        internal sealed class BaiHatMetadata
        {
            private BaiHatMetadata() { }
            [Display(Name = "Mã bài hát")]
            public int BaiHatId { get; set; }

            [Display(Name = "Tên bài hát")]
            [Required(ErrorMessage = "{0} không dc để trống")]
            [MinLength(2, ErrorMessage = "{0} tối thiểu là {1} ký tự")]
            [MaxLength(100, ErrorMessage = "{0} tối đa là {1} ký tự")]
            [MaxWords(4)]
            public string TenBaiHat { get; set; }

            [Display(Name = "Lời bài hát")]
            [DataType(DataType.MultilineText)]
            public string LoiBaiHat { get; set; }

            [Display(Name = "Tên tác giả")]
            [Required(ErrorMessage = "{0} không dc để trống")]
            public int TacGiaId { get; set; }

            [Display(Name = "Tên thể loại")]
            [Required(ErrorMessage = "{0} không dc để trống")]
            public int TheLoaiId { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(DatHang.DatHangMetadata))]
    public partial class DatHang
    {
        internal sealed class DatHangMetadata
        {
            private DatHangMetadata() { }
            [Display(Name = "Mã đặt hàng")]
            public int DatHangId { get; set; }

            [Display(Name = "Họ khách hàng")]
            [Required(ErrorMessage = "{0} không dc để trống")]
            [StringLength(50, ErrorMessage = "{0} tối đa là {1} ký tự!")]
            public string HoKhachHang { get; set; }


            [Display(Name = "Tên khách hàng")]
            [Required(ErrorMessage = "{0} không dc để trống")]
            [StringLength(16, ErrorMessage = "{0} tối đa là {1} ký tự!")]
            public string TenKhachHang { get; set; }

            [Display(Name = "Địa chỉ")]
            [Required(ErrorMessage = "{0} không dc để trống")]
            [StringLength(100, ErrorMessage = "{0} tối đa là {1} ký tự!")]
            public string DiaChi { get; set; }
        }
    }
}//nsp
