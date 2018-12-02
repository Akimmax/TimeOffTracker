using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TOT.Business.Services;
using TOT.Data.RoleInitializer;
using TOT.Dto.Identity;
using TOT.Dto.Identity.Models;
using TOT.Entities;
using TOT.Entities.IdentityEntities;
using TOT.Interfaces;
using TOT.Web.TagHelpers;
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
        private const int pageSize = 10;

        public AccountController(Interfaces.IMapper _mapper,IUnitOfWork unitOfWork, UserManager<User> userManager, SignInManager<User> signInManager,IdentityService identityService)
        {
            mapper = _mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _identityService = identityService;
        }
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Index(UserFilterModel model, UserSortState sortOrder=UserSortState.NameAsc,int page=1)
        {
            ViewData["Position"] = new SelectList(_unitOfWork.EmployeePositions.GetAll(), "Id", "Title");
            ViewData["Roles"] = new SelectList(_identityService.GetAllRoles(), "Name", "Name");
            var fireState = new Dictionary<string, bool>();
            fireState.Add("Fired",true);
            fireState.Add("Working", false);
            ViewData["FiredState"] = new SelectList(fireState,"Value","Key");

            var Users = await _identityService.GetFilteredUsersAsync(model);
            int userCount = Users.Count();
            Users = _identityService.SortUsers(Users, sortOrder);
            Users = _identityService.Pagginator(Users, page, pageSize);
            return View(new UserShowModel(){
                UserFilter = new UserFilterModel(),
                Users = mapper.Map<IEnumerable<UserDTO>,IEnumerable<UserUpdateDTO>>(Users),
                UserSortView = new UserSortViewModel(sortOrder),
                UserPageView = new UserPageViewModel(userCount,page,pageSize)
            });
        }

        [Authorize(Roles = Roles.Admin)]
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
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Details(string id)
        {
            return RedirectToAction("UserRequsts", "Request", new { id });
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Register()
        {
            ViewData["Position"] = new SelectList(_unitOfWork.EmployeePositions.GetAll(), "Id", "Title");
            ViewData["Roles"] = new SelectList(_identityService.GetAllRoles(), "Name", "Name");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Register([FromForm]UserDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _identityService.RegisterAsync(model);
                    return RedirectToAction(nameof(Index));
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
        [Authorize]
        public async Task<IActionResult> UpdatePassword()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(new UserUpdatePasswordDTO() { Id=user.Id});
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdatePassword([FromForm]UserUpdatePasswordDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _identityService.UpdatePasswordAsync(model);
                    return RedirectToAction("List","Request",new { });
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
        [Authorize(Roles = Roles.Admin)]
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
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Update([FromForm]UserUpdateDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _identityService.UpdateAsync(mapper.Map<UserUpdateDTO,UserDTO>(model));
                    return RedirectToAction(nameof(Index));
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
            if (returnUrl == null)
            {
                return View(new LoginViewModel { ReturnUrl = Url.Action("List", "Requst", new { }) });
            }
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
        [Authorize]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("List","Request");
        }
    }
}