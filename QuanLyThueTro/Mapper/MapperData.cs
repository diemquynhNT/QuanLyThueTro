using AutoMapper;
using QuanLyThueTro.Dto;
using QuanLyThueTro.Model;

namespace QuanLyThueTro.Mapper
{
    public class MapperData:Profile
    {
        public MapperData()
        {
            CreateMap<TinDangModel, TinDang>();
              //.ForMember(dest => dest.tieuDe, act => act.MapFrom(src => src.tieuDe))
              // .ForMember(dest => dest.loaiTin, act => act.MapFrom(src => src.loaiTin))
              //.ForMember(dest => dest.sdtNguoiLienHe, act => act.MapFrom(src => src.sdtNguoiLienHe))
              //.ForMember(dest => dest.nguoiLienHe, act => act.MapFrom(src => src.nguoiLienHe))
              //.ForMember(dest => dest.doiTuongChoThue, act => act.MapFrom(src => src.doiTuongChoThue))
              //.ForMember(dest => dest.soLuongPhong, act => act.MapFrom(src => src.soLuongPhong))
              //.ForMember(dest => dest.idKhuVuc, act => act.MapFrom(src => src.idKhuVuc));

            CreateMap<TinDangModel, PhongTro>()
              .ForMember(dest => dest.diaChi, act => act.MapFrom(src => src.diaChi))
              .ForMember(dest => dest.giaPhong, act => act.MapFrom(src => src.giaPhong))
              .ForMember(dest => dest.dienTich, act => act.MapFrom(src => src.dienTich))
              .ForMember(dest => dest.moTa, act => act.MapFrom(src => src.moTa))
              .ForMember(dest => dest.tienNuoc, act => act.MapFrom(src => src.tienNuoc))
              .ForMember(dest => dest.tienDichVu, act => act.MapFrom(src => src.tienDichVu))
              .ForMember(dest => dest.tienDien, act => act.MapFrom(src => src.tienDien));
            CreateMap<TinDang, TinDangVM>()
              .ForMember(dest => dest.idDichVu, act => act.MapFrom(src => src.idDichVu))
              .ForMember(dest => dest.loaiTin, act => act.MapFrom(src => src.loaiTin))
              .ForMember(dest => dest.tieuDe, act => act.MapFrom(src => src.tieuDe))
              .ForMember(dest => dest.ngayBatDau, act => act.MapFrom(src => src.ngayBatDau))
              .ForMember(dest => dest.ngayKetThuc, act => act.MapFrom(src => src.ngayKetThuc))
              .ForMember(dest => dest.ngayTaoTin, act => act.MapFrom(src => src.ngayTaoTin))
              .ForMember(dest => dest.idDichVu, act => act.MapFrom(src => src.idDichVu))
              .ForMember(dest => dest.sdtNguoiLienHe, act => act.MapFrom(src => src.sdtNguoiLienHe))
              .ForMember(dest => dest.nguoiLienHe, act => act.MapFrom(src => src.nguoiLienHe))
              .ForMember(dest => dest.doiTuongChoThue, act => act.MapFrom(src => src.doiTuongChoThue))
              .ForMember(dest => dest.soLuongPhong, act => act.MapFrom(src => src.soLuongPhong))
              .ForMember(dest => dest.idKhuVuc, act => act.MapFrom(src => src.idKhuVuc));


            CreateMap<PhongTro, TinDangVM>()
              .ForMember(dest => dest.diaChi, act => act.MapFrom(src => src.diaChi))
              .ForMember(dest => dest.giaPhong, act => act.MapFrom(src => src.giaPhong))
              .ForMember(dest => dest.dienTich, act => act.MapFrom(src => src.dienTich))
              .ForMember(dest => dest.moTa, act => act.MapFrom(src => src.moTa))
              .ForMember(dest => dest.tienNuoc, act => act.MapFrom(src => src.tienNuoc))
              .ForMember(dest => dest.tienDichVu, act => act.MapFrom(src => src.tienDichVu))
              .ForMember(dest => dest.tienDien, act => act.MapFrom(src => src.tienDien));

            CreateMap<UserModel, Users>();
            CreateMap<UsersUpdateVM, Users>();


        }
    }
}
