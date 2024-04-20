using Microsoft.EntityFrameworkCore;
using PopularMuseumsAPI.Data;
using PopularMuseumsAPI.Models;
using PopularMuseumsAPI.Models.DTO;
using PopularMuseumsAPI.Utility;

namespace PopularMuseumsAPI.Services.NewsService {
    public class NewsService : INewsService {
        private readonly DataContext _context;

        public NewsService(DataContext context) {
            _context = context;
        }

        public async Task<ErrorOr<News>> AddNews(NewsDto newsToAdd) {
            News news = new News {
                Description = newsToAdd.Description,
                Title = newsToAdd.Title,
                DatePosted = DateTime.Now,
            };

            try {
                _context.News.Add(news);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException) {
                return new ErrorOr<News>("Error while creating news post");
            }

            News? createdNews = GetById(news.Id).Result.Value;

            if (createdNews == null) {
                return new ErrorOr<News>("Error while getting news post");
            }

            return new ErrorOr<News>(createdNews);
        }

        public async Task<ErrorOr<bool>> DeleteNews(int id) {
            News? newsToDelete = GetById(id).Result.Value;

            if (newsToDelete == null) {
                return new ErrorOr<bool>("News post not found");
            }

            _context.News.Remove(newsToDelete);
            await _context.SaveChangesAsync();

            return new ErrorOr<bool>(true);
        }

        public async Task<List<News>> GetAllNews() {
            List<News> result = await _context.News.OrderByDescending(x => x.DatePosted).ToListAsync();
            return result;
        }

        public async Task<ErrorOr<News>> GetById(int id) {
            News? news = await _context.News.FindAsync(id);

            if (news == null) {
                return new ErrorOr<News>("News post not found");
            }

            return new ErrorOr<News>(news);
        }

        public async Task<ErrorOr<News>> UpdateNews(int id, NewsDto news) {
            News? newsToUpdate = GetById(id).Result.Value;

            if (newsToUpdate == null) {
                return new ErrorOr<News>("News post not found");
            }

            newsToUpdate.Description = news.Description;
            newsToUpdate.Title = news.Title;

            try {
                _context.News.Update(newsToUpdate);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException) {
                return new ErrorOr<News>("Error while updating news post");
            }

            News? updatedNews = GetById(id).Result.Value;

            if (updatedNews == null) {
                return new ErrorOr<News>("Error while getting news post");
            }

            return new ErrorOr<News>(updatedNews);
        }

        public async Task<List<News>> GetNews(uint count) {
            List<News> result = await _context.News.OrderByDescending(x => x.DatePosted).Take((int)count).ToListAsync();
            return result;
        }
    }
}
