using Microsoft.EntityFrameworkCore;
using PopularMuseumsAPI.Data;
using PopularMuseumsAPI.Models;
using PopularMuseumsAPI.Models.DTO;
using PopularMuseumsAPI.Utility;

namespace PopularMuseumsAPI.Services.ImageService {
    public class ImageService : IImageService {
        private readonly DataContext _context;

        public ImageService(DataContext context) {
            _context = context;
        }

        public async Task<ErrorOr<Image>> AddImage(ImageDto image) {
            Image newImage = new Image {
                ImageUrl = image.ImageUrl,
                MuseumId = image.MuseumId,
            };

            Museum? museum = await _context.Museums.FindAsync(image.MuseumId);
            if (museum == null) {
                return new ErrorOr<Image>("Museum not found");
            }

            try {
                _context.Images.Add(newImage);
                await _context.SaveChangesAsync();
            } catch (DbUpdateException) {
                return new ErrorOr<Image>("Error while creating image");
            }

            Image? createdImage = GetById(newImage.ImageId).Result.Value;

            if (createdImage == null) {
                return new ErrorOr<Image>("Error while getting image");
            }

            return new ErrorOr<Image>(createdImage);
        }

        public async Task<List<Image>> GetAllImages() {
            List<Image> images = await _context.Images.Include(m => m.Museum).ToListAsync();
            return images;
        }

        public async Task<List<Image>> GetByMuseumId(int museumId) {
            List<Image> images = await _context.Images.Where(x => x.MuseumId == museumId).Include(m => m.Museum).ToListAsync();
            return new List<Image>(images);
        }

        public async Task<ErrorOr<bool>> DeleteImage(int imageId) {
            Image? image = GetById(imageId).Result.Value;

            if (image == null) {
                return new ErrorOr<bool>("Image not found");
            }

            _context.Images.Remove(image);
            await _context.SaveChangesAsync();

            return new ErrorOr<bool>(true);
        }

        public async Task<ErrorOr<Image>> GetById(int id) {
            IQueryable<Image> image = _context.Images.Where(x => x.ImageId == id).Include(m => m.Museum);

            if (image == null) {
                return new ErrorOr<Image>("Image not found");
            }

            Image result = await image.FirstOrDefaultAsync();

            return new ErrorOr<Image>(result);
        }

        public async Task<ErrorOr<Image>> UpdateImage(int id, ImageDto newImage) {
            Image? imageToUpdate = GetById(id).Result.Value;

            if (imageToUpdate == null) {
                return new ErrorOr<Image>("Image not found");
            }

            imageToUpdate.ImageUrl = newImage.ImageUrl;
            imageToUpdate.MuseumId = newImage.MuseumId;

            try {
                _context.Images.Update(imageToUpdate);
                await _context.SaveChangesAsync();
            } catch (DbUpdateException) {
                return new ErrorOr<Image>("Error while updating image");
            }

            Image? updatedImage = GetById(id).Result.Value;

            if (updatedImage == null) {
                return new ErrorOr<Image>("Error while getting image");
            }

            return new ErrorOr<Image>(updatedImage);
        }
    }
}
