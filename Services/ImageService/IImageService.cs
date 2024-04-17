using PopularMuseumsAPI.Models;
using PopularMuseumsAPI.Models.DTO;
using PopularMuseumsAPI.Utility;

namespace PopularMuseumsAPI.Services.ImageService {
    public interface IImageService {
        Task<List<Image>> GetAllImages();
        Task<List<Image>> GetByMuseumId(int museumId);
        Task<ErrorOr<Image>> GetById (int id);
        Task<ErrorOr<Image>> UpdateImage(int id, ImageDto newImage);
        Task<ErrorOr<bool>> DeleteImage(int imageId);
        Task<ErrorOr<Image>> AddImage(ImageDto image);
    }
}
