using Azure.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstApiTask.Application.Dto
{
    public class CombinationRequestDto
    {
        [Required]
        public List<int> Items { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Length { get; set; }
    }
}
