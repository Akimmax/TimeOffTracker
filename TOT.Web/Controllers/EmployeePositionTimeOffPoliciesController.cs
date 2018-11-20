using TOT.Dto;
using TOT.Data;
using TOT.Dto.SelectLists;
using System.Linq;
using TOT.Interfaces;
using TOT.Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TOT.Entities.TimeOffPolicies;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TOT.Entities;

namespace TOT.Web.Controllers
{
    public class EmployeePositionTimeOffPoliciesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _UnitOfWork;
        private readonly EmployeePositionTimeOffPolicyService _EmployeePositionTimeOffPolicyService;

        public EmployeePositionTimeOffPoliciesController(IUnitOfWork UnitOfWork, IMapper mapper,
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

        public IActionResult Create()
        {
            ViewData["Type"] = new SelectList(_UnitOfWork.TimeOffTypes.GetAll(), "Id", "Title");
            ViewData["Position"] = new SelectList(_UnitOfWork.EmployeePositions.GetAll(), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm]PolicyCreateModel ItemCreateModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var ApproversJson = JObject.Parse(ItemCreateModel.Approvers);
                    var Approvers = new List<TimeOffPolicyApproverDTO>();
                    foreach (var item in ApproversJson)
                    {
                        var AprPosition = _UnitOfWork.EmployeePositions.Get(int.Parse(item.Key));
                        var AprAmount = item.Value.Value<int>();
                    if (AprPosition == null)
                        {
                            continue;
                        }
                        Approvers.Add(new TimeOffPolicyApproverDTO() {
                            EmployeePosition = _mapper.Map<EmployeePosition, EmployeePositionDTO>(AprPosition),
                            Amount = AprAmount});
                    }
                    if (Approvers.Count < 1)
                    {
                        ViewData["Type"] = new SelectList(_UnitOfWork.TimeOffTypes.GetAll(), "Id", "Title");
                        ViewData["Position"] = new SelectList(_UnitOfWork.EmployeePositions.GetAll(), "Id", "Title");
                        ViewData["Error"] = "There should be at least 1 approver";
                        return View(ItemCreateModel);
                    }
                    var ItemDTO = _mapper.Map<PolicyCreateModel, EmployeePositionTimeOffPolicyDTO>(ItemCreateModel);
                    ItemDTO.Approvers = Approvers;
                    _EmployeePositionTimeOffPolicyService.CreateAsync(ItemDTO);
                    return RedirectToAction(nameof(Index));
                }
                ViewData["Type"] = new SelectList(_UnitOfWork.TimeOffTypes.GetAll(), "Id", "Title");
                ViewData["Position"] = new SelectList(_UnitOfWork.EmployeePositions.GetAll(), "Id", "Title");
                ViewData["Error"] = "Invalid model";

                return View(ItemCreateModel);
            }
            catch (System.Exception ex)
            {
                ViewData["Type"] = new SelectList(_UnitOfWork.TimeOffTypes.GetAll(), "Id", "Title");
                ViewData["Position"] = new SelectList(_UnitOfWork.EmployeePositions.GetAll(), "Id", "Title");
                ViewData["Error"] = ex.Message;
                return View(ItemCreateModel);
            }
        }

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
        public IActionResult Edit(int id, PolicyCreateModel ItemCreateModel)
        {
            if (id != ItemCreateModel.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                if (_UnitOfWork.EmployeePositionTimeOffPolicies
                    .Find(x=>x.PositionId == ItemCreateModel.Position.Id &&
                    x.TypeId == ItemCreateModel.Type.Id) != null)
                {
                    ViewData["Type"] = new SelectList(_UnitOfWork.TimeOffTypes.GetAll(), "Id", "Title");
                    ViewData["Position"] = new SelectList(_UnitOfWork.EmployeePositions.GetAll(), "Id", "Title");
                    ViewData["Error"] = "Policy for this Position and Type alredy exist.";
                    return View(ItemCreateModel);
                }
                var ApproversJson = JObject.Parse(ItemCreateModel.Approvers);
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
                if (Approvers.Count < 1)
                {
                    ViewData["Type"] = new SelectList(_UnitOfWork.TimeOffTypes.GetAll(), "Id", "Title");
                    ViewData["Position"] = new SelectList(_UnitOfWork.EmployeePositions.GetAll(), "Id", "Title");
                    ViewData["Error"] = "There should be at least 1 approver";
                    return View(ItemCreateModel);
                }
                var ItemDTO = _mapper.Map<PolicyCreateModel, EmployeePositionTimeOffPolicyDTO>(ItemCreateModel);
                ItemDTO.Approvers = Approvers;
                _EmployeePositionTimeOffPolicyService.UpdateAsync(ItemDTO);
                return RedirectToAction(nameof(Index));
            }
            ViewData["Type"] = new SelectList(_UnitOfWork.TimeOffTypes.GetAll(), "Id", "Title");
            ViewData["Position"] = new SelectList(_UnitOfWork.EmployeePositions.GetAll(), "Id", "Title");
            ViewData["Error"] = "Invalid model";

            return View(ItemCreateModel);
        }

        public IActionResult Delete(int id)
        {
            try
            {
                _EmployeePositionTimeOffPolicyService.DeleteByIdAsync(id);
                return Ok();
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }
    }
}
