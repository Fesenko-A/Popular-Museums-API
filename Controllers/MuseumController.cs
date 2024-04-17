using Microsoft.AspNetCore.Mvc;
using PopularMuseumsAPI.Models.DTO;
using PopularMuseumsAPI.Services.MuseumService;
using PopularMuseumsAPI.Utility;
using System.Net;

namespace PopularMuseumsAPI.Controllers {
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MuseumController : ControllerBase {
        private readonly IMuseumService _museumService;

        public MuseumController(IMuseumService museumService) {
            _museumService = museumService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetAll(string? name) {
            var result = await _museumService.GetAllMuseums(name);
            return Ok(new ApiResponse(result));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse>> GetById(int id) {
            var result = await _museumService.GetById(id);

            if (result.Value == null) {
                return NotFound(new ApiResponse(HttpStatusCode.NotFound, false, result.Message));
            }

            return Ok(new ApiResponse(result.Value));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse>> Update(int id, MuseumDto museum) {
            var result = await _museumService.UpdateMuseum(id, museum);

            if (result.Value == null) {
                return BadRequest(new ApiResponse(HttpStatusCode.BadRequest, false, result.Message));
            }

            return Ok(new ApiResponse(result.Value));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse>> Delete(int id) {
            var result = await _museumService.DeleteMuseum(id);

            if (result.Value == false) {
                return BadRequest(new ApiResponse(HttpStatusCode.BadRequest, false, result.Message));
            }

            return Ok(new ApiResponse(result.Value));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Add([FromForm] MuseumDto museum) {
            var result = await _museumService.AddMuseum(museum);

            if (result.Value == null) {
                return BadRequest(new ApiResponse(HttpStatusCode.BadRequest, false, result.Message));
            }

            return Ok(new ApiResponse(result.Value));
        }
    }
}
