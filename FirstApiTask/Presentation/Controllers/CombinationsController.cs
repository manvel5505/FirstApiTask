using FirstApiTask.Domain.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System;
using FirstApiTask.Infrastructure.Data;
using FirstApiTask.Infrastructure.Services;
using FirstApiTask.Application.Dto;

namespace FirstApiTask.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CombinationsController : ControllerBase
    {
        private readonly ICombinationService service;

        public CombinationsController(ICombinationService service)
        {
            this.service = service;
        }

        [HttpPost("generate")]
        public async Task<ActionResult<CombinationResponseDto>> GenerateCombinations([FromBody] CombinationRequestDto request)
        {
            if (request.Items == null || request.Length < 1 || request.Length > request.Items.Count)
            {
                return BadRequest("Invalid input");
            }

            var items = await service.GenerateCombinations(request);

            return Ok(items);
        }
    }
}
