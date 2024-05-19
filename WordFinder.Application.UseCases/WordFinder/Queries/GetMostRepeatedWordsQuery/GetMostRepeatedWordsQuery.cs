using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordFinder.Application.UseCases.Commons.Bases;

namespace WordFinder.Application.UseCases.WordFinder.Queries.GetMostRepeatedWordsQuery
{
    public class GetMostRepeatedWordsQuery : IRequest<BaseResponse<IEnumerable<string>>>
    {
        public IEnumerable<string> matrix { get; set; }
        public IEnumerable<string> wordStream { get; set; }
    }
}
