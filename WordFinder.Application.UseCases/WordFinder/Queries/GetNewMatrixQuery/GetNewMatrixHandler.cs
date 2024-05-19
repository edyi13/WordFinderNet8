using MediatR;
using WordFinder.Application.Interface.Persistence;
using WordFinder.Application.UseCases.Commons.Bases;

namespace WordFinder.Application.UseCases.WordFinder.Queries.GetNewMatrixQuery
{
    public class GetNewMatrixHandler : IRequestHandler<GetNewMatrixQuery, BaseResponse<IEnumerable<string>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetNewMatrixHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<IEnumerable<string>>> Handle(GetNewMatrixQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<IEnumerable<string>>();
            try
            {
                var matrix = await _unitOfWork.WordFinder.GenerateMatrix(request.words, request.size);
                if (matrix is not null)
                {
                    response.Data = matrix;
                    response.Message = "Matrix generated successfully";
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
