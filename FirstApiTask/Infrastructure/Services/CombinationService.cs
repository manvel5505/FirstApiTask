using AutoMapper;
using FirstApiTask.Application.Dto;
using FirstApiTask.Domain.Entities;
using FirstApiTask.Domain.interfaces;
using FirstApiTask.Infrastructure.Data;
using FirstApiTask.Presentation.Middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Principal;
using System.Text.Json;

namespace FirstApiTask.Infrastructure.Services
{
    public class CombinationService : ICombinationService
    {
        private readonly StorageDbContext data;
        private readonly ICombinationGenerator generator;
        public CombinationService(StorageDbContext data, ICombinationGenerator generator)
        {
            this.data = data;
            this.generator = generator;
        }
        public async Task<CombinationResponseDto> GenerateCombinations([FromBody] CombinationRequestDto request)
        {
            if (request.Items == null || request.Length < 1 || request.Length > request.Items.Count)
            {
                throw new ArgumentException("Invalid input!");
            }

            var combinations = generator.GenerateCombinations(request.Items, request.Length);

            await using var transaction = await data.Database.BeginTransactionAsync();

            try
            {
                var requestEntity = new RequestEntity
                {
                    InputItems = JsonSerializer.Serialize(request.Items),
                    CombinationLength = request.Length,
                    CreatedAt = DateTime.UtcNow
                };

                data.Requests.Add(requestEntity);
                await data.SaveChangesAsync();

                foreach (var combo in combinations)
                {
                    var combinationEntity = new CombinationEntity
                    {
                        RequestId = requestEntity.Id,
                        Items = JsonSerializer.Serialize(combo)
                    };
                    data.Combinations.Add(combinationEntity);
                }

                await data.SaveChangesAsync();

                await transaction.CommitAsync();

                return new CombinationResponseDto
                {
                    Id = requestEntity.Id,
                    Combination = combinations
                };
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        
    }
}
