using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TOT.Business.Services;
using TOT.Dto.TimeOffRequests;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using TOT.Entities.IdentityEntities;
using Microsoft.AspNetCore.Authorization;


namespace TOT.Web.Controllers
{
    public class RequestController : Controller
    {
        private readonly TimeOffRequestService requestService;
        private readonly TimeOffTypeService requestTypeService;
        private readonly Interfaces.IMapper dtoMapper;
        private readonly UserManager<User> _userManager;

        public RequestController(UserManager<User> userManager, TimeOffRequestService requests, TimeOffTypeService requestTypes, Interfaces.IMapper mapper)
        {
            requestService = requests;
            dtoMapper = mapper;
            requestTypeService = requestTypes;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {

            ViewData["AvailableTypes"] = requestTypeService.GetAll().Select(t =>
            new SelectListItem() { Value = t.Id.ToString(), Text = t.Title });

            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(TimeOffRequestDTO req)
        {
            User usr = await _userManager.GetUserAsync(HttpContext.User);

            req.User = usr.Id.ToString();

            if (ModelState.IsValid)
            {
                await requestService.CreateAsync(req);
                return RedirectToAction(nameof(List));
            }

            return Create();

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var request = requestService.GetById(id);

            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TimeOffRequestDTO req)
        {
            await requestService.UpdateAsync(req);

            return RedirectToAction(nameof(List));
        }


        [HttpGet]
        public IActionResult Details(int id)
        {
            var request = requestService.GetById(id);

            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await requestService.DeleteAsync(id);

            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public IActionResult List()
        {
            var сategories = requestService.GetAll();

            return View(сategories);
        }

    }
}