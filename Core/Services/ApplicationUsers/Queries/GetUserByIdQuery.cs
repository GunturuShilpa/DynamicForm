using AutoMapper;
using Core.Services.ApplicationUsers.Responses;
using Infrastructure.ApplicationUsers.Repository;
using MediatR;
using Shared.Result;

namespace Core.Services.ApplicationUsers.Queries
{
    public class GetUserByIdQuery : IRequest<Result<UserResponse>>
    {
        public int Id { get; set; }
    }
    internal class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationUserRepository _applicationUserRepository;
        public GetUserByIdQueryHandler(IMapper mapper, IApplicationUserRepository applicationUserRepository)
        {
            _mapper = mapper;
            _applicationUserRepository = applicationUserRepository;
        }
        public async Task<Result<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var rtn = await _applicationUserRepository.GetById(request.Id);
                if (rtn != null)
                {
                    UserResponse response = _mapper.Map<UserResponse>(rtn);
                    return await Result<UserResponse>.SuccessAsync(response);
                }
                else
                {
                    return await Result<UserResponse>.FailAsync();
                }
            }
            catch (Exception ex)
            {
                return await Result<UserResponse>.FailAsync(ex.Message);
            }
        }
    }
}
