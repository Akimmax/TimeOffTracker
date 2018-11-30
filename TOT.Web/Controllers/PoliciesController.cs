using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TOT.Entities;
using TOT.Interfaces;
using TOT.Business.Services;
using TOT.Dto.TimeOffPolicies;
using TOT.Business.Exceptions;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
using TOT.Data.RoleInitializer;

namespace TOT.Web.Controllers
{
    [Authorize]
    public class PoliciesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _UnitOfWork;
        private readonly EmployeePositionTimeOffPolicyService _EmployeePositionTimeOffPolicyService;

        public PoliciesController(IUnitOfWork UnitOfWork, IMapper mapper,
            EmployeePositionTimeOffPolicyService EmployeePositionTimeOffPolicyService)
        {
            _mapper = mapper;
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

        [Authorize(Roles = Roles.Admin)]
        public IActionResult Create()
        {
            ViewData["Type"] = new SelectList(_UnitOfWork.TimeOffTypes.GetAll(), "Id", "Title");
            ViewData["Position"] = new SelectList(_UnitOfWork.EmployeePositions.GetAll(), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Create([FromForm]PolicyCreateModel ItemCreateModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var Approvers = GetApproversFromJson(ItemCreateModel.Approvers);
                    if (Approvers.Count < 1)
                    {
                        ViewData["Type"] = new SelectList(_UnitOfWork.TimeOffTypes.GetAll(), "Id", "Title");
                        ViewData["Position"] = new SelectList(_UnitOfWork.EmployeePositions.GetAll(), "Id", "Title");
                        ViewData["Error"] = "There should be at least 1 approver";
                        return View(ItemCreateModel);
                    }
                    var ItemDTO = _mapper.Map<PolicyCreateModel, EmployeePositionTimeOffPolicyDTO>(ItemCreateModel);
                    ItemDTO.Approvers = Approvers;
                    await _EmployeePositionTimeOffPolicyService.CreateAsync(ItemDTO);
                    return RedirectToAction(nameof(Index));
                }
                ViewData["Type"] = new SelectList(_UnitOfWork.TimeOffTypes.GetAll(), "Id", "Title");
                ViewData["Position"] = new SelectList(_UnitOfWork.EmployeePositions.GetAll(), "Id", "Title");
                ViewData["Error"] = "Invalid model";

                return View(ItemCreateModel);
            }
            catch (Exception ex)
            {
                ViewData["Type"] = new SelectList(_UnitOfWork.TimeOffTypes.GetAll(), "Id", "Title");
                ViewData["Position"] = new SelectList(_UnitOfWork.EmployeePositions.GetAll(), "Id", "Title");
                if (ex is ArgumentException || ex is ArgumentNullException || ex is EntityNotFoundException)
                {
                    ViewData["Error"] = ex.Message;
                }
                else
                {
                    ViewData["Error"] = "Unexpected error";
                }
                return View(ItemCreateModel);
            }
        }

        [Authorize(Roles = Roles.Admin)]
        public IActionResult Edit(int id)
        {
            var ItemDTO = _EmployeePositionTimeOffPolicyService.GetById(id);
            if (ItemDTO == null)
            {
                return NotFound();
            }
            ViewData["Type"] = new SelectList(_UnitOfWork.TimeOffTypes.GetAll(), "Id", "Title");
            ViewData["Position"] = new SelectList(_UnitOfWork.EmployeePositions.GetAll(), "Id", "Title");

            return View(_mapper.Map<EmployeePositionTimeOffPolicyDTO,PolicyCreateModel>(ItemDTO));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Edit(int id, PolicyCreateModel ItemCreateModel)
        {
            try
            {
                if (id != ItemCreateModel.Id)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    if (_UnitOfWork.EmployeePositionTimeOffPolicies
                        .Find(x => x.PositionId == ItemCreateModel.Position.Id &&
                        x.TypeId == ItemCreateModel.Type.Id &&
                        x.IsActive ==true &&
                        x.Id != ItemCreateModel.Id) != null)
                    {
                        ViewData["Type"] = new SelectList(_UnitOfWork.TimeOffTypes.GetAll(), "Id", "Title");
                        ViewData["Position"] = new SelectList(_UnitOfWork.EmployeePositions.GetAll(), "Id", "Title");
                        ViewData["Error"] = "Policy for this Position and Type alredy exist.";
                        return View(ItemCreateModel);
                    }
                    var Approvers = GetApproversFromJson(ItemCreateModel.Approvers);
                    if (Approvers.Count < 1)
                    {
                        ViewData["Type"] = new SelectList(_UnitOfWork.TimeOffTypes.GetAll(), "Id", "Title");
                        ViewData["Position"] = new SelectList(_UnitOfWork.EmployeePositions.GetAll(), "Id", "Title");
                        ViewData["Error"] = "There should be at least 1 approver";
                        return View(ItemCreateModel);
                    }
                    var ItemDTO = _mapper.Map<PolicyCreateModel, EmployeePositionTimeOffPolicyDTO>(ItemCreateModel);
                    ItemDTO.Approvers = Approvers;
                    await _EmployeePositionTimeOffPolicyService.UpdateAsync(ItemDTO);
                    return RedirectToAction(nameof(Index));
                }
                ViewData["Type"] = new SelectList(_UnitOfWork.TimeOffTypes.GetAll(), "Id", "Title");
                ViewData["Position"] = new SelectList(_UnitOfWork.EmployeePositions.GetAll(), "Id", "Title");
                ViewData["Error"] = "Invalid model";

                return View(ItemCreateModel);
            }
            catch (Exception ex)
            {
                ViewData["Type"] = new SelectList(_UnitOfWork.TimeOffTypes.GetAll(), "Id", "Title");
                ViewData["Position"] = new SelectList(_UnitOfWork.EmployeePositions.GetAll(), "Id", "Title");
                if (ex is ArgumentException || ex is ArgumentNullException || ex is EntityNotFoundException)
                {
                    ViewData["Error"] = ex.Message;
                }
                else
                {
                    ViewData["Error"] = "Unexpected error";
                }
                return View(ItemCreateModel);
            }
        }

        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _EmployeePositionTimeOffPolicyService.DeleteByIdAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        protected List<TimeOffPolicyApproverDTO> GetApproversFromJson(string JsonString)
        {
            var ApproversJson = JObject.Parse(JsonString);
            var Approvers = new List<TimeOffPolicyApproverDTO>();
            foreach (var item in ApproversJson)
            {
                var AprPosition = _UnitOfWork.EmployeePositions.Get(int.Parse(item.Key));
                var AprAmount = item.Value.Value<int>();
                if (AprPosition == null)
                {
                    continue;
                }
                Approvers.Add(new TimeOffPolicyApproverDTO()
                {
                    EmployeePosition = _mapper.Map<EmployeePosition, EmployeePositionDTO>(AprPosition),
                    Amount = AprAmount
                });
            }
            return Approvers;
        }
    }
}
