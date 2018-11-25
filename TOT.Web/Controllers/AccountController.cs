using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;
using TOT.Business.Services;
using TOT.Dto.Identity;
using TOT.Entities;
using TOT.Entities.IdentityEntities;
using TOT.Interfaces;
using TOT.Web.ViewModels;

namespace TOT.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IdentityService _identityService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Interfaces.IMapper mapper;

        public AccountController(Interfaces.IMapper _mapper,IUnitOfWork unitOfWork, UserManager<User> userManager, SignInManager<User> signInManager,IdentityService identityService)
        {
            mapper = _mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _identityService = identityService;
        }

        public async Task<IActionResult> Index()
        {
            var Users = await _identityService.GetAllUsersAsync();
            return View(Users);
        }

        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _identityService.DeleteUserAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            return RedirectToAction("UserRequsts", "Request", new { id });
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewData["Position"] = new SelectList(_unitOfWork.EmployeePositions.GetAll(), "Id", "Title");
            ViewData["Roles"] = new SelectList(_identityService.GetAllRoles(), "Name", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm]UserDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _identityService.RegisterAsync(model);
                    return RedirectToAction("Index", "Home");
                }

                ViewData["Position"] = new SelectList(_unitOfWork.EmployeePositions.GetAll(), "Id", "Title");
                ViewData["Roles"] = new SelectList(_identityService.GetAllRoles(), "Name", "Name");
                ViewData["Error"] = "Model is not correct";

                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewData["Position"] = new SelectList(_unitOfWork.EmployeePositions.GetAll(), "Id", "Title");
                ViewData["Roles"] = new SelectList(_identityService.GetAllRoles(), "Name", "Name");
                ViewData["Error"] = "Unexpected error";
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdatePassword()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(new UserUpdatePasswordDTO() { Id=user.Id});
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePassword([FromForm]UserUpdatePasswordDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _identityService.UpdatePasswordAsync(model);
                    return RedirectToAction("Index", "Home");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var user = await _identityService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            ViewData["Position"] = new SelectList(_unitOfWork.EmployeePositions.GetAll(), "Id", "Title");
            ViewData["Roles"] = new SelectList(_identityService.GetAllRoles(), "Name", "Name");
            return View(mapper.Map<UserDTO,UserUpdateDTO>(user));
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm]UserUpdateDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _identityService.UpdateAsync(mapper.Map<UserUpdateDTO,UserDTO>(model));
                    return RedirectToAction("Index", "Home");
                }

                ViewData["Position"] = new SelectList(_unitOfWork.EmployeePositions.GetAll(), "Id", "Title");
                ViewData["Roles"] = new SelectList(_identityService.GetAllRoles(), "Name", "Name");
                ViewData["Error"] = "Model is not correct";

                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewData["Position"] = new SelectList(_unitOfWork.EmployeePositions.GetAll(), "Id", "Title");
                ViewData["Roles"] = new SelectList(_identityService.GetAllRoles(), "Name", "Name");
                ViewData["Error"] = "Unexpected error";
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Wrong login or password");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}