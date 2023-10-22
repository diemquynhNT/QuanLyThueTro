using AutoMapper;
using QuanLyThueTro.Dto;
using QuanLyThueTro.Model;

namespace QuanLyThueTro.Mapper
{
    public class MapperData:Profile
    {
        public MapperData()
        {
            CreateMap<TinDangVM, TinDang>()
              .ForMember(dest => dest.tieuDe, act => act.MapFrom(src => src.tieuDe))
              .ForMember(dest => dest.sdtNguoiLienHe, act => act.MapFrom(src => src.sdtNguoiLienHe))
              .ForMember(dest => dest.nguoiLienHe, act => act.MapFrom(src => src.nguoiLienHe))
              .ForMember(dest => dest.doiTuongChoThue, act => act.MapFrom(src => src.doiTuongChoThue))
              .ForMember(dest => dest.soLuongPhong, act => act.MapFrom(src => src.soLuongPhong))
              .ForMember(dest => dest.idKhuVuc, act => act.MapFrom(src => src.idKhuVuc));

        }
    }
}
