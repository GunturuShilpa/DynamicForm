using AutoMapper;
using Core.Services.ControlFields.Responses;
using Infrastructure.ControlType.Repository;
using MediatR;
using Shared.Result;

namespace Core.Services.ControlFields.Queries
{
    public class GetAllControlTypesQuery : IRequest<Result<IEnumerable<ControlTypesResponse>>>
    {
        public string Where { get; set; } = "";
    }

    internal class GetAllControlTypesQueryHandler : IRequestHandler<GetAllControlTypesQuery, Result<IEnumerable<ControlTypesResponse>>>
    {
        private readonly IMapper _mapper;

        private readonly IControlTypesRepository _controlTypesRepository;


        public GetAllControlTypesQueryHandler(IMapper mapper, IControlTypesRepository controlTypesRepository)
        {
            _mapper = mapper;
            _controlTypesRepository = controlTypesRepository;

        }

        public async Task<Result<IEnumerable<ControlTypesResponse>>> Handle(GetAllControlTypesQuery command, CancellationToken cancellationToken)
        {
            try
            {
                var rtn = await _controlTypesRepository.GetByQuery(command.Where);

                if (rtn != null)
                {
                    IEnumerable<ControlTypesResponse> response = _mapper.Map<IEnumerable<ControlTypesResponse>>(rtn);

                    return await Result<IEnumerable<ControlTypesResponse>>.SuccessAsync(response);

                }
                else
                {
                    return await Result<IEnumerable<ControlTypesResponse>>.FailAsync();
                }
            }
            catch (Exception ex)
            {
                return await Result<IEnumerable<ControlTypesResponse>>.FailAsync(ex.Message);
            }
        }
    }
}
