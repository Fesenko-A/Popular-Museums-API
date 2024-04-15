using PopularMuseumsAPI.Models;
using PopularMuseumsAPI.Models.DTO;

namespace PopularMuseumsAPI.Services.ImageService
{
    public interface IImageService {
        Task<List<Image>> GetAllImages();
        Task<List<Image>> GetByMuseumId(int museumId);
        Task<Image?> UpdateImage(int id, ImageDto newImage);
        Task<bool> DeleteImage(int imageId);
        Task<Image?> AddImage(ImageDto image);
    }
}
