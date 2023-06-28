using AutoMapper;
using Core.Services.TemplateFields.Responses;
using Infrastructure.Field.Repository;
using MediatR;
using Shared.Result;

namespace Core.Services.TemplateFields.Queries
{
    public class GetFieldDetailsById : IRequest<Result<IEnumerable<FieldResponse>>>
    {
        public string Where { get; set; } = string.Empty;
    }
    internal class GetFieldDetailsByIdQueryHandler : IRequestHandler<GetFieldDetailsById, Result<IEnumerable<FieldResponse>>>
    {
        private readonly IMapper _mapper;

        private readonly IFieldRepository _fieldRepository;

        public GetFieldDetailsByIdQueryHandler(IMapper mapper, IFieldRepository fieldRepository)
        {
            _mapper = mapper;
            _fieldRepository = fieldRepository;

        }

        public async Task<Result<IEnumerable<FieldResponse>>> Handle(GetFieldDetailsById command, CancellationToken cancellationToken)
        {
            try
            {
                var rtn = await _fieldRepository.GetByQuery(command.Where);

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
