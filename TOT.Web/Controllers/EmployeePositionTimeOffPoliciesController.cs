using TOT.Dto;
using TOT.Data;
using System.Linq;
using TOT.Interfaces;
using TOT.Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TOT.Web.Controllers
{
    public class EmployeePositionTimeOffPoliciesController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly EmployeePositionTimeOffPolicyService _EmployeePositionTimeOffPolicyService;

        public EmployeePositionTimeOffPoliciesController(IUnitOfWork UnitOfWork,
            EmployeePositionTimeOffPolicyService EmployeePositionTimeOffPolicyService)
        {
            _UnitOfWork = UnitOfWork;
            _EmployeePositionTimeOffPolicyService = EmployeePositionTimeOffPolicyService;
        }

        public IActionResult Index()
        {
            var PoliciesList = _EmployeePositionTimeOffPolicyService.GetAll().ToList();
            return View(PoliciesList);
        }

        public IActionResult Details(int id)
        {
            var Policy = _EmployeePositionTimeOffPolicyService.GetById(id);
            if (Policy == null)
            {
                return NotFound();
            }
            return View(Policy);
        }

        public IActionResult Create()
        {
            ViewData["Position"] = _UnitOfWork.EmployeePositions.GetAll();
            ViewData["Policy"] = _UnitOfWork.TimeOffPolicies.GetAll();
            ViewData["Type"] = _UnitOfWork.TimeOffTypes.GetAll();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm]EmployeePositionTimeOffPolicyDTO ItemDTO)
        {
            if (ModelState.IsValid)
            {
                if (ItemDTO.Approvers.Any())
                {
                    _EmployeePositionTimeOffPolicyService.CreateAsync(ItemDTO);
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["Position"] = _UnitOfWork.EmployeePositions.GetAll();
            ViewData["Policy"] = _UnitOfWork.TimeOffPolicies.GetAll();
            ViewData["Type"] = _UnitOfWork.TimeOffTypes.GetAll();

            return View(ItemDTO);
        }

        public IActionResult Edit(int id)
        {
            var ItemDTO = _EmployeePositionTimeOffPolicyService.GetById(id);
            if (ItemDTO == null)
            {
                return NotFound();
            }
            ViewData["Position"] = _UnitOfWork.EmployeePositions.GetAll();
            ViewData["Policy"] = _UnitOfWork.TimeOffPolicies.GetAll();
            ViewData["Type"] = _UnitOfWork.TimeOffTypes.GetAll();

            return View(ItemDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,
            [Bind("Id,TypeId,PolicyId,IsActive")] EmployeePositionTimeOffPolicyDTO ItemDTO)
        {
            if (id != ItemDTO.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _EmployeePositionTimeOffPolicyService.UpdateAsync(ItemDTO);
                return RedirectToAction(nameof(Index));
            }
            ViewData["Position"] = _UnitOfWork.EmployeePositions.GetAll();
            ViewData["Policy"] = _UnitOfWork.TimeOffPolicies.GetAll();
            ViewData["Type"] = _UnitOfWork.TimeOffTypes.GetAll();

            return View(ItemDTO);
        }

        public IActionResult Delete(int id)
        {
            _EmployeePositionTimeOffPolicyService.DeleteByIdAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
