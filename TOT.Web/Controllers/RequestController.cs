﻿using System.Linq;
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

namespace TOT.Web.Controllers
{
    [Authorize]
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

            if (ModelState.IsValid)
            {
                await requestService.CreateAsync(req, usr);

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
            var currentUserid = _userManager.GetUserId(HttpContext.User);
            var requests = requestService.GetAllForCurrentUser(currentUserid);

            return View(requests);
        }

        [HttpGet]
        public async Task<IActionResult> PartialAsync(int typeId)
        {

            User user = await _userManager.GetUserAsync(HttpContext.User);

            try
            {
                var users = requestService.GetUsers(typeId, user.PositionId, _userManager);
                ViewData["Users"] = users.Select(u =>
               new SelectListItem() { Value = u.Id.ToString(), Text = u.Email });
                ViewData["AmountRequestApprovalsForRequest"] = users.Count();

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