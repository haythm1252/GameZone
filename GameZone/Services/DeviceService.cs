
namespace GameZone.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly ApplicationDbContext _context;
        public DeviceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SelectListItem>> GetSelectList()
        {
            return await _context.Devices
                        .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                        .OrderBy(d => d.Text)
                        .AsNoTracking()
                        .ToListAsync();
        }
    }
}
