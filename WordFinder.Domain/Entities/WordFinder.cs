using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFinder.Domain.Entities
{
    public class WordFinder
    {
        public IEnumerable<string> Matrix { get; set; }
        public List<string> WordStream { get; set; }
    }
}
