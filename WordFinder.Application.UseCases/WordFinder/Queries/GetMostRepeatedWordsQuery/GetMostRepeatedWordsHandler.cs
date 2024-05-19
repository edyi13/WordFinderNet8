using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using WordFinder.Application.Interface.Persistence;
using WordFinder.Application.UseCases.Commons.Bases;

namespace WordFinder.Application.UseCases.WordFinder.Queries.GetMostRepeatedWordsQuery
{
    public class GetMostRepeatedWordsHandler : IRequestHandler<GetMostRepeatedWordsQuery, BaseResponse<IEnumerable<string>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMostRepeatedWordsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<IEnumerable<string>>> Handle(GetMostRepeatedWordsQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<IEnumerable<string>>();
            try
            {
                var words = await _unitOfWork.WordFinder.Find(request.matrix, request.wordStream);
                if (words is not null)
                {
                    response.Data = words;
                    response.Message = "Words found successfully";
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }
    }
}
