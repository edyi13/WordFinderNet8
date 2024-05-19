using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordFinder.Application.UseCases.Commons.Bases;

namespace WordFinder.Application.UseCases.WordFinder.Queries.GetNewMatrixQuery
{
    public class GetNewMatrixQuery : IRequest<BaseResponse<IEnumerable<string>>>
    {
        public IEnumerable<string> words { get; set; }
        public int size { get; set; }
    }
}
