
namespace GameZone.Helpers
{
    public class FileUploadService : IFileUploadService
    {
        private readonly string _baseUploadPath;
        private readonly ILogger _logger;
        public FileUploadService(IWebHostEnvironment env, ILogger<FileUploadService> logger)
        {
            _baseUploadPath = Path.Combine(env.WebRootPath);
            _logger = logger;
        }
        public async Task<string> UploadFileAsync(IFormFile file, string targetFolder)
        {
            if (file == null || file.Length == 0)
                return null;

            try
            {
                //generate the uniqe fillname guid+extention
                string FileExt = Path.GetExtension(file.FileName).ToLower();
                string FileName = Guid.NewGuid().ToString() + FileExt;

                //check if the folder exist or not and if not create it 
                string FolderPath = Path.Combine(_baseUploadPath, targetFolder);
                if (!Directory.Exists(FolderPath))
                    Directory.CreateDirectory(FolderPath);

                //combine the folder path and file name
                string FilePath = Path.Combine(FolderPath, FileName);

                //add the file in the server
                using (var stream = new FileStream(FilePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // return to save in DB
                string relativePath = Path.Combine(targetFolder, FileName).Replace("\\", "/");
                return relativePath;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to upload file");
                throw;
            }
        }
        public bool DeleteFile(string FileName)
        {
            try
            {
                _logger.LogInformation($"_baseUploadPathBefore = '{_baseUploadPath}'");
                _logger.LogInformation($"FileNameBefore = '{FileName}'");
                var FullPath = Path.Combine(_baseUploadPath, FileName);
                _logger.LogInformation($"_baseUploadPath = '{_baseUploadPath}'");
                _logger.LogInformation($"FileName = '{FileName}'");
                _logger.LogInformation($"Combined path = '{Path.Combine(_baseUploadPath, FileName)}'");
                if (File.Exists(FullPath))
                {
                    File.Delete(FullPath);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting file");
                return false;
            }
        }

    }
}
