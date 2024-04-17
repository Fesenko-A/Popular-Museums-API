using PopularMuseumsAPI.Models;
using PopularMuseumsAPI.Models.DTO;
using PopularMuseumsAPI.Utility;

namespace PopularMuseumsAPI.Services.MuseumService {
    public interface IMuseumService {
        Task<List<Museum>> GetAllMuseums(string? name);
        Task<ErrorOr<Museum>> GetById(int id);
        Task<ErrorOr<Museum>> UpdateMuseum(int id, MuseumDto museum);
        Task<ErrorOr<bool>> DeleteMuseum(int id);
        Task<ErrorOr<Museum>> AddMuseum(MuseumDto museum);
    }
}
