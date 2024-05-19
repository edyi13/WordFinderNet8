using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFinder.Application.UseCases.WordFinder.Queries.GetNewMatrixQuery
{
    public class GetNewMatrixQueryValidator: AbstractValidator<GetNewMatrixQuery>
    {
        public GetNewMatrixQueryValidator()
        {
            RuleFor(x => x.size).GreaterThan(0);
            RuleFor(x => x.words).NotEmpty();
        }
    }
}
