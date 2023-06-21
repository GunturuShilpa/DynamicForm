using AutoMapper;
using Core.Services.Form.Responses;
using Infrastructure.Form.Repository;
using Shared;
using MediatR;
using Shared.Result;

namespace Core.Services.Form.Queries
{
    public class GetAllFormsQuery : IRequest<Result<FormResponse>>
    {
        public int Status { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public string SearchText { get; set; }

        public GetAllFormsQuery(int status, int pageNumber, int pageSize, string searchText)
        {
            Status = status;
            PageSize = pageSize;
            PageNumber = pageNumber;
            SearchText = searchText;
        }

    }
    internal class GetAllFormsQueryHandler : IRequestHandler<GetAllFormsQuery, Result<FormResponse>>
    {
        private readonly IMapper _mapper;

        private readonly IFormRepository _formRepository;


        public GetAllFormsQueryHandler(IMapper mapper, IFormRepository formRepository)
        {
            _mapper = mapper;
            _formRepository = formRepository;
           
        }

        public async Task<Result<FormResponse>> Handle(GetAllFormsQuery command, CancellationToken cancellationToken)
        {
            try
            {
                string query = string.Empty;

                if (command.Status != 0)
                {
                    query += String.Format("where status = {0}", command.Status);
                }

                var rtn = await _formRepository.GetByQuery(query);

                if (rtn != null)
                {
                    var response = _mapper.Map<FormResponse>(rtn);

                    return await Result<FormResponse>.SuccessAsync(response);
                }
                else
                {
                    return await Result<FormResponse>.FailAsync();
                }
            }
            catch (Exception ex)
            {
                return await Result<FormResponse>.FailAsync(ex.Message);
            }
        }
    }
}
