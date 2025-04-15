namespace GameZone.Attributes
{
    public class AllowedFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public AllowedFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if(file is not null)
            {
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult($"File size exceeds the maximum limit {FileSettings.MaxFileSizeInMP}MP.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
