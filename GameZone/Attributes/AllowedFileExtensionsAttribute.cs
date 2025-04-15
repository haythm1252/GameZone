namespace GameZone.Attributes
{
    public class AllowedFileExtensionsAttribute : ValidationAttribute
    {
        public readonly string _extensions;
        public AllowedFileExtensionsAttribute(string AllowedExtensions)
        {
            _extensions = AllowedExtensions;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if(file is not null)
            {
                var ext = Path.GetExtension(file.FileName).ToLower();
                var extensions = _extensions.Split(',');
                if (!extensions.Contains(ext))
                {
                    return new ValidationResult($"File type is not allowed. Allowed file types are: {_extensions}");
                }
            }
            return ValidationResult.Success;
        }
    }
}
