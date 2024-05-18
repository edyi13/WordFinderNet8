using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFinder.Application.Interface.Persistence
{
    public interface IWordFinderRepository
    {
        IEnumerable<string> Find(IEnumerable<string> matrix, IEnumerable<string> wordstream);
        IEnumerable<string> GenerateMatrix(IEnumerable<string> words, int matrixSize);
    }
}
