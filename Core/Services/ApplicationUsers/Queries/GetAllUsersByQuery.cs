using AutoMapper;
using Core.Services.ApplicationUsers.Responses;
using Infrastructure.ApplicationUsers.Repository;
using MediatR;
using Shared.Result;

namespace Core.Services.ApplicationUsers.Queries
{

    public class GetAllUsersByQuery : IRequest<Result<IEnumerable<UserResponse>>>
    {
        public string Where { get; set; } = string.Empty;
    }

    internal class GetAllUsersByQueryHandler : IRequestHandler<GetAllUsersByQuery, Result<IEnumerable<UserResponse>>>
    {
        private readonly IMapper _mapper;

        private readonly IApplicationUserRepository _applicationUserRepository;

        public GetAllUsersByQueryHandler(IMapper mapper, IApplicationUserRepository applicationUserRepository)
        {
            _mapper = mapper;
            _applicationUserRepository = applicationUserRepository;
        }

        public async Task<Result<IEnumerable<UserResponse>>> Handle(GetAllUsersByQuery command, CancellationToken cancellationToken)
        {
            try
            {
                var rtn = await _applicationUserRepository.GetByQuery(command.Where);

                if (rtn != null)
                {
                    IEnumerable<UserResponse> response = _mapper.Map<IEnumerable<UserResponse>>(rtn);

                    return await Result<IEnumerable<UserResponse>>.SuccessAsync(response);
                }
                else
                {
                    return await Result<IEnumerable<UserResponse>>.FailAsync();
                }
            }
            catch (Exception ex)
            {
                return await Result<IEnumerable<UserResponse>>.FailAsync(ex.Message);
            }
        }
    }
}
