using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFinder.Application.UseCases.WordFinder.Queries.GetMostRepeatedWordsQuery
{
    public class GetMostRepeatedWordsQueryValidator: AbstractValidator<GetMostRepeatedWordsQuery>
    {
        public GetMostRepeatedWordsQueryValidator()
        {
            RuleFor(x => x.matrix).NotEmpty();
            RuleFor(x => x.wordStream).NotEmpty();
        }
    }
}
