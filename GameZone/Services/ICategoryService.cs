namespace GameZone.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<SelectListItem>> GetSelectList();
    }
}
