namespace GameZone.Services
{
    public class GamesService : IGamesServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileUploadService _fileUploadService;
        private readonly ILogger<GamesService> _logger;
        public GamesService(ApplicationDbContext context,IFileUploadService fileUploadService, ILogger<GamesService> logger)
        {
            _context = context;
            _fileUploadService = fileUploadService;
            _logger = logger;
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _context.Games
                        .Include(g => g.Category)
                        .Include(g => g.Devices)
                        .ThenInclude(g => g.Device)
                        .AsNoTracking()
                        .ToListAsync();
        }
        public async Task<Game?> GetByIdAsync(int id)
        {
            return await _context.Games
                        .Include(g => g.Category)
                        .Include(g => g.Devices)
                        .ThenInclude(g => g.Device)
                        .AsNoTracking()
                        .SingleOrDefaultAsync(g => g.Id == id);

        }
        public async Task CreateAsync(CreateGameViewModel model)
        {
            string imagePath = await _fileUploadService.UploadFileAsync(model.Cover, FileSettings.GamesFolderPath);

            Game game = new()
            {
                Name = model.Name,
                Description = model.Description,
                Cover = imagePath,
                CategoryId = model.CategoryId,
                Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList()

            };
            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();

        }

        public async Task<bool> UpdateAsync(EditGameViewModel model)
        {
            var game = await _context.Games
                .Include(g => g.Devices)
                .SingleOrDefaultAsync(g => g.Id == model.Id);

            if (game is null)
                return false;

            var CurrentImagePath = game.Cover;
            var HasNewCover = model.Cover is not null;
            var NewCoverPath = string.Empty;
            game.Name = model.Name;
            game.Description = model.Description;
            game.CategoryId = model.CategoryId;
            game.Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList();

            // Upload new cover if the user added one 
            if (HasNewCover)
            {
                NewCoverPath = await _fileUploadService.UploadFileAsync(model.Cover!, FileSettings.GamesFolderPath);
            }
            try
            { 
                if (HasNewCover)
                {
                    game.Cover = NewCoverPath;
                }
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    // delete old cover if we have a new one
                    if (HasNewCover && !string.IsNullOrEmpty(CurrentImagePath))
                    {
                        return _fileUploadService.DeleteFile(CurrentImagePath);
                    }
                    return true;
                }
                else
                {
                    //delete new cover if we created one
                    if (HasNewCover && !string.IsNullOrEmpty(NewCoverPath))
                    {
                        return _fileUploadService.DeleteFile(NewCoverPath);
                    }
                    return false;
                }
            }
            catch
            {
                // delete new cover if we created one and it didnt delted in the try
                if (HasNewCover && !string.IsNullOrEmpty(NewCoverPath))
                {
                    _fileUploadService.DeleteFile(NewCoverPath);
                }
                throw;
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var game = await _context.Games.FindAsync(id);

            if (game is null)
                return false;

            _context.Games.Remove(game);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                _fileUploadService.DeleteFile(game.Cover);
                return true;
            }
                return false;
        }
    }
}
