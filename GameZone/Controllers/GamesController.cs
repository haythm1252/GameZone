using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
        public readonly ICategoryService _CategoryService;
        public readonly IDeviceService _DeviceService;
        public readonly IGamesServices _GamesService;

        public GamesController(ICategoryService categoryService,IDeviceService deviceService,IGamesServices gamesServices)
        {
            _CategoryService = categoryService;
            _DeviceService = deviceService;
            _GamesService = gamesServices;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var games = await _GamesService.GetAllAsync();
            return View(games);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var game = await _GamesService.GetByIdAsync(id);
            if (game is null)
                return NotFound();
            return View(game);
        }

        [HttpGet]        
        public async Task<IActionResult> Create()
        {
            CreateGameViewModel GameVM = new()
            {
                Categorys = await _CategoryService.GetSelectList(),
                Devices = await _DeviceService.GetSelectList()
            };
            return View(GameVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categorys = await _CategoryService.GetSelectList();
                model.Devices = await _DeviceService.GetSelectList();
                return View(model);
            }
            await _GamesService.CreateAsync(model);
            TempData["Success"] = "Game Created Successfully";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var game = await _GamesService.GetByIdAsync(id);

            if (game is null)
                return NotFound();

            var model = new EditGameViewModel()
            {
                Id = game.Id,
                Name=game.Name,
                CategoryId = game.CategoryId,
                Description = game.Description,
                CurrentCoverPath = game.Cover,
                SelectedDevices = game.Devices.Select(d => d.DeviceId).ToList(),
                Categorys = await _CategoryService.GetSelectList(),
                Devices = await _DeviceService.GetSelectList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categorys = await _CategoryService.GetSelectList();
                model.Devices = await _DeviceService.GetSelectList();
                return View(model);
            }
            if(await _GamesService.UpdateAsync(model))
            {
                TempData["Success"] = "Game Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = "Unable to Update the Game";
            return RedirectToAction(nameof(Index)); ;
        }

        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _GamesService.DeleteAsync(id);
            return isDeleted ? Ok() : BadRequest();
        }

    }
}
