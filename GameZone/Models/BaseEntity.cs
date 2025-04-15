

namespace GameZone.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        [MaxLength(100)]
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
