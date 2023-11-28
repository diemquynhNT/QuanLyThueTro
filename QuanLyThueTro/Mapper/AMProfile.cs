using AutoMapper;
using QuanLyThueTro.Dto;
using QuanLyThueTro.Model;

namespace QuanLyThueTro.Mapper
{
    public class AMProfile: Profile
    {
        public AMProfile()
        {
            CreateMap<ChucVuDto, ChucVu>();
            CreateMap<LoaiTaiKhoanDto, LoaiTaiKhoan>()
             .ForMember(dest => dest.tenLoaiTK, act => act.MapFrom(src => src.tenLoaiTK))
            .ForMember(dest => dest.giaTK, act => act.MapFrom(src => src.giaTK))
            .ForMember(dest => dest.hanDung, act => act.MapFrom(src => src.hanDung));
            CreateMap<DichVuDangTinDto, GoiTinDichVu>()
            .ForMember(dest => dest.loaiDichVu, act => act.MapFrom(src => src.loaiDichVu))
            .ForMember(dest => dest.giaCa, act => act.MapFrom(src => src.giaCa))
            .ForMember(dest => dest.hanDung, act => act.MapFrom(src => src.hanDung));
            CreateMap<TinDangPhongTroWithoutId, TinDang>()
              .ForMember(dest => dest.tieuDe, act => act.MapFrom(src => src.tieuDe))
               .ForMember(dest => dest.loaiTin, act => act.MapFrom(src => src.loaiTin))
              .ForMember(dest => dest.sdtNguoiLienHe, act => act.MapFrom(src => src.sdtNguoiLienHe))
              .ForMember(dest => dest.nguoiLienHe, act => act.MapFrom(src => src.nguoiLienHe))
              .ForMember(dest => dest.doiTuongChoThue, act => act.MapFrom(src => src.doiTuongChoThue))
              .ForMember(dest => dest.idDichVu, act => act.MapFrom(src => src.idDichVu))
              .ForMember(dest => dest.idUser, act => act.MapFrom(src => src.idUser))
              .ForMember(dest => dest.soLuongPhong, act => act.MapFrom(src => src.soLuongPhong));
            CreateMap<TinDangPhongTroWithoutId, PhongTro>()
             .ForMember(dest => dest.diaChi, act => act.MapFrom(src => src.diaChi))
             .ForMember(dest => dest.giaPhong, act => act.MapFrom(src => src.giaPhong))
             .ForMember(dest => dest.dienTich, act => act.MapFrom(src => src.dienTich))
             .ForMember(dest => dest.moTa, act => act.MapFrom(src => src.moTa))
             .ForMember(dest => dest.tienNuoc, act => act.MapFrom(src => src.tienNuoc))
             .ForMember(dest => dest.tienDichVu, act => act.MapFrom(src => src.tienDichVu))
             .ForMember(dest => dest.tienDien, act => act.MapFrom(src => src.tienDien));
            CreateMap<LichXemPhongDto, LichXemPhong>();
        }
    }
}
