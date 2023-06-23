using AutoMapper;
using Core.Services.TemplateFields.Responses;
using Infrastructure.Field.Repository;
using MediatR;
using Shared.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.TemplateFields.Queries
{
    public class GetFieldPropertiesQuery : IRequest<Result<FieldResponse>>
    {
        public int Id { get; set; }
    }

    internal class GetFieldPropertiesQueryHandler : IRequestHandler<GetFieldPropertiesQuery, Result<FieldResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IFieldRepository _fieldRepository;

        public GetFieldPropertiesQueryHandler(IMapper mapper, IFieldRepository fieldRepository)
        {
            _mapper = mapper;
            _fieldRepository = fieldRepository;

        }
        
        public async Task<Result<FieldResponse>> Handle(GetFieldPropertiesQuery command, CancellationToken cancellationToken)
        {
            try
            {
                var sql = $"WHERE Id = {command.Id} ";
                var rtn = await _fieldRepository.GetById(command.Id);

                if (rtn != null)
                {
                    var response = _mapper.Map<FieldResponse>(rtn);

                    return await Result<FieldResponse>.SuccessAsync(response);
                }
                else
                {
                    return await Result<FieldResponse>.FailAsync();
                }
            }
            catch (Exception ex)
            {
                return await Result<FieldResponse>.FailAsync(ex.Message);
            }
        }
    }
}
