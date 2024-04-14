using PopularMuseumsAPI.Models;

namespace PopularMuseumsAPI.Services.NewsService {
    public interface INewsService {
        Task<List<News>> GetAllNews();
        Task<News> GetNewsById(int id);
        Task<News?> UpdateNews(int id, NewsDto news);
        Task<bool> DeleteNews(int id);
        Task<News> AddNews(NewsDto news);
    }
}
