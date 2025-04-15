namespace GameZone.ViewModels
{
    public class EditGameViewModel : GameViewModel
    {
        public int Id { get; set; }

        public string? CurrentCoverPath { get; set; }

        [AllowedFileExtensions(FileSettings.AllowedExtensions)]
        [AllowedFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile? Cover { get; set; } = default!;
    }
}
