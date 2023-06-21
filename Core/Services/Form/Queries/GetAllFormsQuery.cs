using AutoMapper;
using Core.Services.Form.Responses;
using Infrastructure.Form.Repository;
using Shared;
using MediatR;
using Shared.Result;

namespace Core.Services.Form.Queries
{
    public class GetAllFormsQuery : IRequest<Result<IEnumerable<FormResponse>>>
    {
        public string Where { get; set; }

        //public GetAllFormsQuery(int status, int pageNumber, int pageSize, string searchText)
        //{
        //    Status = status;
        //    PageSize = pageSize;
        //    PageNumber = pageNumber;
        //    SearchText = searchText;
        //}

    }
    internal class GetAllFormsQueryHandler : IRequestHandler<GetAllFormsQuery, Result<IEnumerable<FormResponse>>>
    {
        private readonly IMapper _mapper;

        private readonly IFormRepository _formRepository;


        public GetAllFormsQueryHandler(IMapper mapper, IFormRepository formRepository)
        {
            _mapper = mapper;
            _formRepository = formRepository;
           
        }

        public async Task<Result<IEnumerable<FormResponse>>> Handle(GetAllFormsQuery command, CancellationToken cancellationToken)
        {
            try
            {
                var rtn = await _formRepository.GetByQuery(command.Where);

                if (rtn != null)
                {
                    IEnumerable<FormResponse> response = _mapper.Map<IEnumerable<FormResponse>>(rtn);

                    return await Result<IEnumerable<FormResponse>>.SuccessAsync(response);

                }
                else
                {
                    return await Result<IEnumerable<FormResponse>>.FailAsync();
                }
            }
            catch (Exception ex)
            {
                return await Result<IEnumerable<FormResponse>>.FailAsync(ex.Message);
            }
        }
    }
}
