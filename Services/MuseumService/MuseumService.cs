using Microsoft.EntityFrameworkCore;
using PopularMuseumsAPI.Data;
using PopularMuseumsAPI.Models;
using PopularMuseumsAPI.Models.DTO;
using PopularMuseumsAPI.Utility;

namespace PopularMuseumsAPI.Services.MuseumService
{
    public class MuseumService : IMuseumService {
        private readonly DataContext _context;

        public MuseumService(DataContext context) {
            _context = context;
        }

        public async Task<ErrorOr<Museum>> AddMuseum(MuseumDto museum) {
            Museum newMuseum = new Museum {
                Name = museum.Name,
                City = museum.City,
                Country = museum.Country,
                VisitorsPerYear = museum.VisitorsPerYear,
                WebsiteLink = museum.WebsiteLink,
                ImageUrl = museum.ImageUrl,
                Description = museum.Description,
                FoundationYear = museum.FoundationYear,
            };

            try {
                _context.Add(newMuseum);
                await _context.SaveChangesAsync();
            } catch (DbUpdateException) {
                return new ErrorOr<Museum>("Error while creating museum");
            }

            Museum? createdMuseum = GetById(newMuseum.Id).Result.Value;

            if (createdMuseum == null) {
                return new ErrorOr<Museum>("Error while getting museum");
            }

            return new ErrorOr<Museum>(createdMuseum);
        }

        public async Task<ErrorOr<bool>> DeleteMuseum(int id) {
            Museum? museumToDelete = GetById(id).Result.Value;

            if (museumToDelete == null) {
                return new ErrorOr<bool>("Museum not found");
            }

            _context.Museums.Remove(museumToDelete);
            await _context.SaveChangesAsync();

            return new ErrorOr<bool>(true);
        }

        public async Task<List<Museum>> GetAllMuseums(string? name) {
            IQueryable<Museum> museums = _context.Museums.AsQueryable();

            if (!string.IsNullOrEmpty(name)) {
                museums = museums.Where(x => x.Name == name);
            }

            var result = await museums.ToListAsync();
            return result;
        }

        public async Task<ErrorOr<Museum>> GetById(int id) {
            Museum? museum = await _context.Museums.FindAsync(id);

            if (museum == null) {
                return new ErrorOr<Museum>("Museum not found");
            }

            return new ErrorOr<Museum>(museum);
        }

        public async Task<ErrorOr<Museum>> UpdateMuseum(int id, MuseumDto museum) {
            Museum? museumToUpdate = GetById(id).Result.Value;

            if (museumToUpdate == null) {
                return new ErrorOr<Museum>("Museum not found");
            }

            museumToUpdate.City = museum.City;
            museumToUpdate.Description = museum.Description;
            museumToUpdate.Name = museum.Name;
            museumToUpdate.VisitorsPerYear = museum.VisitorsPerYear;
            museumToUpdate.ImageUrl = museum.ImageUrl;
            museumToUpdate.Country = museum.Country;
            museumToUpdate.WebsiteLink = museum.WebsiteLink;
            museumToUpdate.FoundationYear = museum.FoundationYear;

            try {
                _context.Update(museumToUpdate);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException) {
                return new ErrorOr<Museum>("Error while updating museum");
            }

            Museum? updatedMuseum = GetById(id).Result.Value;

            if (updatedMuseum == null) {
                return new ErrorOr<Museum>("Error while getting museum");
            }

            return new ErrorOr<Museum>(updatedMuseum);
        }
    }
}
