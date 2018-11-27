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
using TOT.Web.ViewModels;

namespace TOT.Web.Controllers
{
    [Authorize]
    public class RequestApprovalController : Controller
    {
        private readonly RequestApprovalService approvalService;
        private readonly Interfaces.IMapper dtoMapper;
        private readonly UserManager<User> _userManager;
        
        public RequestApprovalController(UserManager<User> userManager, RequestApprovalService approvals, Interfaces.IMapper mapper)
        {
            approvalService = approvals;
            dtoMapper = mapper;
            _userManager = userManager;
        }

        public IActionResult List()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var request = approvalService.GetRequestForApproval(id);

            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Refuse(int id, string reason)
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            await approvalService.Refuse(id, reason, currentUserId);

            return RedirectToAction(nameof(List));
        }


        [HttpPost]
        public async Task<IActionResult> Approve(int id, string reason)
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            try
            {
                await approvalService.Approve(id, reason, currentUserId);
            }
            catch (UnauthorizedAccessException e)
            {
                return RedirectToAction("Login", "Account");

            }
            return RedirectToAction(nameof(List));
        }
        
        [HttpPost]
        public async Task<IActionResult> EditComment(int id, string reason)
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            try
            {
                await approvalService.UpdateComment(id, reason, currentUserId);
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Login", "Account");
            }

            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public async Task<IActionResult> PartialAsync(int listId)
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            IEnumerable<TimeOffRequestApprovalDTO> approvals;

            ViewData["TypeOfList"] = listId;

            switch (listId)
            {
                case (int)TypeOfApprovalsList.RequestedApprovals:
                    approvals = approvalService.GetRequestedForCurrentUser(currentUserId);
                    return PartialView("_Approvals", approvals);
                case (int)TypeOfApprovalsList.RefusedApprovals:
                    approvals = approvalService.GetRefusedForCurrentUser(currentUserId);
                    return PartialView("_Approvals", approvals);
                case (int)TypeOfApprovalsList.AllMyApprovals:
                    approvals = approvalService.GetAllForCurrentUser(currentUserId);
                    return PartialView("_Approvals", approvals);
                case (int)TypeOfApprovalsList.AllApprovals:
                    approvals = approvalService.GetAll();
                    return PartialView("_Approvals", approvals);
                default:
                    approvals = null;
                    break;
            }

            return PartialView("_Approvals", approvals);
        }
    }
}