using CloudinaryDotNet.Actions;

namespace QuanLyThueTro.Services
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddImageAsync(IFormFile file);
        Task<DeletionResult> DeleteImageAsync(string tinDangId);
    }
}
