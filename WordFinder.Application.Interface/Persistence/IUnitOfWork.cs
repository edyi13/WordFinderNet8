using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFinder.Application.Interface.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IWordFinderRepository WordFinder { get; }
    }
}
