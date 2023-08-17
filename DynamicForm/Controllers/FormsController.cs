using AutoMapper;
using Core.Services.Form.Commands;
using Core.Services.Form.Queries;
using Core.Services.Form.Requests;
using DynamicForm.Filters;
using DynamicForm.Models;
using Infrastructure.Form.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Linq.Dynamic;
using System.Security.Claims;

namespace DynamicForm.Controllers
{
    public class FormsController : BaseController<FormsController>
    {
        private IMapper _mapper;
        public FormsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveForm(FormRequest model)
        {

            dynamic res = new ExpandoObject();
            var ifFormNameExist = await CheckIfFormExist(model.Name);
            if (ifFormNameExist)
            {
                res.error = true;
                res.duplicateForm = true;
                res.message = "This" + " " + model.Name + " " + "Form already exist.";
            }
            else
            {
                model.CreatedBy = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var command = new AddEditFormCommand(model);
                var mediatorResponse = await _mediator.Send(command);

                if (mediatorResponse.Succeeded)
                {
                    res.error = false;
                }
                else
                {
                    res.error = true;
                }
                res.message = mediatorResponse.Messages.FirstOrDefault();
            }
            return Json(res);
        }

        [HttpPost]
        public async Task<IActionResult> SaveEditForm(FormRequest model)
        {
            dynamic result = new ExpandoObject();
            model.ModifiedBy = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var command = new AddEditFormCommand(model);
            var mediatorResponse = await _mediator.Send(command);

            if (mediatorResponse.Succeeded)
            {
                result.error = false;
            }
            else
            {
                result.error = true;
            }
            result.message = mediatorResponse.Messages.FirstOrDefault();
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetAllForms()
        {
            string where = string.Empty;
            var roleId = Convert.ToInt32(HttpContext.User.FindFirst("RoleId").Value);
            if (roleId != 1)
            {
                var userId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                where = string.Format(" where createdBy={0}", userId);
            }

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

            var mediatorResponse = await _mediator.Send(new GetAllFormsQuery() { Where = $"where Status in (1,2)" });

            List<TemplateFormModel> formModel = (List<TemplateFormModel>)_mapper.Map<IEnumerable<TemplateFormModel>>(mediatorResponse.Data);

            if (!string.IsNullOrEmpty(searchValue))
            {
                searchValue = searchValue.ToLower();
                formModel = formModel.Where(x => (string.IsNullOrWhiteSpace(x.Name) == false && x.Name.ToLower().Contains(searchValue))
                                    || (string.IsNullOrWhiteSpace(x.Description) == false && x.Description.ToLower().Contains(searchValue))
                                    || (string.IsNullOrWhiteSpace(Convert.ToString(x.Status)) == false && x.Status.ToString().ToLower().Contains(searchValue))
                                    || (searchValue == "pending" && x.Status == 1)
                                    || (searchValue == "published" && x.Status == 2)
                                    || (searchValue == "unpublished" && x.Status == 3)
                                    || (searchValue == "deleted" && x.Status == 4)
                                    ).ToList();
            }
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                formModel = formModel.OrderBy(sortColumn + " " + sortColumnDirection).ToList();
            }

            recordsTotal = formModel.Count();
            var data = formModel.Skip(skip).Take(pageSize).Select(x => new {
                x.Id,
                x.Name,
                x.Description,
                x.Ordinal,
                x.Status,
                formLink = CustomQueryStringHelper.EncryptString("", "Index", "DynamicForm", new { id = x.Id })
            }).ToList();

            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteForm(int id)
        {
            dynamic res = new ExpandoObject();

            DeleteFormCommand command = new DeleteFormCommand();
            command.Id = id;
            command.UserId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

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

        [HttpGet]
        public async Task<IActionResult> GetFormPropertiesById(int id)
        {
            GetFormsDataById query = new() { Id = id };
            var formProperties = await _mediator.Send(query);
            var formPropertiesModel = _mapper.Map<TemplateFormModel>(formProperties.Data);
            return PartialView("_EditForm", formPropertiesModel);
        }

        [HttpPost]
        public async Task<IActionResult> PublishForm(int id)
        {
            dynamic res = new ExpandoObject();

            PublishFormCommand command = new PublishFormCommand();
            command.Id = id;
            command.UserId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

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

        [HttpPost]
        public async Task<IActionResult> UnPublishForm(int id)
        {
            dynamic res = new ExpandoObject();

            UnPublishFormCommand command = new UnPublishFormCommand();
            command.Id = id;
            command.UserId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

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

        public async Task<bool> CheckIfFormExist(string formName)
        {
            var templateData = await _mediator.Send(new GetAllFormsQuery { Where = "where Name='" + formName + "' and Status= 1" });
            return templateData.Data.Count() > 0;
        }


    }
}
