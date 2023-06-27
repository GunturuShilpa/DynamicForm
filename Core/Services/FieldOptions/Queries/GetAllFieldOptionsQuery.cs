using AutoMapper;
using Core.Services.FieldOptions.Responses;
using Core.Services.Form.Queries;
using Infrastructure.FieldOptions.Repository;
using Infrastructure.Form.Repository;
using MediatR;
using Shared.Result;

namespace Core.Services.FieldOptions.Queries
{
    public class GetAllFieldOptionsQuery : IRequest<Result<IEnumerable<FieldOptionsResponse>>>
    {
        public string Where { get; set; } = string.Empty;
    }
    internal class GetAllFieldOptionsQueryHandler : IRequestHandler<GetAllFieldOptionsQuery, Result<IEnumerable<FieldOptionsResponse>>>
    {
        private readonly IMapper _mapper;

        private readonly IFormFieldOptionsRepository _fieldOptionsRepository;

        public GetAllFieldOptionsQueryHandler(IMapper mapper, IFormFieldOptionsRepository fieldOptionsRepository)
        {
            _mapper = mapper;
            _fieldOptionsRepository = fieldOptionsRepository;
        }

        public async Task<Result<IEnumerable<FieldOptionsResponse>>> Handle(GetAllFieldOptionsQuery command, CancellationToken cancellationToken)
        {
            try
            {
                var rtn = await _fieldOptionsRepository.GetByQuery(command.Where);

                if (rtn != null)
                {
                    IEnumerable<FieldOptionsResponse> response = _mapper.Map<IEnumerable<FieldOptionsResponse>>(rtn);

                    return await Result<IEnumerable<FieldOptionsResponse>>.SuccessAsync(response);
                }
                else
                {
                    return await Result<IEnumerable<FieldOptionsResponse>>.FailAsync();
                }
            }
            catch (Exception ex)
            {
                return await Result<IEnumerable<FieldOptionsResponse>>.FailAsync(ex.Message);
            }
        }
    }
}
