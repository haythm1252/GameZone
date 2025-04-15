namespace GameZone.Services
{
    public interface IDeviceService
    {
        Task<IEnumerable<SelectListItem>> GetSelectList();
    }
}
