namespace GameZone.Settings
{
    public static class FileSettings
    {
        public const string GamesFolderPath = "assets/images/games";
        public const string AllowedExtensions = ".jpg,.png,.jpeg";
        public const int MaxFileSizeInMP = 1;
        public const int MaxFileSizeInBytes = MaxFileSizeInMP * 1024 * 1024;
    }
}
