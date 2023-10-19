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
        }
    }
}
