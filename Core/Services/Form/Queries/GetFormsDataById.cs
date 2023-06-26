using AutoMapper;
using Core.Services.Form.Responses;
using Infrastructure.Form.Repository;
using MediatR;
using Shared.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Form.Queries
{
    public class GetFormsDataById:IRequest<Result<FormResponse>>
    {
        public int Id { get; set; }
        //public GetFormsDataById(int id)
        //{
        //    Id = id;
        //}
    }
    internal class GetFormsDataByIdHandler : IRequestHandler<GetFormsDataById, Result<FormResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IFormRepository _formRepository;
        public GetFormsDataByIdHandler(IMapper mapper,IFormRepository formRepository)
        {
            _mapper = mapper;
            _formRepository = formRepository;
        }
        public async Task<Result<FormResponse>> Handle(GetFormsDataById request, CancellationToken cancellationToken)
        {
            try
            {
                var rtn = await _formRepository.GetById(request.Id);
                if (rtn != null)
                {
                    FormResponse response = _mapper.Map<FormResponse>(rtn);
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
