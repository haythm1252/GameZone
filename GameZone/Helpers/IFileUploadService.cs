namespace GameZone.Helpers
{
    public interface IFileUploadService
    {
        Task<string> UploadFileAsync(IFormFile file, string targetFolder);
        bool DeleteFile(string FileName);
    }
}
