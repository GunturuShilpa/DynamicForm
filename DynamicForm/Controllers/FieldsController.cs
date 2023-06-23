using AutoMapper;
using Core.Services.ControlFields.Queries;
using Core.Services.Form.Commands;
using Core.Services.TemplateFields.Commands;
using Core.Services.TemplateFields.Queries;
using Core.Services.TemplateFields.Requests;
using DynamicForm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Dynamic;
using System.Linq.Dynamic;
using System.Linq;
namespace DynamicForm.Controllers
{
    public class FieldsController : BaseController<FieldsController>
    {
        private IMapper _mapper;

        public FieldsController(IMapper mapper)
        {
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int id = 0)
        {
            await LoadControlTypes();
            ViewBag.FormId = id;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveField(FieldRequest model)
        {
            model.Id = model.FieldId;

            var command = new AddEditFieldCommand(model);
            var response = await _mediator.Send(command);

            dynamic result = new ExpandoObject();
            result.error = response.Succeeded;
            result.message = response.Messages.FirstOrDefault();

            if (response.Succeeded)
            {
                result.error = false;
            }
            else
            {
                result.error = true;
            }

            result.message = response.Messages.FirstOrDefault();

            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetAllFields(int id)
        {
            dynamic res = new ExpandoObject();

            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();

            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();

            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();

            var searchValue = (Request.Form["search[value]"].FirstOrDefault() ?? "").Trim();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var mediatorResponse = await _mediator.Send(new GetAllFieldsQuery() { TemplateFormId = id });

            List<TemplateFieldsModel> formModel = (List<TemplateFieldsModel>)_mapper.Map<IEnumerable<TemplateFieldsModel>>(mediatorResponse.Data);

            if (!string.IsNullOrEmpty(searchValue))
            {
                searchValue = searchValue.ToLower();
                formModel = formModel.Where(x => (string.IsNullOrWhiteSpace(x.Name) == false && x.Name.ToLower().Contains(searchValue))
              || (string.IsNullOrWhiteSpace(x.DefaultValue) == false && x.DefaultValue.ToLower().Contains(searchValue))
              || (string.IsNullOrWhiteSpace(Convert.ToString(x.IsRequired)) == false && x.IsRequired.ToString().ToLower().Contains(searchValue))
               || (x.ControlId.ToString() == Convert.ToString(searchValue))


                                    ).ToList();
            }
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                formModel = formModel.OrderBy(sortColumn + " " + sortColumnDirection).ToList();
            }
            GetAllControlTypesQuery getAllControlTypes = new GetAllControlTypesQuery();
            var fieldsData = await _mediator.Send(getAllControlTypes);
            foreach (TemplateFieldsModel formFields in formModel)
            {
                formFields.ControlName = fieldsData.Data.Where(x => x.Id == formFields.ControlId).FirstOrDefault().Name;
            }

            recordsTotal = formModel.Count();
            var data = formModel.Skip(skip).Take(pageSize).ToList();

            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteForm(int id)
        {
            dynamic res = new ExpandoObject();

            DeleteFieldCommand command = new DeleteFieldCommand();
            command.Id = id;
            command.UserId = 0;

            var mediatorResponse = await _mediator.Send(command);

            if (mediatorResponse.Succeeded)
            {
                res.error = false;
            }
            else
                res.error = true;

            res.message = mediatorResponse.Messages.FirstOrDefault();

            return Json(res);
        }

        #region | Private Methods |

        private async Task LoadControlTypes()
        {
            var data = new List<SelectListItem>();

            try
            {
                var result = await _mediator.Send(new GetAllControlTypesQuery());

                data = result.Data.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
            }
            catch
            {
                //TODO: record ex into logger
            }

            ViewBag.ControlTypes = data;
        }

        #endregion
    }
}
