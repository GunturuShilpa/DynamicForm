using AutoMapper;
using Core.Services.Form.Queries;
using Core.Services.UserFormValue.Responses;
using Infrastructure.Form.Repository;
using Infrastructure.UserFormValue.Repository;
using MediatR;
using Shared.Result;

namespace Core.Services.UserFormValue.Queries
{
    public class GetAllUserFormValues : IRequest<Result<IEnumerable<UserFormValuesResponse>>>
    {
        public string Where { get; set; } = string.Empty;

    }
    internal class GetAllUserFormValuesHandler : IRequestHandler<GetAllUserFormValues, Result<IEnumerable<UserFormValuesResponse>>>
    {
        private readonly IMapper _mapper;

        private readonly IUserFormValuesRepository _formValuesRepository;

        public GetAllUserFormValuesHandler(IMapper mapper, IUserFormValuesRepository formValuesRepository)
        {
            _mapper = mapper;
            _formValuesRepository = formValuesRepository;
        }

        public async Task<Result<IEnumerable<UserFormValuesResponse>>> Handle(GetAllUserFormValues command, CancellationToken cancellationToken)
        {
            try
            {
                var rtn = await _formValuesRepository.GetByQuery(command.Where);

                if (rtn != null)
                {
                    IEnumerable<UserFormValuesResponse> response = _mapper.Map<IEnumerable<UserFormValuesResponse>>(rtn);

                    return await Result<IEnumerable<UserFormValuesResponse>>.SuccessAsync(response);
                }
                else
                {
                    return await Result<IEnumerable<UserFormValuesResponse>>.FailAsync();
                }
            }
            catch (Exception ex)
            {
                return await Result<IEnumerable<UserFormValuesResponse>>.FailAsync(ex.Message);
            }
        }
    }
}
