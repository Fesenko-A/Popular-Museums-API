using Microsoft.AspNetCore.Mvc;
using PopularMuseumsAPI.Models.DTO;
using PopularMuseumsAPI.Services.ImageService;
using PopularMuseumsAPI.Utility;
using System.Net;

namespace PopularMuseumsAPI.Controllers {
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ImageController : ControllerBase {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService) {
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetAll() {
            var result = await _imageService.GetAllImages();
            return Ok(new ApiResponse(result));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse>> GetByMuseumId(int id) {
            var result = await _imageService.GetByMuseumId(id);
            return Ok(new ApiResponse(result));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse>> GetById(int id) {
            var result = await _imageService.GetById(id);

            if (result.Value == null) {
                return NotFound(new ApiResponse(HttpStatusCode.NotFound, false, result.Message));
            }

            return Ok(new ApiResponse(result.Value));
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse>> Update(int id, ImageDto newImage) {
            var result = await _imageService.UpdateImage(id, newImage);

            if (result.Value == null) {
                return BadRequest(new ApiResponse(HttpStatusCode.BadRequest, false, result.Message));
            }

            return Ok(new ApiResponse(result.Value));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse>> Delete(int id) {
            var result = await _imageService.DeleteImage(id);

            if (result.Value == false) {
                return BadRequest(new ApiResponse(HttpStatusCode.BadRequest, false, result.Message));
            }

            return Ok(new ApiResponse(result.Value));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Add([FromForm] ImageDto image) {
            var result = await _imageService.AddImage(image);

            if (result.Value == null) {
                return BadRequest(new ApiResponse(HttpStatusCode.BadRequest, false, result.Message));
            }

            return Ok(new ApiResponse(result.Value));
        }
    }
}
