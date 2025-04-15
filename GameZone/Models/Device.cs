namespace GameZone.Models
{
    public class Device : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Icon { get; set; } = string.Empty;

        public ICollection<GameDevice> Games { get; set; } = new List<GameDevice>();
    }
}
