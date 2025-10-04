using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstApiTask.Domain.Entities
{
    public class CombinationEntity
    {
        [Key]
        public int Id { get; set; }
        public int RequestId { get; set; }
        [ForeignKey("RequestId")]
        public RequestEntity Request { get; set; }
        private string items;
        public string Items 
        {
            get => items;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(items));
                }
                items = value;
            }
        }
    }
}
