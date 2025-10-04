using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstApiTask.Application.Dto
{
    public class CombinationResponseDto
    {
        public int Id { get; set; }
        public List<List<string>> Combination { get; set; }
    }
}
