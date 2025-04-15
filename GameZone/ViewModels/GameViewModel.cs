using System.ComponentModel;

namespace GameZone.ViewModels
{
    public class GameViewModel
    {
        [MaxLength(100)]
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [DisplayName("Category")]
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categorys { get; set; } = Enumerable.Empty<SelectListItem>();

        [DisplayName("Supproted Devices")]
        public List<int> SelectedDevices { get; set; } = default!;
        public IEnumerable<SelectListItem> Devices { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}
