
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TOT.Business.Services;
using TOT.Dto.TimeOffRequests;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using TOT.Entities.IdentityEntities;
using Microsoft.AspNetCore.Authorization;
using System;
using TOT.Business.Exceptions;
using System.Collections.Generic;
using TOT.Data.RoleInitializer;
using TOT.Dto.Identity.Models;

namespace TOT.Web.Controllers
{
    [Authorize]
    public class RequestController : Controller
    {
        private readonly EmployeePositionTimeOffPolicyService employeePositionTimeOffPolicyService;
        private readonly TimeOffRequestService requestService;
        private readonly TimeOffTypeService requestTypeService;
        private readonly Interfaces.IMapper dtoMapper;
        private readonly UserManager<User> _userManager;

        private static TimeOffRequestDTO TransferringRequestToEdit;

        public RequestController(UserManager<User> userManager, TimeOffRequestService requests, TimeOffTypeService requestTypes,
            EmployeePositionTimeOffPolicyService _employeePositionTimeOffPolicyService, Interfaces.IMapper mapper)
        {
            requestService = requests;
            dtoMapper = mapper;
            requestTypeService = requestTypes;
            employeePositionTimeOffPolicyService = _employeePositionTimeOffPolicyService;
            _userManager = userManager;
        }

        [HttpGet]
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
            User curentUser = await _userManager.GetUserAsync(HttpContext.User);

            if (ModelState.IsValid)
            {
                await requestService.CreateAsync(req, curentUser);

                return RedirectToAction(nameof(List));
            }

            return Create();
        }

        [HttpGet]
        public IActionResult EditGetRequest(int id)
        {
            TransferringRequestToEdit = requestService.GetById(id, true);

            return RedirectToAction("Edit");
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var request = TransferringRequestToEdit;

            if (requestService.IfApprovedAtLeastOnce(request.Id))
            {
                return View("EditName", request);
            }
            else
            {
                ViewData["AvailableTypes"] = requestTypeService.GetAll().Select(t =>
                new SelectListItem() { Value = t.Id.ToString(), Text = t.Title });

                return View("Edit", request);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TimeOffRequestDTO req)
        {
            if (ModelState.IsValid)
            {
                User curentUser = await _userManager.GetUserAsync(HttpContext.User);
                await requestService.UpdateAsync(req, curentUser);
                return RedirectToAction(nameof(List));
            }

            return Edit();
        }

        [HttpPost]
        public async Task<IActionResult> EditOnlyName(TimeOffRequestDTO req)
        {
            User curentUser = await _userManager.GetUserAsync(HttpContext.User);
            await requestService.UpdateAsync(req, curentUser);
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
        public IActionResult List(TimeOffRequestFilterModel model)
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);

            var requests = requestService.GetAllForCurrentUserFilter(currentUserId, model);

            ViewData["AvailableTypes"] = requestTypeService.GetAll().Select(t =>
               new SelectListItem() { Value = t.Id.ToString(), Text = t.Title });

            ViewData["AvailableStatuses"] = requestService.GetRequestStatuses().Select(t =>
               new SelectListItem() { Value = t.Id.ToString(), Text = t.Title });

            return View("List", new RequestShowModel()
            {
                RequestFilter = new TimeOffRequestFilterModel(),
                Requests = requests
            });
        }

        [HttpGet]
        [ActionName(name: "UserRequsts")]
        [Authorize(Roles = Roles.Admin)]     
        public IActionResult UserRequstsList(TimeOffRequestFilterModel model, string id)
        {
            var requests = requestService.GetAllForCurrentUserFilter(id, model);

                ViewData["OnlyShow"] = true;

            ViewData["AvailableTypes"] = requestTypeService.GetAll().Select(t =>
               new SelectListItem() { Value = t.Id.ToString(), Text = t.Title });

            ViewData["AvailableStatuses"] = requestService.GetRequestStatuses().Select(t =>
               new SelectListItem() { Value = t.Id.ToString(), Text = t.Title });

            return View("List", new RequestShowModel()
            {
                RequestFilter = new TimeOffRequestFilterModel(),
                Requests = requests
            });
        }

        [HttpGet]
        public async Task<IActionResult> PartialAsync(int typeId)
        {
            User curentUser = await _userManager.GetUserAsync(HttpContext.User);

            try
            {
                var userslists = await requestService.GetUsersAsync(typeId, curentUser.PositionId, _userManager);

                List<IEnumerable<SelectListItem>> listOfUserSelectLists = new List<IEnumerable<SelectListItem>>();

                foreach (var users in userslists)
                {
                    listOfUserSelectLists.Add(users.Select(u =>
                    new SelectListItem() { Value = u.Id.ToString(), Text = u.Email }));
                }

                ViewData["Users"] = listOfUserSelectLists;

                ViewData["AmountRequestApprovalsForRequest"] = listOfUserSelectLists.Count();

            }
            catch (Exception e)
            {
                if (e is ApprovalsNotFoundException || e is EntityNotFoundException)
                {
                    ViewData["Eror message"] = e.Message;
                }
            }

            return PartialView("_RequestApprovals");

        }

    }
}
