using AutoMapper;
using Core.Services.Form.Queries;
using Core.Services.TemplateFields.Queries;
using Core.Services.TemplateFields.Responses;
using Infrastructure.Field.Repository;
using Infrastructure.Form.Repository;
using Infrastructure.Form.Responses;
using MediatR;
using Shared.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.TemplateFields.Queries
{
    public class GetAllFieldsQuery : IRequest<Result<IEnumerable<FieldResponse>>>
    {
        public string TemplateFormId { get; set; }
    }
    internal class GetAllFieldsQueryHandler : IRequestHandler<GetAllFieldsQuery, Result<IEnumerable<FieldResponse>>>
    {
        private readonly IMapper _mapper;

        private readonly IFieldRepository _fieldRepository;


        public GetAllFieldsQueryHandler(IMapper mapper, IFieldRepository fieldRepository)
        {
            _mapper = mapper;
            _fieldRepository = fieldRepository;

        }

        public async Task<Result<IEnumerable<FieldResponse>>> Handle(GetAllFieldsQuery command, CancellationToken cancellationToken)
        {
            try
            {
                var rtn = await _fieldRepository.GetByQuery(command.TemplateFormId);

                if (rtn != null)
                {
                    IEnumerable<FieldResponse> response = _mapper.Map<IEnumerable<FieldResponse>>(rtn);

                    return await Result<IEnumerable<FieldResponse>>.SuccessAsync(response);

                }
                else
                {
                    return await Result<IEnumerable<FieldResponse>>.FailAsync();
                }
            }
            catch (Exception ex)
            {
                return await Result<IEnumerable<FieldResponse>>.FailAsync(ex.Message);
            }
        }        
    }
}
