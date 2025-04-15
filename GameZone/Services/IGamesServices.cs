namespace GameZone.Services
{
    public interface IGamesServices
    {
        Task<IEnumerable<Game>> GetAllAsync();
        Task<Game?> GetByIdAsync(int id);
        Task CreateAsync(CreateGameViewModel model);
        Task<bool> UpdateAsync(EditGameViewModel model);
        Task<bool> DeleteAsync(int id);
    }
}
