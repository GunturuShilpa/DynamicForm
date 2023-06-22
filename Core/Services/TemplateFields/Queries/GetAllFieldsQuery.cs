using AutoMapper;
using Core.Services.TemplateFields.Responses;
using Infrastructure.Field.Repository;
using MediatR;
using Shared.Result;

namespace Core.Services.TemplateFields.Queries
{
    public class GetAllFieldsQuery : IRequest<Result<IEnumerable<FieldResponse>>>
    {
        public string TemplateFormId { get; set; } = string.Empty;
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
