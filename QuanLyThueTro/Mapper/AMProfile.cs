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
            .ForMember(dest => dest.hanDung, act => act.MapFrom(src => src.hanDung))


;
        }
    }
}
