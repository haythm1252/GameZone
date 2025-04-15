using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace GameZone.ViewModels
{
    public class CreateGameViewModel : GameViewModel
    {
        [Required]
        [AllowedFileExtensions(FileSettings.AllowedExtensions)]
        [AllowedFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile Cover { get; set; } = default!;
    }
}
