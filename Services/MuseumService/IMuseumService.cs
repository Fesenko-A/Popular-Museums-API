using PopularMuseumsAPI.Models;

namespace PopularMuseumsAPI.Services.MuseumService {
    public interface IMuseumService {
        Task<List<Museum>> GetAllMuseums();
        Task<Museum?> GetById(int id);
        Task<List<Museum>> SearchByName(string name);
        Task<Museum?> UpdateMuseum(int id, MuseumDto museum);
        Task<bool> DeleteMuseum(int id);
        Task<Museum?> AddMuseum(MuseumDto museum);
    }
}
