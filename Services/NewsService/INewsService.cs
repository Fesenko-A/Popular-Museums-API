using PopularMuseumsAPI.Models;
using PopularMuseumsAPI.Models.DTO;
using PopularMuseumsAPI.Utility;

namespace PopularMuseumsAPI.Services.NewsService {
    public interface INewsService {
        Task<List<News>> GetAllNews();
        Task<List<News>> GetNews(uint count);
        Task<ErrorOr<News>> GetById(int id);
        Task<ErrorOr<News>> UpdateNews(int id, NewsDto news);
        Task<ErrorOr<bool>> DeleteNews(int id);
        Task<ErrorOr<News>> AddNews(NewsDto news);
    }
}
