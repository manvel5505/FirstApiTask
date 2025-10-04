using FirstApiTask.Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FirstApiTask.Domain.interfaces
{
    public interface ICombinationService
    {
        Task<CombinationResponseDto> GenerateCombinations([FromBody] CombinationRequestDto request);
    }
}
