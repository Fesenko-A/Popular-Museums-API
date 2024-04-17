using Microsoft.AspNetCore.Mvc;
using PopularMuseumsAPI.Models.DTO;
using PopularMuseumsAPI.Services.NewsService;
using PopularMuseumsAPI.Utility;
using System.Net;

namespace PopularMuseumsAPI.Controllers {
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NewsController : ControllerBase {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService) {
            _newsService = newsService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetAll() {
            var result = await _newsService.GetAllNews();
            return Ok(new ApiResponse(result));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse>> GetById(int id) {
            var result = await _newsService.GetById(id);

            if (result.Value == null) {
                return NotFound(new ApiResponse(HttpStatusCode.NotFound, false, result.Message));
            }

            return Ok(new ApiResponse(result.Value));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse>> Update(int id, NewsDto news) {
            var result = await _newsService.UpdateNews(id, news);

            if (result.Value == null) {
                return BadRequest(new ApiResponse(HttpStatusCode.BadRequest, false, result.Message));
            }

            return Ok(new ApiResponse(result.Value));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse>> Delete(int id) {
            var result = await _newsService.DeleteNews(id);

            if (result.Value == false) {
                return BadRequest(new ApiResponse(HttpStatusCode.BadRequest, false, result.Message));
            }

            return Ok(new ApiResponse(result.Value));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Add([FromForm] NewsDto news) {
            var result = await _newsService.AddNews(news);

            if (result.Value == null) {
                return BadRequest(new ApiResponse(HttpStatusCode.BadRequest, false, result.Message));
            }

            return Ok(new ApiResponse(result.Value));
        }
    }
}
